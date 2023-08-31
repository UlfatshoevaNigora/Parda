using System.Collections.Generic;
using System.Diagnostics;
using Domain.Dtos.Client;
using Domain.Wrapper;
using Infrastructure.Services.ClientService;
using Microsoft.AspNetCore.Mvc;

public class ClientController : Controller
{
   private readonly IClientService _clientService;

   public ClientController(IClientService clientService)
   {
      _clientService = clientService;
   }


   [HttpGet]
   public async Task<IActionResult> Index(string? name)
   {
      var result = await _clientService.GetListOfClients(name);

      var clients = result.Data.ToList();

      return View(clients);
   }
   

   [HttpGet]
   public IActionResult Create()
   {
      return View(new AddClient());
   }

   
   [HttpPost]
   public IActionResult Create(AddClient addClient)
   {
      if (ModelState.IsValid)
      {
         _clientService.AddClient(addClient);
         return RedirectToAction("Index");
      }
      return View(addClient);
   }


   [HttpGet]
   public async Task<IActionResult> Update(int id)
   {
      var existing = await _clientService.GetClientById(id);
      
      var client = existing.Data;

        var result = (new AddClient()
        {
            Id = client.Id,
            Name = client.Name,
        });
      return View(result); 
   }

   [HttpPost]
   public async Task<IActionResult> Update(AddClient addClient)
   {
      if (ModelState.IsValid)
      {
        await _clientService.UpdateClient(addClient);
         return RedirectToAction("Index");
      }
      return View(addClient);
   }

   public async Task<ActionResult> Delete(int id)
   {
      await _clientService.DeleteClient(id);
      return RedirectToAction("Index");
   }



   
   [HttpGet]
   public async Task<IActionResult> GetClientById(int id)
   {
      var existing = await _clientService.GetClientById(id);
      
      var client = existing.Data;

      var result = (new GetClient()
      {
         Id = client.Id,
         Name = client.Name,
         OrdersOfClient = client.OrdersOfClient,
        });
      return View(result); 
   }
}