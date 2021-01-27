using Xamarin.Forms;

namespace Onboarding.Pages
{
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();

            contentView.ContinueButtonAction = () => Navigation.PushAsync(new GoalsPage(Dependencies.ProfileViewModel));
        }
    }
}