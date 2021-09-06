using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XShop.Model.Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public DateTimeOffset CreationTime {get; set;}

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreationTime = DateTime.Now;
        }
    }
}
