using OrchardCore.Themes.TheCleanBlogTheme.ViewModels;
using Stripe;
using System;

namespace OrchardCore.Themes.TheCleanBlogTheme.Services
{
    public class CardPaymentService : ICardPaymentService
    {
        private const int PaymentAmountPence = 9900;
        private const string PaymentCurrency = "USD";
        private const string PaymentDescription = "Orchard Skills Test Stripe Card Payment";
        private const bool CaptureCardPayment = true; // "false" prevents the card being charged!
        private readonly ChargeService _chargeService;

        public CardPaymentService()
        {
            this._chargeService = new ChargeService();
        }

        public CardPaymentReceiptViewModel Create(CardPaymentViewModel viewModel)
        {
            var paymentTransactionId = Guid.NewGuid().ToString();

            var chargeCreateOptions = new ChargeCreateOptions
            {
                TransferGroup = paymentTransactionId,
                Amount = PaymentAmountPence,
                Currency = PaymentCurrency,
                Description = PaymentDescription,
                Source = viewModel.Token,
                Capture = CaptureCardPayment,
                ReceiptEmail = viewModel.Email,
            };

            var charge = _chargeService.Create(chargeCreateOptions);

            return ToPaymentReceipt(charge);
        }

        private CardPaymentReceiptViewModel ToPaymentReceipt(Charge charge)
        {
            var cardPaymentReceiptViewModel = new CardPaymentReceiptViewModel
            {
                Amount = charge.Amount,
                Currency = charge.Currency,
                Description = charge.Description,
                Status = charge.Status,
                Created = charge.Created,
                BalanceTransactionId = charge.BalanceTransactionId,
                Id = charge.Id,
                SourceId = charge.Source.Id,

            };

            return cardPaymentReceiptViewModel;
        }
    }
}