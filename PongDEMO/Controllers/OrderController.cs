using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PongDEMO.Models;

namespace PongDEMO.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult MockOrder()
        {
            Order o = new Order();
            o.Id = 1;
            o.UserName = "Jon Snow";
            o.UserMail = "Jon@watch.wst";
            o.Adress = "Castle Black, 123 North";
            o.ProductName = "Dragon";
            o.Color = "Brown";
            o.DeliveryMethod = "DHL";
            o.Price = 10000;

            Order o2 = new Order();
            o2.Id = 2;
            o2.UserName = "Jaime L";
            o2.UserMail = "J@kingguard";
            o2.Adress = "Kings Landing, 321 KL";
            o2.ProductName = "Hand";
            o2.Color = "Gold";
            o2.DeliveryMethod = "InPost";
            o2.Price = 50000;

            Order o3 = new Order();
            o3.Id = 2;
            o3.UserName = "The Imo";
            o3.UserMail = "J@drinkers";
            o3.Adress = "Kings Landing, 521 KL";
            o3.ProductName = "Wine";
            o3.Color = "Red";
            o3.DeliveryMethod = "Pigeon post";
            o3.Price = 50;

            List<Order> os = new List<Order>();
            os.Add(o);
            os.Add(o2);
            os.Add(o3);

            return View("_Order", os);
        }
       
    }
}