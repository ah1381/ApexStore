using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApexStore.Application.Services.Orders.Queries.GetOrdersForAdmin;
using ApexStore.Domain.Entities.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Store.Site.Areas.Admin.Controllers
{
    [Area("admin")]
    //[Authorize(Roles ="Admin")]
    public class OrdersController : Controller
    {
        private readonly IGetOrdersForAdminService _getOrdersForAdminService;
        public OrdersController(IGetOrdersForAdminService getOrdersForAdminService)
        {
            _getOrdersForAdminService = getOrdersForAdminService;
        }
        public IActionResult Index(OrderState orderState)
        {
            return View(_getOrdersForAdminService.Execute(orderState).Data);
        }
    }
}
