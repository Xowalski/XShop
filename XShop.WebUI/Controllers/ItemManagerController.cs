using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XShop.Model.Models;
using XShop.DataAccess.Memory;
using XShop.Model.ViewModels;
using XShop.Model.Contracts;
using System.IO;

namespace XShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ItemManagerController : Controller
    {
        IRepository<Item> context;
        IRepository<ItemCategory> itemCategories;

        public ItemManagerController(IRepository<Item> contextItem, IRepository<ItemCategory> contextItemCategory)
        {
            context = contextItem;
            itemCategories = contextItemCategory;
        }

        // GET: ItemManager
        public ActionResult Index()
        {
            List<Item> items = context.Collection().ToList();
            return View(items);
        }

        public ActionResult Create()
        {
            ItemManagerViewModel viewModel = new ItemManagerViewModel();

            viewModel.Item = new Item();
            viewModel.ItemCategories = itemCategories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Item item, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }
            else
            {
                if (file != null)
                {
                    item.Image = item.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ItemImages//") + item.Image);
                }
                context.Insert(item);
                context.Commit();
                return RedirectToAction(nameof(Index)); //todo nameof albo cudzysłów - uspójnić
            }
        }

        public ActionResult Edit(string Id)
        {
            Item item = context.Find(Id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                ItemManagerViewModel viewModel = new ItemManagerViewModel();
                viewModel.Item = item;
                viewModel.ItemCategories = itemCategories.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Item item, string Id, HttpPostedFileBase file)
        {
            Item itemToEdit = context.Find(Id);

            if (itemToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(item);
                }

                if (file != null)
                {
                    itemToEdit.Image = item.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ItemImages//") + itemToEdit.Image);
                }

                itemToEdit.Name = item.Name;
                itemToEdit.Category = item.Category;
                itemToEdit.Description = item.Description;
                itemToEdit.Price = item.Price;

                context.Commit();

                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Delete(string Id)
        {
            Item itemToDelete = context.Find(Id);
            if (itemToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(itemToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Item itemToDelete = context.Find(Id);

            if (itemToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction(nameof(Index));
            }
        }

    }
}