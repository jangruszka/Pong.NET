using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PongDEMO.Data;
using PongDEMO.Models;

namespace PongDEMO.Controllers
{
    public class LikesController : Controller
    {
        ApplicationDbContext db;
        UserManager<IdentityUser> userManager;
        public LikesController(ApplicationDbContext _db, UserManager<IdentityUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }

        //CORE METHOD
        public IActionResult LikesCoreGet(int itemKey)
        {
            Likes l = db.Likes.Where(x => x.Id == itemKey).FirstOrDefault();

            return View("_Likes", l);
        }
        //HIGHER ORDER METHOD
        public int[] GetPostLikes(int postId)
        {
            List<int> l = db.Likes.Where(x => x.PostId == postId).Select(x => x.Id).ToList();
            int[] arr = l.ToArray();
            return arr;
        }
    }
}