using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Themes.TheCleanBlogTheme.Services;
using OrchardCore.Themes.TheCleanBlogTheme.ViewModels;

namespace OrchardCore.Themes.TheCleanBlogTheme.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ICardPaymentService _cardPaymentService;

        public PaymentController(ICardPaymentService cardPaymentService)
        {
            this._cardPaymentService = cardPaymentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CardPaymentViewModel viewModel)
        {
            var receiptViewModel = _cardPaymentService.Create(viewModel);

            return RedirectToAction("Receipt", "Payment", receiptViewModel);
        }

        [HttpGet]
        public IActionResult Receipt(CardPaymentReceiptViewModel viewModel)
        {
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
