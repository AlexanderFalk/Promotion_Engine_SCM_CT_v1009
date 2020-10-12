using System;
using System.Collections.Generic;
using System.Linq;
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
            // TODO - Missing implementation
            return 0.0;
        }

        /// <summary>
        /// Checks whether the cart is eligible for any Promotions
        /// </summary>
        /// <returns>A bool whether it is eligible</returns>
        private bool CheckForPromotionEligibility()
        {
            var promotions = _promotionEngine.GetPromotions();

            foreach (var promotion in promotions)
            {
                var tempDict = new Dictionary<SKUEnum, int>();
                foreach (var data in promotion.Promotions)
                {
                    tempDict.Add(data.SKU, data.Count);
                }

                var matchCount = tempDict
                    .Where(entry => _cart.SKUs.ContainsKey(entry.Key) && _cart.SKUs[entry.Key] == entry.Value)
                    .Count();

                if (tempDict.Count == matchCount)
                {
                    _cart.IsPromotionUsed = true;
                    _cart.ActivePromotion = promotion;
                }

                if(_cart.IsPromotionUsed)
                {
                    break;
                }
            }

            return false;
        }
    }
}
