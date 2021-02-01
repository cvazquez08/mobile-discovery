using Xamarin.Forms;

namespace Onboarding.Pages
{
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();

            contentView.ContinueButtonAction = async () => await Navigation.PushAsync(new GoalsPage(Dependencies.ProfileViewModel));
        }
    }
}