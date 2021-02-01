using Onboarding.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Onboarding.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomeIntroPage : ContentPage
    {
        public IncomeIntroPage(BudgetGroupViewModel budgetGroup)
        {
            InitializeComponent();

            BindingContext = budgetGroup;

            contentView.ContinueButtonAction = async () => await Navigation.PushAsync(new IncomePage(budgetGroup));
        }
    }
}