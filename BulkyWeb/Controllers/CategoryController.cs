﻿using Bulky.Models;
using BulkyWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();          
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {      
            if (ModelState.IsValid)
            {
                if (obj.Name.ToLower() == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError(String.Empty, "The DisplayOrder and Name cannot be equals");                  
                }
                else
                {
                    _db.Categories.Add(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Category created successfully";
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? categoryFromDb = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Name.ToLower() == obj.Name.ToString())
                {
                    ModelState.AddModelError(String.Empty, "The DisplayOrder and Name cannot be equals");
                }
                else
                {
                    _db.Categories.Update(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Category updated successfully";
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(categoryFromDb);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
