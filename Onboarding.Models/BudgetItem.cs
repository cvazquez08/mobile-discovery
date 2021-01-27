using System.ComponentModel;

namespace Onboarding.Models
{
    public class BudgetItem : INotifyPropertyChanged
    {
        public readonly BudgetItemType Type;

        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set;  }
        public decimal Amount { get; set;  }

        public BudgetItem(string name, BudgetItemType type = BudgetItemType.Expense, decimal amount = 0)
        {
            Name = name;
            Type = type;
            Amount = amount;
        }
    }
}