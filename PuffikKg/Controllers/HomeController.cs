using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PuffikKg.Data;
using PuffikKg.Models;
using MimeKit;
using MailKit.Net.Smtp;

namespace PuffikKg.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string contactName, string contactEmail, string contactPhone, string contactMessage)
        {
            var message = new MimeMessage();
            var text = $@"
            Вам письмо от {contactName},
            Телефон: {contactPhone},
            Email: {contactEmail},
            Текст:
            {contactMessage}
        ";
            message.From.Add(new MailboxAddress("Sender", contactEmail));
            message.To.Add(new MailboxAddress("ComfyBag", "comfybag.bishkek@gmail.com"));
            message.Subject = "ComfyBag Mail";
            message.Body = new TextPart("plain")
            {
                Text = text
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("comfybag.bishkek@gmail.com", password);
                client.Send(message);
                client.Disconnect(true);
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
