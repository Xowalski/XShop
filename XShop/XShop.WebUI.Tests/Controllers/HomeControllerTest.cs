using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using XShop.Model.Contracts;
using XShop.Model.Models;
using XShop.Model.ViewModels;
using XShop.WebUI;
using XShop.WebUI.Controllers;

namespace XShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexPageDoesReturnItems()
        {
            //Arrange
            IRepository<Item> itemContext = new Mocks.MockContext<Item>();
            IRepository<ItemCategory> itemCategoryContext = new Mocks.MockContext<ItemCategory>();

            itemContext.Insert(new Item());

            HomeController homeController = new HomeController(itemContext, itemCategoryContext);

            //Act
            var result = homeController.Index() as ViewResult;
            var viewModel = (ItemListViewModel)result.ViewData.Model;

            //Assert
            Assert.AreEqual(1, viewModel.Items.Count());
        }
    }
}
 