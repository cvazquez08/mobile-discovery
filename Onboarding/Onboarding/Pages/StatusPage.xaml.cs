using Onboarding.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Onboarding.Pages
{
    public partial class StatusPage : ContentPage
    {
        OnboardingProfileViewModel onboardingProfileViewModel;
        public StatusPage(OnboardingProfileViewModel onboardingProfileViewModel)
        {
            InitializeComponent();

            this.onboardingProfileViewModel = onboardingProfileViewModel;
            BindingContext = onboardingProfileViewModel;

            contentView.ContinueButton.IsEnabled = false;
            contentView.ContinueButtonAction = async () => await ContinueClicked();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateContinueButton();
        }

        // TODO: likely belongs in subview
        // TODO: Unify this code across Status and Goals
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedIcons = e.CurrentSelection.Cast<TitledIconViewModel>().ToList();

            // Only set selected to true for items the users has selected in the local view
            onboardingProfileViewModel.Status.ToList().ForEach(icon => icon.IsSelected = selectedIcons.Contains(icon));

            // Only enable the continue button if more than one item is selected
            UpdateContinueButton();
        }

        public async Task ContinueClicked()
        {
            onboardingProfileViewModel.LogSelectedGoals();

            await Navigation.PushAsync(new MenuPage());            
        }

        // TODO: Unify this code across Status and Goals
        private void UpdateContinueButton()
            => contentView.ContinueButton.IsEnabled = onboardingProfileViewModel.Status.Any(item => item.IsSelected);
    }
}