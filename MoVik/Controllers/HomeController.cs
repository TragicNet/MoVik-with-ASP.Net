using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MoVik.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MoVik.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Menu()
        {
            using var connection = new SqlConnection();
            connection.Open();

            using var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT Username FROM Users;";

            using var reader = command.ExecuteReader();

            List<MenuItem> menuItems = new List<MenuItem>();

            while (reader.Read())
            {
                string name = reader.GetString(0);
                menuItems.Add(new MenuItem { Name = name });
            }

            
            return View(menuItems);
        }

        public IActionResult Index()
        {
            return View();
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
