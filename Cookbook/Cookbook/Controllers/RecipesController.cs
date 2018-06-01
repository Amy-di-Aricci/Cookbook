using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cookbook.Models;

namespace Cookbook.Controllers
{
    public class RecipesController : Controller
    {
        private IDbContext db;
        public RecipesController():base()
        {
            db = new RecipeDBContext();
        }

        public RecipesController(IDbContext context):base()
        {
            db = context;
        }

        // GET: Recipes
        public ActionResult Index()
        {
            return View(db.Set<Recipe>().ToList());
        }

        // GET: Recipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Set<Recipe>().FirstOrDefault(p => p.RecipeId == id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View("Details", recipe);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecipeId,Name,Description,Difficulty")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                recipe.PublishDate = DateTime.Now;
                db.Set<Recipe>().Add(recipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Set<Recipe>().FirstOrDefault(p => p.RecipeId == id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipeId,Name,Description,Difficulty,PublishDate")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                Recipe tempRecipe = db.Set<Recipe>().First(a => a.RecipeId == recipe.RecipeId);
                tempRecipe.Name = recipe.Name;
                tempRecipe.Description = recipe.Description;
                tempRecipe.Difficulty = recipe.Difficulty;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Set<Recipe>().Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe recipe = db.Set<Recipe>().FirstOrDefault(p => p.RecipeId == id);
            db.Set<Recipe>().Remove(recipe);
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
