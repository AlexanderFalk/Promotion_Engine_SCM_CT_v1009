using System.Collections.Generic;

namespace Promotion_Engine_SCM_CT_v1009.Models
{
    /// <summary>
    /// The Cart contains the chosen SKUs by the user. If a Promotion can be applied
    /// based on the SKUs chosen, it will be applied.
    /// Only one Promotion can be active.
    /// </summary>
    public class Cart
    {
        public List<SKUEnum> SKUs { get; set; }
        public bool IsPromotionUsed { get; set; } = false;
        public PromotionType ActivePromotion { get; set; }


        public virtual PromotionType PromotionType { get; set; }
    }
}
