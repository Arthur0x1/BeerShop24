using Beershop24.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Beershop24.Controllers
{
    public class GenericController : Controller
    {
        public IActionResult Index()
        {
            var cities = new DataStore<string>();
            cities.AddOrUpdate(0, "Mumbai");
            cities.AddOrUpdate(1, "Chicago");
            cities.AddOrUpdate(2, "London");

            var empIds = new DataStore<int>();
            empIds.AddOrUpdate(0, 50);
            empIds.AddOrUpdate(1, 65);
            empIds.AddOrUpdate(2, 89);


            Debug.WriteLine(cities.GetData(2));
            Debug.WriteLine(empIds.GetData(1));
            return View();
        }
    }
}
