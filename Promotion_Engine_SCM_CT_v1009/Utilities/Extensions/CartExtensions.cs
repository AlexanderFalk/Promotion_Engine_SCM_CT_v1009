using System.Text;
using Promotion_Engine_SCM_CT_v1009.Models;

namespace Promotion_Engine_SCM_CT_v1009.Utilities.Extensions
{
    /// <summary>
    /// The class contains extension methods for the Cart object
    /// </summary>
    public static class CartExtensions
    {
        /// <summary>
        /// The extension method "ShowCart" can be used to show the content of the cart.
        /// It prints the size
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public static string ShowCart(this Cart cart)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Size of Cart: {cart.SKUs.Count}\n");
            if (cart.ActivePromotion != null)           
            {
                stringBuilder.Append($"Active Promotion: {cart.ActivePromotion.PromotionTypeId} \n");
                stringBuilder.Append($"Cost: {cart.ActivePromotion.Cost}\n");
                stringBuilder.Append("\tContaining:\n");
                foreach (var promo in cart.ActivePromotion.Promotions)
                {
                    stringBuilder.Append($"\t{promo.Count}x {promo.SKU}\n");
                }
            }

            stringBuilder.Append($"Total Cost: {cart.TotalCost}");
            
            return stringBuilder.ToString();
        }
    }
}
