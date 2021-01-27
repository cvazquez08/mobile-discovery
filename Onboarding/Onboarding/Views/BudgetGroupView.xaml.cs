using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Onboarding.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetGroupView : ContentView
    {
        private const float BudgetItemHeight = 55;
        
        public BudgetGroupView()
        {
            InitializeComponent();
        }
    }
}