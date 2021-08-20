using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using XShop.Model.Models;

namespace XShop.DataAccess.Memory
{
    public class ItemCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ItemCategory> itemCategories;

        public ItemCategoryRepository()
        {
            itemCategories = cache[nameof(itemCategories)] as List<ItemCategory>;
            if (itemCategories == null)
            {
                itemCategories = new List<ItemCategory>();
            }
        }

        public void Commit()
        {
            cache[nameof(itemCategories)] = itemCategories;
        }

        public void Insert(ItemCategory i)
        {
            itemCategories.Add(i);
        }

        public void Update(ItemCategory itemCategory)
        {
            ItemCategory itemCategoryToUpdate = itemCategories.Find(i => i.Id == itemCategory.Id);

            if (itemCategoryToUpdate != null)
            {
                itemCategoryToUpdate = itemCategory;
            }
            else
            {
                throw new Exception("Item category not found");
            }
        }

        public ItemCategory Find(string Id)
        {
            ItemCategory itemCategory = itemCategories.Find(i => i.Id == Id);

            if (itemCategory != null)
            {
                return itemCategory;
            }
            else
            {
                throw new Exception("Item category not found");
            }
        }

        public IQueryable<ItemCategory> Collection()
        {
            return itemCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ItemCategory itemCategoryToDelete = itemCategories.Find(i => i.Id == Id);

            if (itemCategoryToDelete != null)
            {
                itemCategories.Remove(itemCategoryToDelete);
            }
            else
            {
                throw new Exception("Item not found");
            }
        }
    }
}
