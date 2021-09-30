using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XShop.Model.Models;
using XShop.Model.ViewModels;

namespace XShop.Model.Contracts
{
    public interface IOrderService
    {
        void CreateOrder(Order basedOrder, List<BasketItemViewModel> basketItems);
        List<Order> GetOrderList();
        Order GetOrder(string Id);
        void UpdateOrder(Order updateOrder);
    }
}
