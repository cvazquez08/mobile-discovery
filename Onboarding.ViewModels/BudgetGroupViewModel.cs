using Onboarding.Models;
using Onboarding.Models.Builders;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Onboarding.ViewModels
{
    public class BudgetGroupViewModel : INotifyPropertyChanged
    {
        readonly BudgetGroupPresentation presentation;

        internal readonly BudgetGroup BudgetGroup;

        internal string HeaderTextWithDate => $"{BudgetGroup.Name?.ToUpper()} for {DateTime.Now:MMMM}";
        internal string HeaderTextWithoutDate => BudgetGroup.Name?.ToUpper();

        public event PropertyChangedEventHandler PropertyChanged;
        public string Name => BudgetGroup.Name;
        public string NameLower => BudgetGroup.Name.ToLower(); // To work around span issue not supportign TextTransform (https://github.com/xamarin/Xamarin.Forms/issues/11499) which I think is fixed in 5.0.0
        public string TotalAmount => BudgetGroup.TotalAmount.ToCurrencyString();
        public bool IsTotalAmountZero => BudgetGroup.TotalAmount == 0;
        public string ColorKey => presentation.ColorKey;
        public string IconSource => presentation.IconSource;
        public string Description => presentation.Description;
        public string Subtitle => presentation.Subtitle;
        public string AddItemText => presentation.AddItemText;
        public string CustomTitle => presentation.CustomTitle;
        public bool HasSecondaryHeader => presentation.HasSecondaryHeader;
        public bool IsTitleVisible => string.IsNullOrWhiteSpace(CustomTitle);
        public bool IsCustomTitleVisible => !IsTitleVisible;
        public string HeaderText => presentation.HasDateOnHeader ? HeaderTextWithDate : HeaderTextWithoutDate;
        public ObservableCollection<BudgetItemViewModel> BudgetItems { get; } = new ObservableCollection<BudgetItemViewModel>();
        
        public BudgetGroupViewModel(BudgetGroup budgetGroup)
        {
            BudgetGroup = budgetGroup ?? throw new ArgumentNullException(nameof(budgetGroup));
            presentation = BudgetGroupPresentationBuilder.Build(budgetGroup);

            budgetGroup.BudgetItems.ToList().ForEach(item => AddBudgetItemViewModel(item));
            budgetGroup.BudgetItems.CollectionChanged += BudgetItemsChanged;

            budgetGroup.PropertyChanged += (o, e) => PropertyChanged?.Invoke(this, e); // NOTE: this only works as the names in this VM are the same as the model
        }

        public void AddNewBudgetItem() => BudgetGroup.AddNewBudgetItem();

        // TODO: implement an abstraction/shared pattern for VMs containing a collection of models that needs to be mirrored
        private void BudgetItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Add new items as view models
            e.NewItems?.Cast<BudgetItem>().ToList().ForEach(item => AddBudgetItemViewModel(item));

            // Remove old items from viewmodels
            e.OldItems?.Cast<BudgetItem>().ToList().ForEach(item => BudgetItems.Remove(FindMatchingViewModel(item)));
        }

        private BudgetItemViewModel FindMatchingViewModel(BudgetItem item) => BudgetItems.Where(vm => vm.BudgetItem == item).FirstOrDefault();

        private void AddBudgetItemViewModel(BudgetItem item)
        {
            var itemViewModel = new BudgetItemViewModel(item);
            BudgetItems.Add(itemViewModel);
        }
    }
}
