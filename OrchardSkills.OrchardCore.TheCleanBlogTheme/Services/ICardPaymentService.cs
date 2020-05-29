using OrchardCore.Themes.TheCleanBlogTheme.ViewModels;

namespace OrchardCore.Themes.TheCleanBlogTheme.Services
{
    public interface ICardPaymentService
    {
        CardPaymentReceiptViewModel Create(CardPaymentViewModel viewModel);
    }
}
