using System;
using System.Linq;
using Promotion_Engine_SCM_CT_v1009.Core.Interfaces;
using Promotion_Engine_SCM_CT_v1009.Models;

namespace Promotion_Engine_SCM_CT_v1009.Core.Business
{
    public class CartActions : ICartActions
    {
        private Cart _cart = new Cart();

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
    }
}
