using System.Collections.Generic;

namespace Onboarding.Models.Builders
{
    public static class BudgetBuilder
    {
        //
        // NOTE: we could make this data driven from JSON to provide dynamic, easy-to-update content
        //
        public static BudgetGroup IncomeGroup => new BudgetGroup(
            "Income",
            BudgetItemType.Income,
            new List<BudgetItem>()
            {
                    new BudgetItem("Paycheck 1", BudgetItemType.Income),
                    new BudgetItem("Paycheck 2", BudgetItemType.Income),
            });

        public static BudgetGroup HousingGroup => new BudgetGroup(
                "Housing",
                budgetItems: new List<BudgetItem>()
                {
                    new BudgetItem("Mortgage/Rent"),
                    new BudgetItem("Water"),
                    new BudgetItem("Natural Gas"),
                    new BudgetItem("Electricity"),
                    new BudgetItem("Cable"),
                    new BudgetItem("Trash"),
                });

        public static BudgetGroup TransportationGroup => new BudgetGroup(
                "Transportation",
                budgetItems: new List<BudgetItem>()
                {
                    new BudgetItem("Lambo Cleaning"),
                    new BudgetItem("Ferrari Repairs"),
                });

        public static BudgetGroup FoodGroup => new BudgetGroup(
                "Food",
                budgetItems: new List<BudgetItem>()
                {
                    new BudgetItem("Groceries"),
                    new BudgetItem("Restaurants"),
                    new BudgetItem("Hard Liquor"),
                });

        public static BudgetGroup PersonalGroup => new BudgetGroup(
                "Personal",
                budgetItems: new List<BudgetItem>()
                {
                    new BudgetItem("Clothing"),
                    new BudgetItem("Phone"),
                    new BudgetItem("Fun Money"),
                    new BudgetItem("Hair/Cosmetics"),
                });

        public static BudgetGroup DebtGroup => new BudgetGroup(
            "Debt",
            BudgetItemType.Debt,
            new List<BudgetItem>()
            {
                    new BudgetItem("Car Payment", BudgetItemType.Debt),
                    new BudgetItem("Credit Card 1", BudgetItemType.Debt),
                    new BudgetItem("Credit Card 2", BudgetItemType.Debt),
                    new BudgetItem("Student Loan", BudgetItemType.Debt),
            });

        public static BudgetGroup GivingGroup => new BudgetGroup(
            "Giving",
            budgetItems: new List<BudgetItem>()
            {
                    new BudgetItem("Tithe"),
                    new BudgetItem("Email Phising"),
            });

        // For the basic expenses intro page, we need a display-only view model.  Not synced to remote.
        public static BudgetGroup BasicExpensesDisplayGroup 
            => new BudgetGroup(
                "Basic Expenses",
                budgetItems: new List<BudgetItem>()
                {
                        new BudgetItem("Placeholder to store total amount of several other groups")
                });

        public static Budget Build()
            => new Budget(
                  IncomeGroup,
                  HousingGroup,
                  TransportationGroup,
                  FoodGroup,
                  PersonalGroup,
                  GivingGroup,
                  DebtGroup,
                  BasicExpensesDisplayGroup);          
        
    }
}