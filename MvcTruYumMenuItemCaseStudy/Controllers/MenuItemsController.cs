using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTruYumMenuItemCaseStudy.Models;
using System.Data.Entity;

namespace MvcTruYumMenuItemCaseStudy.Controllers
{
    public class MenuItemsController : Controller
    {
        TruYumContext context = new TruYumContext();
        // GET: MenuItems
        public ActionResult Index()
        {
            return View();
        }
   
        public ActionResult MenuItemAdmin(bool isAdmin=false)
        {
            if (isAdmin == false)
            {
                return Content("not an admin");

            }
            else
            {
                return View();
            }
        }
        

        public ActionResult MenuItemCustomer(bool isAdmin = false)
        {
            if (isAdmin == false)
            {
                return View("ViewMenuItem", context); ;

            }
            else
            {
                return Content("You are an admin");
            }
        }

        [HttpGet]
        public ActionResult CreateMenuItem()
        {
            List<string> categories = new List<string> { "Main Course", "Starters", "Snack" };
            ViewBag.Categories = new SelectList(categories);
            return View();
        }

        [HttpPost]
        public ActionResult CreateMenuItem(MenuItem modelMenuItem)
        {
            
            
            List<string> categories = new List<string> { "Main Course", "Starters", "Snack" };
            ViewBag.Categories = new SelectList(categories);
            
            context.MenuItems.Add(modelMenuItem);
            context.SaveChanges();
            return View("ViewMenuItem",context);
        }
        public ActionResult ViewMenuItem()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditMenuItem()
        {
            List<string> categories = new List<string> { "Main Course", "Starters", "Snack" };
            ViewBag.Categories = new SelectList(categories);

            return View();
        }


        [HttpPost]
        public ActionResult EditMenuItem(MenuItem modelMenuItem)
        {
            List<string> categories = new List<string> { "Main Course", "Starters", "Snack" };
            ViewBag.Categories = new SelectList(categories);




            
            var Query = from mentItem in context.MenuItems.Include(m => m.Category)
                        where mentItem.Name == modelMenuItem.Name
                        select mentItem;
            

            foreach (var item in Query.ToList())
            {
               // Console.WriteLine("{0} {1} {2} {3} {4}", item.Name, item.Price.ToString(),
                 //   item.dateOfLaunch.ToString(), item.Active.ToString(), item.freeDelivery.ToString());


                Boolean myparameter = true;
                while (myparameter)
                {
                    if (item.Name != modelMenuItem.Name)
                    {
                        Console.WriteLine("Incorrect menu item.  Please check");
                    }
                    else
                    {
                        
                        if ((item.Price != modelMenuItem.Price)||(item.dateOfLaunch!=modelMenuItem.dateOfLaunch)||(item.Active!=modelMenuItem.Active))
                        {
                            
                            item.Price = modelMenuItem.Price;
                            item.Active = modelMenuItem.Active;
                            item.dateOfLaunch = modelMenuItem.dateOfLaunch;
                            myparameter = false;
                            
                            context.SaveChanges();
                            
                        }
                        
                    }


                }




            }



            return View("ViewMenuItem", context);
        }
        public ActionResult Customer()
        {
            return View();
        }
    }
}