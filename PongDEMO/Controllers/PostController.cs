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
    public class PostController : Controller
    {
       
       ApplicationDbContext db;
        UserManager<IdentityUser> userManager;
        public PostController(ApplicationDbContext _db, UserManager<IdentityUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }



        //CORE METHODS

        //1. Core Method for using data from back-end
        public IActionResult PostCoreGet(int itemKey)
        {
            Post p = db.Posts.Where(x => x.Id == itemKey).FirstOrDefault();

            return View("_Post", p);
        }

        //2. Core Method for using data from front-end
        public IActionResult PostCoreRender(string nick, string email, string content)
        {
            Post p = new Post();
            p.Nick = nick;
            p.Email = email;
            p.Content = content;
            return View("_Post", p);
        }



        //HIGHER ORDER METHOD

        public int[] GetAllPosts()
        {
            List<int> postIds = db.Posts.Select(x => x.Id).ToList();
            

            //Just mock data filling empty table
            if (postIds.Count == 0)
            {
                Post p1 = new Post();
                p1.Nick = "Jon";
                p1.Email = "jon@watch.wst";
                p1.Content = "Winter is coming";
                db.Posts.Add(p1);
                db.SaveChanges();

                Likes l1 = new Likes();
                Random r1 = new Random();
                l1.PostId = p1.Id;
                l1.Yes = r1.Next(1, 1000);
                l1.No = r1.Next(1, 1000);
                db.Likes.Add(l1);
                db.SaveChanges();

                Post p2 = new Post();
                p2.Nick = "Ygritte";
                p2.Email = "ygr@wild.wld";
                p2.Content = "You know nothing";
                db.Posts.Add(p2);
                db.SaveChanges();

                Likes l2 = new Likes();
                Random r2 = new Random();
                l2.PostId = p2.Id;
                l2.Yes = r2.Next(1, 1000);
                l2.No = r2.Next(1, 1000);
                db.Likes.Add(l2);
                db.SaveChanges();

                Post p3 = new Post();
                p3.Nick = "Tyrion";
                p3.Email = "tyr@dragons.team";
                p3.Content = "Wine, wine, wine";
                db.Posts.Add(p3);
                db.SaveChanges();

                Likes l3 = new Likes();
                Random r3 = new Random();
                l3.PostId = p3.Id;
                l3.Yes = r3.Next(1, 1000);
                l3.No = r3.Next(1, 1000);
                db.Likes.Add(l3);
                db.SaveChanges();

                postIds = db.Posts.Select(x => x.Id).ToList();
            }


            int[] arr = postIds.ToArray();
            return arr;

           



        }




        [HttpPost]
        public JsonResult AddPost(string nick, string email, string content)
        {

            Post p = new Post();
            p.Nick = nick;
            p.Email = email;
            p.Content = content;
            db.Posts.Add(p);
            db.SaveChanges();

            Likes l = new Likes();
            Random r = new Random();
            l.PostId = p.Id;
            l.Yes = r.Next(1, 1000);
            l.No = r.Next(1, 1000);
            db.Likes.Add(l);
            db.SaveChanges();

            return Json(true);
        }

        [HttpPost]
        public JsonResult EditPost(int id, string content)
        {
            Post p = db.Posts.Where(x => x.Id == id).FirstOrDefault();
            p.Content = content;
            db.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return Json(true);
        }
    }
}