namespace Promotion_Engine_SCM_CT_v1009.Models.DTO
{
    /// <summary>
    /// This class is a Data Transfer Object used to pass around promotion data
    /// </summary>
    public class PromotionDTO
    {
        public PromotionType PromotionType { get; set; }
        public Promotion Promotion { get; set; }
        public PromotionDiscount PromotionDiscount { get; set; }
    }
}
