using la_mia_pizzeria_crud_mvc.Database;
using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_crud_mvc.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();
                return View("Index", pizzas);
            }
        }
    }
}
