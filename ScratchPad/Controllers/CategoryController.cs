﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScratchPad.Models;

namespace ScratchPad.Controllers
{
    public class CategoryController : Controller
    {
        public EFDBFirstDatabaseEntities DatabaseOperation()
        {
            var db = new EFDBFirstDatabaseEntities();
            return db;
        }
        // GET: Category
        public ActionResult Index()
        {
            var db = DatabaseOperation();
            List<Category> categories = db.Categories.ToList();
            return View(categories);
        }
    }
}