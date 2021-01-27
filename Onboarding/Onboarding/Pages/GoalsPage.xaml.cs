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
        
        // TODO: likely belinds in a subview
        // TODO: unify this code across Status and Goals
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedIcons = e.CurrentSelection.Cast<TitledIconViewModel>().ToList();
            
            // only set selected to true for items the users has selected in the local view
            onboardingProfileViewModel.Goals.ToList().ForEach(icon => icon.IsSelected = selectedIcons.Contains(icon));
            
            // only enable the continue button if more than more item is selected
            contentView.ContinueButton.IsEnabled = selectedIcons.Count >= 1;
        }

        public async Task ContinueClicked()
        {
            onboardingProfileViewModel.LogSelectedGoals();

            await Navigation.PushAsync(new StatusPage(onboardingProfileViewModel));            
        }
    }
}