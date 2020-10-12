using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Promotion_Engine_SCM_CT_v1009.Core.Business;
using Promotion_Engine_SCM_CT_v1009.Core.Database;
using Promotion_Engine_SCM_CT_v1009.Models;
using Promotion_Engine_SCM_CT_v1009.Utilities;

namespace Promotion_Engine_SCM_CT_v1009_Tests
{
    public abstract class TestsBase : IDisposable
    {
        protected TestsBase()
        {
            // Called before every test method.
            var serviceProvider = new ServiceCollection()
                .AddDbContext<PromotionEngineDbContext>(options => options.UseInMemoryDatabase(databaseName: "PromotionEngineTest"))
                .BuildServiceProvider();

            DataGenerator.Generate(serviceProvider);
        }

        public void Dispose()
        {
            // Called after every test method.
        }
    }

    [TestClass]
    public class CartTests
    {

        [TestMethod]
        public void Add_SKU_To_Cart_And_Expect_Size_To_Be_One_Test()
        {
            // Arrange
            var expectedSize = 1;
            var cart = new CartBuilder();

            // Act
            cart.Add(SKUEnum.A);

            // Assert
            Assert.AreEqual(expectedSize, cart.Size());
        }

        [TestMethod]
        public void Remove_SKU_Fron_Cart_And_Expect_Size_To_Be_Zero_Test()
        {
            // Arrange
            var expectedSizeAdd = 1;
            var expectedSizeRemove = 0;
            var cart = new CartBuilder();
            cart.Add(SKUEnum.B);
            // Act
            Assert.AreEqual(expectedSizeAdd, cart.Size());

            cart.Remove(SKUEnum.B);
            // Assert
            Assert.AreEqual(expectedSizeRemove, cart.Size());
        }


        [TestMethod]
        public void Add_Three_SKU_A_And_Expect_If_Promo_Applies_Test()
        {
            // Arrange
            var expectedSize = 3;
            var cart = new CartBuilder();

            // Act
            cart.Add(SKUEnum.A);
            cart.Add(SKUEnum.A);
            cart.Add(SKUEnum.A);

            // Assert
            Assert.AreEqual(expectedSize, cart.Size());
            Assert.IsTrue(cart.GetCart().IsPromotionUsed);
        }
    }
}
