using Microsoft.AspNetCore.Mvc;
using QRScanner.Models;
using QRScanner.Services;
using System.Diagnostics;

namespace QRScanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmailService _emailService;

        public HomeController(ILogger<HomeController> logger, EmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View(new ContactFormModel());
        }

           [HttpPost]
        public async Task<IActionResult> Contact(ContactFormModel form)
        {
            if (ModelState.IsValid)
            {
                await _emailService.SendEmailAsync(form.Name, form.ContactNumber, form.Message);
                //ViewBag.Message = "Your message was sent successfully!";
                TempData["Success"] = "Email sent successfully!";
                return RedirectToAction("Index", "Home");
            }

            return View(form);
        }

        //[HttpPost]
        //public IActionResult SubmitForm(ContactFormModel form)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Save to DB or send email (logic goes here)
        //        ViewBag.Message = "Form submitted successfully!";
        //    }

        //    return View("Index", form);
        //}

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
