using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChefsAndDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsAndDishes.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Chef> allChefs = dbContext.Chefs
                .Include(chef => chef.CreatedDishes)
                .ToList();
            ViewBag.allChefs = allChefs;
            return View("Index");
        }

        [HttpGet("dishes")]
        public IActionResult AllDishes()
        {
            List<Dish> allDishes = dbContext.Dishes
                .Include(dish => dish.Creator)
                .ToList();
            ViewBag.allDishes = allDishes;
            return View("Dishes");
        }

        [HttpGet("new")]
        public IActionResult AddChefPage()
        {
            return View("AddChef");
        }

        // POST ROUTE FOR ADD CHEF
        [HttpPost("new")]
        public IActionResult CreateChef(Chef newChef)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newChef);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("AddChef");
        }

        [HttpGet("dishes/new")]
        public IActionResult AddDishPage()
        {
            List<Chef> allChefs = dbContext.Chefs.ToList();
            ViewBag.allChefs = allChefs;
            return View("AddDish");
        }

        // POST ROUTE FOR ADD DISH
        [HttpPost("dishes/new")]
        public IActionResult CreateDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("AllDishes");
            }
            List<Chef> allChefs = dbContext.Chefs.ToList();
            ViewBag.allChefs = allChefs;
            return View("AddDish");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
