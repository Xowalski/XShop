using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XShop.Model.Models;

namespace XShop.Model.ViewModels
{
    public class ItemManagerViewModel
    {
        public Item Item { get; set; }
        public IEnumerable<ItemCategory> ItemCategories { get; set; }
    }
}
