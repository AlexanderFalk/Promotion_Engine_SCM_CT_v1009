using System;
using System.Collections.Generic;
using Promotion_Engine_SCM_CT_v1009.Core.Interfaces;
using Promotion_Engine_SCM_CT_v1009.Models;

namespace Promotion_Engine_SCM_CT_v1009.Core.Business
{
    public class CartActions : ICartActions
    {
        private Cart _cart = new Cart();

        public CartActions()
        {
        }

        public void Add(SKUEnum sku)
        {
            throw new NotImplementedException();
        }

        public void Remove(SKUEnum sku)
        {
            throw new NotImplementedException();
        }

        public int Size()
        {
            throw new NotImplementedException();
        }
    }
}
