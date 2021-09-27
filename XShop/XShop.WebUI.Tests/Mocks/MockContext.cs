using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XShop.Model.Contracts;
using XShop.Model.Models;

namespace XShop.WebUI.Tests.Mocks
{
    public class MockContext<T> : IRepository<T> where T: BaseEntity
    {
        List<T> items;
        string className;

        public MockContext()
        {
            items = new List<T>();
        }

        public void Commit()
        {
            return;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T toUpdate = items.Find(i => i.Id == t.Id);

            if (toUpdate != null)
            {
                toUpdate = t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public T Find(string Id)
        {
            T t = items.Find(i => i.Id == Id);

            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T toDelete = items.Find(i => i.Id == Id);

            if (toDelete != null)
            {
                items.Remove(toDelete);
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
    }
}