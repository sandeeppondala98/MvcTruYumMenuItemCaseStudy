using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTruYumMenuItemCaseStudy.Models;
using System.Data.Entity;

namespace MvcTruYumMenuItemCaseStudy.Controllers
{
    public class CartController : Controller
    {
        TruYumContext context = new TruYumContext();
        // GET: Cart
        
        public ActionResult Index()
        {
            return View();
        }
        [Route("/Cart/AddToCart/id")]
        public ActionResult AddToCart(int id)
        {
            
            int userId = 1;
            Cart c = new Cart();
            c.UserId = userId;
            c.MenuItemId = id;
            

            context.Carts.Add(c);
            context.SaveChanges();

            return RedirectToAction("ViewCartItems");
        }
        public ActionResult ViewCartItems()
        {
            //MenuItem menuItem = new MenuItem();
            

            var selctedCartItems = from c1 in context.Carts
                      join m1 in context.MenuItems
                      on c1.MenuItemId equals m1.Id
                      select m1;  

            //List < MenuItem > cartItems = new List<MenuItem> { };
            

            return View(selctedCartItems);
        }

        public ActionResult DeleteFromCart(int id)
        {
            var Query = from cart in context.Carts.Include(m => m.MenuItem)
                        where cart.MenuItemId == id
                        select cart;
            foreach (var item in Query.ToList())
            {
                if (item.MenuItem.Id == id)
                {
                    context.Carts.Remove(item);
                    context.SaveChanges();
                    //Console.WriteLine("Menu item removed from cart successfully");
                }


            }
            return RedirectToAction("ViewCartItems");
        }
    }
}