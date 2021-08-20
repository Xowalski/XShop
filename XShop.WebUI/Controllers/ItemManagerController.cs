﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XShop.Model.Models;
using XShop.DataAccess.Memory;
using XShop.Model.ViewModels;

namespace XShop.WebUI.Controllers
{
    public class ItemManagerController : Controller
    {
        ItemRepository context;
        ItemCategoryRepository itemCategories;

        public ItemManagerController()
        {
            context = new ItemRepository();
            itemCategories = new ItemCategoryRepository();
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
        public ActionResult Create(Item item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }
            else
            {
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
        public ActionResult Edit(Item item, string Id)
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

                itemToEdit.Name = item.Name;
                itemToEdit.Category = item.Category;
                itemToEdit.Image = item.Image;
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