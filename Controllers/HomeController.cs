using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Emailer.Entities;
using Microsoft.AspNetCore.Mvc;
using Emailer.Models;
using Emailer.Services;

namespace Emailer.Controllers
{
    public class HomeController : Controller
    {
        private const string SenderEmail = "kirill.mehtiev@ukr.net";
        private readonly IEmailMessageService _messageService;
        private readonly IEmailSenderService _emailSenderService;

        public HomeController(IEmailMessageService messageService, IEmailSenderService emailSenderService)
        {
            _messageService = messageService;
            _emailSenderService = emailSenderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(EmailMessageViewModel emailMessageViewModel)
        {
            if (ModelState.IsValid)
            {
                var emailMessage = new EmailMessage
                {
                    SenderEmail = SenderEmail,
                    ReceiverEmail = emailMessageViewModel.Email,
                    Subject = emailMessageViewModel.Subject,
                    Body = emailMessageViewModel.Message,
                    Status = EmailMessageStatus.Pending
                };

                await _messageService.InsertAsync(emailMessage);

                //await _emailSenderService.Send(SenderEmail, 
                //    emailMessageViewModel.Email, 
                //    emailMessageViewModel.Subject,
                //    emailMessageViewModel.Message);

                return View();
            }

            return View(emailMessageViewModel);
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
