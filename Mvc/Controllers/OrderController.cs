using System.Collections.Generic;
using System.Diagnostics;
using Domain.Dtos.Client;
using Domain.Dtos.Order;
using Domain.Wrapper;
using Infrastructure.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

public class OrderController : Controller
{
   private readonly IOrderService _orderService;

   public OrderController(IOrderService orderService)
   {
      _orderService = orderService;
   }


   [HttpGet]
   public async Task<IActionResult> Index()
   {
      var result = await _orderService.GetListOfOrders();

      var orders = result.Data.ToList();

      return View(orders);
   }

   [HttpGet]
   public IActionResult Create()
   {
      return View(new AddOrder());
   }

   
   [HttpPost]
   public IActionResult Create(AddOrder addOrder)
   {
      if (ModelState.IsValid)
      {
         _orderService.AddOrder(addOrder);
         return RedirectToAction("Index");
      }
      return View(addOrder);
   }


   
   [HttpGet]
   public async Task<IActionResult> Update(int id)
   {
      var existing = await _orderService.GetOrderById(id);
      
      var order = existing.Data;

        var result = (new AddOrder()
        {
            Id = order.Id,
            ClientId = order.ClientId,
            CorniceWidth=order.CorniceWidth,
            SewingType=order.SewingType,
            Height=order.Height,
            MaterialWidth=order.MaterialWidth,
            Description=order.Description,
            PriceOfOneMetr=order.PriceOfOneMetr,
            ClientName=order.ClientName,
        });
      return View(result); 
   }

   [HttpPost]
   public async Task<IActionResult> Update(AddOrder addOrder)
   {
      if (ModelState.IsValid)
      {
        await _orderService.UpdateOrder(addOrder);
         return RedirectToAction("Index");
      }
      return View(addOrder);
   }

   public async Task<ActionResult> Delete(int id)
   {
      await _orderService.DeleteOrder(id);
      return RedirectToAction("Index");
   }
}