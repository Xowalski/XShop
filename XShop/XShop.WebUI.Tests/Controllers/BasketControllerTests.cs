using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using XShop.Model.Contracts;
using XShop.Model.Models;
using XShop.WebUI.Tests.Mocks;
using XShop.Services;
using System.Linq;
using XShop.WebUI.Controllers;
using System.Web.Mvc;
using XShop.Model.ViewModels;

namespace XShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTests
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            //Arrange
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Item> items = new MockContext<Item>();

            var httpContext = new MockHtttpContext();

            IBasketService basketService = new BasketService(items, baskets);
            var controller = new BasketController(basketService);
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            //basketService.AddToBasket(httpContext, "1");
            controller.AddToBasket("1");

            Basket basket = baskets.Collection().FirstOrDefault();

            //Assert
            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count);
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ItemId);
        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            //Arrange
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Item> items = new MockContext<Item>();

            items.Insert(new Item() { Id = "1", Price = 10.00m });
            items.Insert(new Item() { Id = "2", Price = 100.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ItemId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ItemId = "2", Quantity = 3 });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(items, baskets);

            var controller = new BasketController(basketService);
            var httpContext = new MockHtttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            //Assert
            Assert.AreEqual(5, basketSummary.BasketCount);
            Assert.AreEqual(320, basketSummary.BasketTotal);
        }
    }
}
