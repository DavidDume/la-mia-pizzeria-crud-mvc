using la_mia_pizzeria_crud_mvc.Database;
using la_mia_pizzeria_crud_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace la_mia_pizzeria_crud_mvc.Controllers
{
    public class PizzaController : Controller
    {

        private readonly PizzaContext _db;
        public IActionResult Index()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();
                return View("Index", pizzas);
            }
        }

        public IActionResult Details(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? Pizza = db.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Category).FirstOrDefault();

                if (Pizza == null)
                {
                    return NotFound($"La pizza con id:{id} non è stato trovato!");
                }
                else
                {
                    return View("Details", Pizza);
                }
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            using (PizzaContext db = new())
            {
                List<Category> categories = db.Categories.ToList();
                PizzaForm model = new();
                model.Pizza = new Pizza();
                model.Categories = categories;

                return View("Create", model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza newPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", newPizza);
            }
            using (PizzaContext db = new PizzaContext())
            {
                db.Pizzas.Add(newPizza);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza pizzaToEdit = db.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Category).FirstOrDefault() as Pizza;

                if (pizzaToEdit == null)
                {
                    return NotFound("La pizza non è stata trovata");
                }
                else
                {
                    List<Category> categories = db.Categories.ToList();
                    PizzaForm model = new();
                    model.Pizza = pizzaToEdit;
                    model.Categories = categories;

                    return View("Update", model);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaForm updatedPizza)
        {
            var pizza = _db.Pizzas.Include(p => p.Ingredients).Include(p => p.Category).DefaultIfEmpty().SingleOrDefault(p => p.Id == id);

            if (pizza is null)
            {

                    List<Category> Categorie = _db.Categories.ToList();
                    updatedPizza.Categories = Categorie;
                    return View("Update", updatedPizza);
                
                    
            } else
            {
                updatedPizza.Pizza.Id = id;
                using (PizzaContext db = new PizzaContext())
                {
                    Pizza? pizzaToUpdate = db.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Category).FirstOrDefault();

                    if (pizzaToUpdate != null)
                    {
                        pizzaToUpdate.Name = updatedPizza.Pizza.Name;
                        pizzaToUpdate.Description = updatedPizza.Pizza.Description;
                        pizzaToUpdate.Image = updatedPizza.Pizza.Image;
                        pizzaToUpdate.Price = updatedPizza.Pizza.Price;

                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return NotFound("Mi spiace, non sono state trovate Pizze da aggiornare");
                    }
                }
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? pizzaToDelete = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToDelete != null)
                {
                    db.Pizzas.Remove(pizzaToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("La Pizza da eliminare non è stata trovata");
                }
            }
        }
    }
}
