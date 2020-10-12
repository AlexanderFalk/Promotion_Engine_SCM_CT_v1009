using System.Collections.Generic;
using Promotion_Engine_SCM_CT_v1009.Models;

namespace Promotion_Engine_SCM_CT_v1009.Core.Interfaces
{
    public interface IPromotionEngine
    {
        /// <summary>
        /// Using this method, you are able to add one or more of a SKU to a cart
        /// </summary>
        /// <param name="sku">The SKU to be added</param>
        /// <param name="count">The number of SKUs needed. Default is 1</param>
        void AddSkuToCart(SKUEnum sku, int count = 1);

        /// <summary>
        /// Deletes one SKU from the cart
        /// </summary>
        /// <param name="sku">The SKU to be deleted</param>
        void DeleteSkuFromCart(SKUEnum sku);

        /// <summary>
        /// Gets the price of a SKU
        /// </summary>
        /// <param name="sku">The SKU to retrieve the price for</param>
        /// <returns>The price of the specified SKU</returns>
        double GetSkuPrice(SKUEnum sku);

        /// <summary>
        /// Gets the price of a SKU
        /// </summary>
        /// <param name="sku">The SKU to retrieve the price for</param>
        /// <returns>The price of the specified SKU</returns>
        List<PromotionType> GetPromotions();
    }
}
