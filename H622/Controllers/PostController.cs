using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using H622.DAL;
using H622.Models;
using System.Text.RegularExpressions;
using PagedList;
using System.Collections;

namespace H622.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private BlogContext db;
        IBlogRepository _repo;
        public PostController(IBlogRepository repo, BlogContext _db) {
            _repo = repo;
            db = _db;
        }
        // GET: Post
        public ActionResult Index(int? page)
        {
            
            return View(db.Posts.ToList().ToPagedList(page ?? 1, 5));
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ID,Title,Content")] Post post,string Tags)
        {
            if (ModelState.IsValid)
            {
                
                post.CreateTime = DateTime.UtcNow;
                post.LastModified = DateTime.UtcNow;
                post.Urlslug = new Regex("[^a-zA-Z0-9\u4e00-\u9fa5]+").Replace(post.Title, "-");
                //post.Tags.Add()
                foreach (var t in Tags.Split(',')) {
                    var s = t.Trim();
                    Tag curTag;
                    if (!_repo.getTags().Any(p => p.name == s))
                    {
                        curTag = new Tag { name = s };
                        db.Tags.Add(curTag);
                    }
                    else {
                        curTag = _repo.getTags().Where(x => x.name == s).Single();
                    }
                    if (curTag.posts == null)
                    {
                        curTag.posts = new List<Post>();
                    }
                    if (post.Tags == null) {
                        post.Tags = new List<Tag>();
                    }
                    curTag.posts.Add(post);
                    post.Tags.Add(curTag); 
                }
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,CreateTime")] Post post,string Tags)
        {
            if (ModelState.IsValid)
            {
                Post prev = db.Posts.Find(post.ID);
                post.CreateTime = post.CreateTime;
                post.LastModified = DateTime.UtcNow;
                post.Urlslug = new Regex("[^a-zA-Z0-9\u4e00-\u9fa5]+").Replace(post.Title, "-");

                //remove original tags from database
                foreach (var t in prev.Tags.ToList())
                {
                    if (t.posts.Count == 1)
                    {
                        db.Tags.Remove(t);
                    }
                    else if (t.posts.Count > 1)
                    {
                        db.Tags.Find(t.name).posts.Remove(prev);
                    }
                }
                db.Posts.Remove(prev);
                db.SaveChanges();
                foreach (var t in Tags.Split(','))
                {
                    var s = t.Trim();
                    Tag curTag;
                    if (!_repo.getTags().Any(p => p.name == s))
                    {
                        curTag = new Tag { name = s };
                        db.Tags.Add(curTag);
                    }
                    else
                    {
                        curTag = _repo.getTags().Where(x => x.name == s).Single();
                        db.Entry(curTag).State = EntityState.Modified;
                    }
                    if (curTag.posts == null)
                    {
                        curTag.posts = new List<Post>();
                    }
                    if (post.Tags == null)
                    {
                        post.Tags = new List<Tag>();
                    }
                    curTag.posts.Add(post);
                    post.Tags.Add(curTag);
                }
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
