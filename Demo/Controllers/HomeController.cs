using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUser _repository;

        public HomeController(ILogger<HomeController> logger, IUser repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region add without ajax

        [HttpPost]
        public IActionResult addUser(User userViewModel)
        {
            if (ModelState.IsValid)
            {
                var status = _repository.addUser(userViewModel);
                return RedirectToAction("getUsers");
            }
            else
            {
                return View("getAddTemplate");
            }
            
        }

        public IActionResult getAddTemplate()
        {
            return View();
        }
        #endregion add without ajax

        #region add using ajax

        [HttpPost]
        public IActionResult addWithAjax(User userViewModel)
        {
            if (ModelState.IsValid)
            {
                var status = _repository.addUser(userViewModel);
                return RedirectToAction("getUsers");
            }
            else
            {
                return PartialView("getForm");
            }

        }

        public IActionResult addWithAjax()
        {
            return View();
        }

        public PartialViewResult getForm()
        {
            return PartialView();
        }

        #endregion add with ajax

        #region user List

        public IActionResult getUsers()
        {
            List<User> users = _repository.getUsers();
            ViewData["users"] = users;
            return View();
        }

        #endregion user List

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
