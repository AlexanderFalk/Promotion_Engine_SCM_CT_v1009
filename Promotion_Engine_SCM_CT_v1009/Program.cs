using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Promotion_Engine_SCM_CT_v1009.Core.Business;
using Promotion_Engine_SCM_CT_v1009.Core.DataAccess;
using Promotion_Engine_SCM_CT_v1009.Core.Database;
using Promotion_Engine_SCM_CT_v1009.Core.Interfaces;
using Promotion_Engine_SCM_CT_v1009.Utilities;

namespace Promotion_Engine_SCM_CT_v1009
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<PromotionEngineDbContext>(options => options.UseInMemoryDatabase(databaseName: "PromotionEngine"))
                .AddTransient<ICartBuilder, CartBuilder>()
                .AddTransient<IPromotionEngine, PromotionEngine>()
                .BuildServiceProvider();

            DataGenerator.Generate(serviceProvider);

            var checkDatabase = serviceProvider.GetRequiredService<PromotionEngineDbContext>();
            var result = checkDatabase.PromotionTypes.ToList();

            foreach (var pt in result)
            {
                Console.WriteLine(pt.PromotionTypeId + " - " + pt.Cost);
            }
        }
    }
}
