using System.ComponentModel.DataAnnotations;
using Promotion_Engine_SCM_CT_v1009.Utilities;

namespace Promotion_Engine_SCM_CT_v1009.Models
{
    public class Promotion
    {
        [Key]
        public string PromotionId { get; set; } = "prom_" + PrimaryKeyGenerator.Generate();
        public string PromotionTypeId { get; set; }
        public SKUEnum SKU { get; set; }
        public short Count { get; set; }

        public virtual PromotionType PromotionTypes { get; set; }
        public virtual SKUEnum SKUNavigationProperty { get; }
    }
}
