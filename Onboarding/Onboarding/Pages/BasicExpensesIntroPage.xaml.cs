using Onboarding.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Onboarding.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasicExpensesIntroPage : ContentPage
    {
        public BasicExpensesIntroPage(BudgetGroupViewModel budgetGroup)
        {
            InitializeComponent();
            BindingContext = budgetGroup;

            contentView.ContinueButtonAction = async () => await Navigation.PushAsync(new TransportationPage(Dependencies.ProfileViewModel.Budget.TransportationGroup));
        }
    }
}