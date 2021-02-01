using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Onboarding.Pages
{
    // TODO: Move to models
    public enum OnboardingStepState
    {
        Completed,
        Selected,
        Pending
    }

    // TODO: Move to models and create view model
    public class OnboardingStep
    {
        public OnboardingStepState State { get; set; } = OnboardingStepState.Pending;

        public Color ThemeColor { get; set; } = Color.Maroon;
        public string Name { get; set; }
        public string NameFontAttributes { get; set; } = "None";
        public string NameColor { get; set; } = "OnboardingGray";
        public string NameDecorations { get; set; } = "None";
        public string IconSource { get; set; }
        public string IconGraySource { get; set; }
        public string Amount { get; set; }

        public bool IsSelected => State == OnboardingStepState.Selected;
        public bool IsCompleted => State == OnboardingStepState.Completed;
        public bool IsPending => State == OnboardingStepState.Pending;
    }

    public partial class MenuPage : ContentPage
    {
        public ObservableCollection<OnboardingStep> OnboardingSteps { get; private set; } = new ObservableCollection<OnboardingStep>();

        public MenuPage()
        {
            InitializeComponent();

            CreateOnboardingSteps();

            // TODO: VM
            BindingContext = this;

            contentView.ContinueButtonAction = () => Navigation.PushAsync(new IncomeIntroPage(Dependencies.ProfileViewModel.Budget.IncomeGroup));
        }

        // TODO: move out
        private void CreateOnboardingSteps()
        {
            OnboardingSteps = new ObservableCollection<OnboardingStep>()
            {
                new OnboardingStep(){Name = "Goals", Amount = "", ThemeColor = Application.Current.Resources.GetColor("OnboardingGreen"), State = OnboardingStepState.Completed, NameDecorations="Strikethrough"},
                new OnboardingStep(){Name = "Income", Amount = "$123.45", ThemeColor = Application.Current.Resources.GetColor("OnboardingGreen"), IconSource="icon_income.png", State = OnboardingStepState.Selected},
                new OnboardingStep(){Name = "Basic Expenses", Amount = "$123.45", ThemeColor = Application.Current.Resources.GetColor("OnboardingBlue"), IconSource="icon_housing.png", IconGraySource="icon_housing_gray.png"},
                new OnboardingStep(){Name = "Giving", Amount = "$123.45", ThemeColor = Application.Current.Resources.GetColor("OnboardingLightPurple"), IconSource="icon_giving.png", IconGraySource="icon_giving_gray.png"},
                new OnboardingStep(){Name = "Debt", Amount = "$123.45", ThemeColor = Application.Current.Resources.GetColor("OnboardingRed"), IconSource="icon_debt.png", IconGraySource="icon_debt_gray.png"},
            };
        }
    }
}