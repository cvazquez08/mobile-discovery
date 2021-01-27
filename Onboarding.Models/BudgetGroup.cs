using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Onboarding.Models
{
    public class BudgetGroup : INotifyPropertyChanged
    {
        internal string NewBudgetItemName => $"Item {BudgetItems.Count + 1}";

        public readonly string Name;
        public readonly BudgetItemType DefaultItemType;
        public readonly ObservableCollection<BudgetItem> BudgetItems = new ObservableCollection<BudgetItem>();

        public event PropertyChangedEventHandler PropertyChanged;

        public decimal TotalAmount => BudgetItems.Sum(x => x.Amount);

        public BudgetGroup(
            string name,
            BudgetItemType defaultItemType = BudgetItemType.Expense,
            List<BudgetItem> budgetItems = null)
        {
            Name = name;
            DefaultItemType = defaultItemType;

            BudgetItems.CollectionChanged += BudgetItemCollectionChanged;

            // Add any passed in budget items
            budgetItems?.ForEach(item => BudgetItems.Add(item));
        }

        // Helper method
        public void AddNewBudgetItem()
        {
            var newItem = new BudgetItem(NewBudgetItemName, DefaultItemType);
            BudgetItems.Add(newItem);
        }

        private void BudgetItemCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Add a listener to each new account
            var newItemsList = e.NewItems?.Cast<BudgetItem>().ToList();
            newItemsList?.ForEach(a => a.PropertyChanged += BudgetItemPropertyChanged);

            // NOTE: we could look into the specific type of event to see if calculations actually changed,
            // but this would be premature optimization at this point
            NotifyTotalAmountChanged();
        }

        private void BudgetItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Notify listeners of amount changes
            if (e.PropertyName == nameof(BudgetItem.Amount))
            {
                NotifyTotalAmountChanged();
            }
        }

        private void NotifyTotalAmountChanged()
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalAmount)));
    }
}