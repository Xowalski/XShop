using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using XShop.Models;

namespace XShop.DataAccess.Memory
{
    public class ItemRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Item> items = new List<Item>();

        public ItemRepository()
        {
            items = cache[nameof(items)] as List<Item>;
            if (items == null)
            {
                items = new List<Item>();
            } 
        }

        public void Commit()
        {
            cache[nameof(items)] = items;
        }

        public void Insert(Item item)
        {
            items.Add(item);
        }

        public void Update(Item item)
        {
            Item itemToUpdate = items.Find(i => i.Id == item.Id);

            if (itemToUpdate != null)
            {
                itemToUpdate = item;
            }
            else
            {
                throw new Exception("Item not found");
            }
        }

        public Item Find(string Id)
        {
            Item item = items.Find(i => i.Id == Id);

            if (item != null)
            {
                return item;
            }
            else
            {
                throw new Exception("Item not found");
            }
        }

        public IQueryable<Item> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            Item itemToDelete = items.Find(i => i.Id == Id);

            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);
            }
            else
            {
                throw new Exception("Item not found");
            }
        }
    }
}
