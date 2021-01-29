using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Onboarding.ViewModels;
using Xamarin.Forms;

namespace Onboarding.Pages
{
    public partial class GoalsPage : ContentPage
    {
        OnboardingProfileViewModel onboardingProfileViewModel;

        public GoalsPage(OnboardingProfileViewModel onboardingProfileViewModel)
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
        
        //TODO: Likely belongs in subview
        //TODO: unify this code acroess Status and Goals
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedIcons = e.CurrentSelection.Cast<TitledIconViewModel>().ToList();
            
            // only set selected to true for items the users has selected in the local view
            onboardingProfileViewModel.Goals.ToList().ForEach(icon => icon.IsSelected = selectedIcons.Contains(icon));
            
            // only enable the continue button if more than one item is selected
            contentView.ContinueButton.IsEnabled = selectedIcons.Count >= 1;
        }

        public async Task ContinueClicked()
        {
            onboardingProfileViewModel.LogSelectedGoals();

            await Navigation.PushAsync(new StatusPage(onboardingProfileViewModel));
        }
        
        //TODO: unifiy this code accress Status and Gaols
        private void UpdateContinueButton() => contentView.ContinueButton.IsEnabled =
            onboardingProfileViewModel.Goals.Any(item => item.IsSelected);
    }
        
}