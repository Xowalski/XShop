using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XShop.Model.Contracts;
using XShop.Model.Models;
using XShop.Model.ViewModels;

namespace XShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Item> context;
        IRepository<ItemCategory> itemCategories;

        public HomeController(IRepository<Item> contextItem, IRepository<ItemCategory> contextItemCategory)
        {
            context = contextItem;
            itemCategories = contextItemCategory;
        }

        public ActionResult Index(string Category = null)
        {
            List<Item> items;
            List<ItemCategory> categories = itemCategories.Collection().ToList();

            if (Category == null)
            {
                items = context.Collection().ToList();
            }
            else
            {
                items = context.Collection().Where(p => p.Category == Category).ToList();
            }

            ItemListViewModel viewModel = new ItemListViewModel();
            viewModel.Items = items;
            viewModel.ItemCategories = categories;

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details(string Id)
        {
            Item item = context.Find(Id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(item);
            }
        }
    }
}