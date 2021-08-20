using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XShop.Model.Models
{
    public class ItemCategory
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public ItemCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
