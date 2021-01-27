using Onboarding.Models;
using System;
using System.ComponentModel;

namespace Onboarding.ViewModels
{
    public class BudgetItemViewModel : INotifyPropertyChanged
    {
        internal readonly BudgetItem BudgetItem;

        public event PropertyChangedEventHandler PropertyChanged;
        public string Amount
        {
            get => Math.Abs(BudgetItem.Amount).ToCurrencyString();
            set => BudgetItem.Amount = value.ToDecimalAsCurrency();
        }
        public string Name
        {
            get => BudgetItem.Name;
            set => BudgetItem.Name = value;
        }

        public BudgetItemViewModel(BudgetItem budgetItem)
        {
            this.BudgetItem = budgetItem ?? throw new ArgumentNullException(nameof(budgetItem));

            // NOTE: this only works if names in the VM are perfectly aligned to the model
            budgetItem.PropertyChanged += (o, e) => PropertyChanged?.Invoke(this, e);
        }
    }

}