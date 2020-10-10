using System.ComponentModel.DataAnnotations;
using Promotion_Engine_SCM_CT_v1009.Utilities;

namespace Promotion_Engine_SCM_CT_v1009.Models
{
    public class PromotionDiscount
    {
        [Key]
        public string PromotionDiscountId { get; set; } = "pdisc_" + PrimaryKeyGenerator.Generate();
        public double Discount { get; set; }
    }
}
