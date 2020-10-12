using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Promotion_Engine_SCM_CT_v1009.Core.Business;
using Promotion_Engine_SCM_CT_v1009.Core.DataAccess;
using Promotion_Engine_SCM_CT_v1009.Core.Database;
using Promotion_Engine_SCM_CT_v1009.Core.Interfaces;
using Promotion_Engine_SCM_CT_v1009.Models;
using Promotion_Engine_SCM_CT_v1009.Utilities;

namespace Promotion_Engine_SCM_CT_v1009_Tests
{
    [TestClass]
    public class CartTests
    {
        private static IServiceProvider serviceProvider;

        [ClassInitialize]
        public static void AssemblyInit(TestContext testContext)
        {
            // Called before every test method.
            serviceProvider = new ServiceCollection()
                .AddDbContext<PromotionEngineDbContext>(options => options.UseInMemoryDatabase(databaseName: "PromotionEngine"))
                .AddTransient<ICartBuilder, CartBuilder>()
                .AddTransient<IPromotionEngine, PromotionEngine>()
                .BuildServiceProvider();

            DataGenerator.Generate(serviceProvider);
        }

        [TestMethod]
        public void Add_SKU_To_Cart_And_Expect_Size_To_Be_One_Test()
        {
            // Arrange
            var expectedSize = 1;
            var cart = serviceProvider.GetRequiredService<ICartBuilder>();

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
            var cart = serviceProvider.GetRequiredService<ICartBuilder>();
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
            var cart = serviceProvider.GetRequiredService<ICartBuilder>();

            // Act
            cart.Add(SKUEnum.A);
            cart.Add(SKUEnum.A);
            cart.Add(SKUEnum.A);

            // Assert
            Assert.AreEqual(expectedSize, cart.Size());
            Assert.IsTrue(cart.GetCart().IsPromotionUsed);
        }

        [TestMethod]
        public void Add_Three_SKU_A_And_Expect_TotalCost_Of_150_Test()
        {
            // Arrange
            var expectedTotalCost = 150.0;
            var cart = serviceProvider.GetRequiredService<ICartBuilder>();

            // Act
            cart.Add(SKUEnum.A);
            cart.Add(SKUEnum.C);
            cart.Add(SKUEnum.D);

            // Assert
            Assert.AreEqual(expectedTotalCost, cart.GetTotalCost());
            Assert.IsTrue(cart.GetCart().IsPromotionUsed);
        }
    }
}
