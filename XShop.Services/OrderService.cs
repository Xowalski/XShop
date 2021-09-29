using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XShop.Model.Contracts;
using XShop.Model.Models;
using XShop.Model.ViewModels;

namespace XShop.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Order> orderContext;
        public OrderService(IRepository<Order> OrderContext)
        {
            this.orderContext = OrderContext;
        }

        public void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach (var item in basketItems)
            {
                baseOrder.OrderItems.Add(new OrderItem()
                {
                    ItemId = item.Id,
                    ItemName = item.ItemName,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Image = item.Image
                });
            }

            orderContext.Insert(baseOrder);
            orderContext.Commit();

        }
    }
}
