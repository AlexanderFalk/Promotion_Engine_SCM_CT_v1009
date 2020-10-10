using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Promotion_Engine_SCM_CT_v1009.Core.Database;
using Promotion_Engine_SCM_CT_v1009.Models;

namespace Promotion_Engine_SCM_CT_v1009.Utilities
{
    public static class DataGenerator
    {
        public static void Generate(IServiceProvider serviceProvider)
        {
            using var context = new PromotionEngineDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<PromotionEngineDbContext>>());

            // Check whether data has been generated
            if (context.PromotionTypes.Any())
            {
                return; // Data already generated
            }

            // Setup ID's for Promotions
            string promotionId1 = "prom_" + PrimaryKeyGenerator.Generate();
            string promotionId2 = "prom_" + PrimaryKeyGenerator.Generate();
            string promotionId3 = "prom_" + PrimaryKeyGenerator.Generate();
            string promotionId4 = "prom_" + PrimaryKeyGenerator.Generate();

            // Setup ID's for Promotion Types
            string promotionTypeId1 = "ptype_" + PrimaryKeyGenerator.Generate();
            string promotionTypeId2 = "ptype_" + PrimaryKeyGenerator.Generate();
            string promotionTypeId3 = "ptype_" + PrimaryKeyGenerator.Generate();

            // Insert promotion types
            context.PromotionTypes.AddRange(
                new PromotionType
                {
                    PromotionTypeId = promotionTypeId1,
                    Cost = 130
                },
                new PromotionType
                {
                    PromotionTypeId = promotionTypeId2,
                    Cost = 45
                },
                new PromotionType
                {
                    PromotionTypeId = promotionTypeId3,
                    Cost = 30
                }
            );

            // Insert promotions:
            //  3A's  = 130
            //  2B's  = 45
            //  C + D = 30
            context.Promotions.AddRange(
                new Promotion
                {
                    PromotionId = promotionId1,
                    PromotionTypeId = promotionTypeId1,
                    SKU = SKUEnum.A,
                    Count = 3
                },
                new Promotion
                {
                    PromotionId = promotionId2,
                    PromotionTypeId = promotionTypeId2,
                    SKU = SKUEnum.B,
                    Count = 2
                },
                new Promotion
                {
                    PromotionId = promotionId3,
                    PromotionTypeId = promotionTypeId3,
                    SKU = SKUEnum.C,
                    Count = 1
                },
                new Promotion
                {
                    PromotionId = promotionId4,
                    PromotionTypeId = promotionTypeId3,
                    SKU = SKUEnum.D,
                    Count = 1
                }
            );

            context.SaveChanges();
        }
    }
}
