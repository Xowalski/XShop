using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XShop.DataAccess.Memory;
using XShop.Model.Models;

namespace XShop.WebUI.Controllers
{
    public class ItemCategoryManagerController : Controller
    {
        MemoryRepository<ItemCategory> context;

        public ItemCategoryManagerController()
        {
            context = new MemoryRepository<ItemCategory>();
        }

        // GET: ItemManager
        public ActionResult Index()
        {
            List<ItemCategory> itemCategories = context.Collection().ToList();
            return View(itemCategories);
        }

        public ActionResult Create()
        {
            ItemCategory itemCategory = new ItemCategory();
            return View(itemCategory);
        }

        [HttpPost]
        public ActionResult Create(ItemCategory itemCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(itemCategory);
            }
            else
            {
                context.Insert(itemCategory);
                context.Commit();

                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Edit(string Id)
        {
            ItemCategory itemCategory = context.Find(Id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(itemCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ItemCategory itemCategory, string Id)
        {
            ItemCategory itemCategoryToEdit = context.Find(Id);

            if (itemCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(itemCategory);
                }

                itemCategoryToEdit.Category = itemCategory.Category;

                context.Commit();

                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Delete(string Id)
        {
            ItemCategory itemCategoryToDelete = context.Find(Id);

            if (itemCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(itemCategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ItemCategory itemCategoryToDelete = context.Find(Id);

            if (itemCategoryToDelete == null)
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