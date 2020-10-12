using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Promotion_Engine_SCM_CT_v1009.Core.Interfaces;
using Promotion_Engine_SCM_CT_v1009.Models;

namespace Promotion_Engine_SCM_CT_v1009.Core.Business
{
    public class CartBuilder : ICartBuilder
    {
        private Cart _cart = new Cart();

        private IPromotionEngine _promotionEngine;
        public CartBuilder(IServiceProvider serviceProvider)
        {
            _promotionEngine = serviceProvider.GetRequiredService<IPromotionEngine>();
        }

        public void Add(SKUEnum sku)
        {
            // If key is present, update the counter. Else add new key with value of 1
            if (_cart.SKUs.ContainsKey(sku))
            {
                _cart.SKUs[sku] = _cart.SKUs[sku] + 1;
            }
            else
            {
                _cart.SKUs.Add(sku, 1);
            }
            CheckForPromotionEligibility();
        }

        public void Remove(SKUEnum sku)
        {
            // If key is present, there must exist one or more
            if (_cart.SKUs.ContainsKey(sku))
            {
                // If the value is larger than 1, remove one. Else remove the SKU
                if (_cart.SKUs[sku] > 1)
                {
                    _cart.SKUs[sku] = _cart.SKUs[sku] - 1;
                }
                else
                {
                    _cart.SKUs.Remove(sku);
                }
            }
        }

        public int Size()
        {
            return _cart.SKUs.Aggregate(0, (total, next) => total + next.Value);
        }

        public Cart GetCart()
        {
            return _cart;
        }

        public double GetTotalCost()
        {
            _cart.TotalCost = 0.0;
            if (_cart.IsPromotionUsed)
            {
                var hasPromotionCostBeenCalculated = false;
                // Calculate those SKUs not a part of a Promotion
                foreach (var sku in _cart.SKUs)
                {
                    if (!_cart.ActivePromotion.Promotions.Any(e => e.SKU.Equals(sku.Key)))
                    {
                        _cart.TotalCost += (int)sku.Key;
                    }
                }
                foreach (var key in _cart.ActivePromotion.Promotions)
                {
                    var quotient = (int)Math.Ceiling((double)key.Count / _cart.SKUs[key.SKU]);
                    var remainder = key.Count % _cart.SKUs[key.SKU];
                    if (hasPromotionCostBeenCalculated)
                    {
                        if (remainder > 0)
                            _cart.TotalCost += (int)key.SKU * remainder;
                    }
                    else
                    {
                        if (remainder > 0)
                        {
                            _cart.TotalCost += _cart.ActivePromotion.Cost * quotient + ((int)key.SKU * remainder);
                        }
                        else
                        {
                            _cart.TotalCost += _cart.ActivePromotion.Cost * quotient;
                        }
                        hasPromotionCostBeenCalculated = true;
                    }
                }
            }
            else
            {
                foreach (var key in _cart.SKUs)
                {
                    _cart.TotalCost += key.Value * (int)key.Key;
                }
            }
            return _cart.TotalCost;
        }

        /// <summary>
        /// Checks whether the cart is eligible for any Promotions
        /// </summary>
        /// <returns>A bool whether it is eligible</returns>
        private bool CheckForPromotionEligibility()
        {
            var promotions = _promotionEngine.GetPromotions();

            // If no promotion has been added, then we check for eligibility
            if (!_cart.IsPromotionUsed)
            {
                foreach (var promotion in promotions)
                {
                    // Create temporary dict to store a promotion like the SKUs
                    // are stored in the cart. 
                    var tempDict = new Dictionary<SKUEnum, int>();
                    foreach (var data in promotion.Promotions)
                    {
                        tempDict.Add(data.SKU, data.Count);
                    }

                    // Check if any promotion has matched
                    var matchCount = tempDict
                        .Where(entry => _cart.SKUs.ContainsKey(entry.Key) && _cart.SKUs[entry.Key] % entry.Value == 0)
                        .ToDictionary(entry => entry.Key, entry => entry.Value);

                    // If any promotion has matched, we save the promotion type
                    // and break out of the loop.
                    if (tempDict.Count == matchCount.Count)
                    {
                        _cart.IsPromotionUsed = true;
                        _cart.ActivePromotion = promotion;
                        break;
                    }
                }
            }

            return _cart.IsPromotionUsed;
        }
    }
}
