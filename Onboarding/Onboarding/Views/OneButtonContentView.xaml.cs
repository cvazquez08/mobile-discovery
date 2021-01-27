using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Onboarding.Views
{
    [ContentProperty("MainContent")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OneButtonContentView : ContentView
    {
        public Action ContinueButtonAction { get; set; } = () => throw new NotImplementedException();
        public Button ContinueButton => continueButton;

        public View MainContent
        {
            get { return mainContent.Content;}
            set { mainContent.Content = value; }
        }

        public OneButtonContentView()
        {
            InitializeComponent();
        }

        void ContinueClicked(object sender, System.EventArgs e) => ContinueButtonAction?.Invoke();
    }
}