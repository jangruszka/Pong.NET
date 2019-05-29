using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PongDEMO.Models;

namespace PongDEMO.Controllers
{
    public class UserCardController : Controller
    {
        public IActionResult UserCardRender(string name, string profession, string desc)
        {
            UserCard u = new UserCard();
            u.Name = name;
            u.Desc = desc;
            u.Profession = profession;

            return View("_UserCard", u);
        }
    }
}