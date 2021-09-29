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
using System.Security.Principal;

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
            IRepository<Order> orders = new MockContext<Order>();
            IRepository<Customer> customers = new MockContext<Customer>();

            var httpContext = new MockHtttpContext();

            IBasketService basketService = new BasketService(items, baskets);
            IOrderService orderService = new OrderService(orders);
            var controller = new BasketController(basketService, orderService, customers);
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
            IRepository<Order> orders = new MockContext<Order>();
            IRepository<Customer> customers = new MockContext<Customer>();

            items.Insert(new Item() { Id = "1", Price = 10.00m });
            items.Insert(new Item() { Id = "2", Price = 100.00m });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ItemId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ItemId = "2", Quantity = 3 });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(items, baskets);
            IOrderService orderService = new OrderService(orders);
            var controller = new BasketController(basketService, orderService, customers);

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

        [TestMethod]
        public void CanCheckOutAndCreateOrder()
        {
            //Arrange
            IRepository<Customer> customers = new MockContext<Customer>();
            IRepository<Item> items = new MockContext<Item>();
            items.Insert(new Item() { Id = "1", Price = 1.00m });
            items.Insert(new Item() { Id = "2", Price = 2.00m });

            IRepository<Basket> baskets = new MockContext<Basket>();
            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ItemId = "1", Quantity = 2, BasketId = basket.Id });
            basket.BasketItems.Add(new BasketItem() { ItemId = "2", Quantity = 1, BasketId = basket.Id });

            baskets.Insert(basket);

            IBasketService basketService = new BasketService(items, baskets);

            IRepository<Order> orders = new MockContext<Order>();
            IOrderService orderService = new OrderService(orders);

            customers.Insert(new Customer() { Id = "1", Email = "what.ever@mail.com", ZipCode="66669" });
            IPrincipal FakeUser = new GenericPrincipal(new GenericIdentity("what.ever@mail.com", "Forms"), null);

            var controller = new BasketController(basketService, orderService, customers);
            var httpContext = new MockHtttpContext();
            httpContext.User = FakeUser;
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket")
            {
                Value = basket.Id
            });

            controller.ControllerContext = new ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            //Act
            Order order = new Order();
            controller.Checkout(order);

            //Assert
            Assert.AreEqual(2, order.OrderItems.Count);
            Assert.AreEqual(0, basket.BasketItems.Count);

            Order orderInRep = orders.Find(order.Id);
            Assert.AreEqual(2, orderInRep.OrderItems.Count);
        }
    }
}
