using Onboarding.Models;
using System;

namespace Onboarding.ViewModels
{
    public class BudgetViewModel
    {
        public readonly Budget Budget;

        // NOTE: currently, these are passed into specific pages, and not bound to directly, so they can be readonly
        // To bind directly to these, we would need to change to getters
        public readonly BudgetGroupViewModel IncomeGroup;
        public readonly BudgetGroupViewModel HousingGroup;
        public readonly BudgetGroupViewModel TransportationGroup;
        public readonly BudgetGroupViewModel FoodGroup;
        public readonly BudgetGroupViewModel PersonalGroup;
        public readonly BudgetGroupViewModel GivingGroup;
        public readonly BudgetGroupViewModel DebtGroup;
        public readonly BudgetGroupViewModel BasicExpensesDisplayGroup; // For display settings only.  Not synced with remote 

        public BudgetViewModel(Budget budget)
        {
            Budget = budget ?? throw new ArgumentNullException(nameof(budget));

            IncomeGroup = new BudgetGroupViewModel(budget.IncomeGroup);
            HousingGroup = new BudgetGroupViewModel(budget.HousingGroup);
            TransportationGroup = new BudgetGroupViewModel(budget.TransportationGroup);
            FoodGroup = new BudgetGroupViewModel(budget.FoodGroup);
            PersonalGroup = new BudgetGroupViewModel(budget.PersonalGroup);
            DebtGroup = new BudgetGroupViewModel(budget.DebtGroup);
            GivingGroup = new BudgetGroupViewModel(budget.GivingGroup);
            BasicExpensesDisplayGroup = new BudgetGroupViewModel(budget.BasicExpensesDisplayGroup);
        }
    }
}