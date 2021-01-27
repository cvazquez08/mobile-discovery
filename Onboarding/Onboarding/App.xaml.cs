using Onboarding.Pages;
using Xamarin.Forms;

namespace Onboarding
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Dependencies.Init();

            MainPage = new NavigationPage(new IntroPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}