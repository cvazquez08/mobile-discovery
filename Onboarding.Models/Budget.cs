using System;
using System.ComponentModel;
using PropertyChanged;

namespace Onboarding.Models
{
    public class Budget : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public readonly BudgetGroup IncomeGroup;
        public readonly BudgetGroup HousingGroup;
        public readonly BudgetGroup TransportationGroup;
        public readonly BudgetGroup FoodGroup;
        public readonly BudgetGroup PersonalGroup;
        public readonly BudgetGroup GivingGroup;
        public readonly BudgetGroup DebtGroup;
        public readonly BudgetGroup BasicExpensesDisplayGroup;
        
        public decimal IncomeRemaining { get; set; }

        public BudgetState BudgetState
        {
            get
            {
                if (IncomeRemaining < 0)
                {
                    return BudgetState.OverBudget;
                }
                else if (IncomeRemaining > 0)
                {
                    return BudgetState.UnderBudget;
                }
                else
                {
                    return BudgetState.OnBudget;
                }
            }
            
        }

        internal Budget(
            BudgetGroup incomeGroup,
            BudgetGroup housingGroup,
            BudgetGroup transportationGroup,
            BudgetGroup foodGroup,
            BudgetGroup personalGroup,
            BudgetGroup givingGroup,
            BudgetGroup debtGroup,
            BudgetGroup basicExpensesDisplayGroup)
        {
            IncomeGroup = incomeGroup ?? throw new ArgumentNullException(nameof(incomeGroup));
            HousingGroup = housingGroup ?? throw new ArgumentNullException(nameof(housingGroup));
            TransportationGroup = transportationGroup ?? throw new ArgumentNullException(nameof(transportationGroup));
            FoodGroup = foodGroup ?? throw new ArgumentNullException(nameof(foodGroup));
            PersonalGroup = personalGroup ?? throw new ArgumentNullException(nameof(personalGroup));
            GivingGroup = givingGroup ?? throw new ArgumentNullException(nameof(givingGroup));
            DebtGroup = debtGroup ?? throw new ArgumentNullException(nameof(debtGroup));
            BasicExpensesDisplayGroup = basicExpensesDisplayGroup ?? throw new ArgumentNullException(nameof(basicExpensesDisplayGroup));
            
            void InitializeBudgetGroupChangeListeners()
            {
                IncomeGroup.PropertyChanged += GroupPropertyChanged;
                HousingGroup.PropertyChanged += GroupPropertyChanged;
                TransportationGroup.PropertyChanged += GroupPropertyChanged;
                FoodGroup.PropertyChanged += GroupPropertyChanged;
                PersonalGroup.PropertyChanged += GroupPropertyChanged;
                GivingGroup.PropertyChanged += GroupPropertyChanged;
                DebtGroup.PropertyChanged += GroupPropertyChanged;
                GivingGroup.PropertyChanged += GroupPropertyChanged;
            }

            void GroupPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(BudgetGroup.TotalAmount))
                {
                    RecalculateTotals();
                }
            }

            void RecalculateTotals()
            {
                IncomeRemaining =
                    IncomeGroup.TotalAmount
                    - HousingGroup.TotalAmount
                    - TransportationGroup.TotalAmount
                    - FoodGroup.TotalAmount
                    - PersonalGroup.TotalAmount
                    - GivingGroup.TotalAmount
                    - DebtGroup.TotalAmount;

                BasicExpensesDisplayGroup.BudgetItems[0].Amount =
                    HousingGroup.TotalAmount
                    + TransportationGroup.TotalAmount
                    + FoodGroup.TotalAmount
                    + PersonalGroup.TotalAmount;
            }
        }
        
    }
}