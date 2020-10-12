using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Promotion_Engine_SCM_CT_v1009.Utilities;

namespace Promotion_Engine_SCM_CT_v1009.Models
{
    public class PromotionType
    {
        [Key]
        public string PromotionTypeId { get; set; } = "ptype_" + PrimaryKeyGenerator.Generate();
        public double Cost { get; set; }
        public string PromotionDiscountId { get; set; }

        public virtual PromotionDiscount Discount { get; set; }
        public virtual List<Promotion> Promotions { get; set; }
    }
}
