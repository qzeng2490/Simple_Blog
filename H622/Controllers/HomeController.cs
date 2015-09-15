using H622.DAL;
using H622.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using PagedList;
namespace H622.Controllers
{
    public class HomeController : Controller
    {
        IBlogRepository _repo;
        public HomeController(IBlogRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Tag(string tagname,int ?page)
        {
            var post = _repo.getTags().Where(t => t.name == tagname)
                                      .Single()
                                      .posts
                                      .AsQueryable()
                                      .OrderByDescending(p =>p.CreateTime)
                                      .ToPagedList(page??1,3);
            return View(post);
        }
        public ActionResult Post(int? year,int? month,string title)
        {
            if (year == null || month == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = _repo.getPosts()
                            .Where(p => p.Urlslug == title && 
                            p.CreateTime.Value.Year == year && p.CreateTime.Value.Month == month)
                            .Single();
                            
            return View(post);
        }
        public ActionResult Index(string searchstring, string currentFilter,int ?page)
        {
            
            if (searchstring != null)
            {
                page = 1;
            }
            else {
                searchstring = currentFilter;
            }
            ViewBag.CurrentFilter = searchstring;
            var posts = _repo.getPosts()
                             .Where(p => searchstring == null ||p.Tags.Any(t => t.name.Contains(searchstring)) ||p.Title.Contains(searchstring.Trim()))
                             .ToList()
                             .ToPagedList(page??1,3);
               
            return View(posts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [ChildActionOnly]
        public PartialViewResult LatestPost()
        {
            
            var model = _repo.getPosts().Take(8).ToList();

            return PartialView("_LatestPosts",model);
        }
    }
}