using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Promotion_Engine_SCM_CT_v1009.Core.Database;
using Promotion_Engine_SCM_CT_v1009.Core.Interfaces;
using Promotion_Engine_SCM_CT_v1009.Models;

namespace Promotion_Engine_SCM_CT_v1009.Core.DataAccess
{
    public class PromotionEngine : IPromotionEngine
    {
        private PromotionEngineDbContext _context;
        public PromotionEngine(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<PromotionEngineDbContext>();
        }

        public void AddSkuToCart(SKUEnum sku, int count = 1)
        {
            throw new NotImplementedException();
        }

        public void DeleteSkuFromCart(SKUEnum sku)
        {
            throw new NotImplementedException();
        }

        public List<PromotionType> GetPromotions()
        {
            return _context
                .PromotionTypes
                .Include(prom => prom.Promotions)
                .Include(prom => prom.Discount)
                .ToList();
        }

        public double GetSkuPrice(SKUEnum sku)
        {
            throw new NotImplementedException();
        }
    }
}
