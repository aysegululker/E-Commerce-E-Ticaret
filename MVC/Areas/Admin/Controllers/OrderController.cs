using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.Models.ViewModels;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly UserManager<AppUser> userManager;

        public OrderController(IOrderService orderService, UserManager<AppUser> userManager)
        {
            this.orderService = orderService;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            OrderVM orderVM = new OrderVM();
            orderVM.Orders = orderService.GetOrders();
            orderVM.OrderDetails = orderService.GetOrderDetail();
            return View(orderVM);
        }

        public IActionResult Confirm(Guid id)
        {
            var order = orderService.GetById(id);
            order.Confirmed = true;
            orderService.Update(order);
            return RedirectToAction("Index");
        }
    }
}