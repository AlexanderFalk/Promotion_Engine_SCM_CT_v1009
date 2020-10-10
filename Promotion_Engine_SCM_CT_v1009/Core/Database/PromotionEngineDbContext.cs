using Microsoft.EntityFrameworkCore;
using Promotion_Engine_SCM_CT_v1009.Models;

namespace Promotion_Engine_SCM_CT_v1009.Core.Database
{
    public class PromotionEngineDbContext : DbContext
    {
        public PromotionEngineDbContext(DbContextOptions<PromotionEngineDbContext> options)
            : base(options)
        {
        }

        public DbSet<PromotionType> PromotionTypes { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionDiscount> PromotionDiscounts { get; set; }
    }
}
