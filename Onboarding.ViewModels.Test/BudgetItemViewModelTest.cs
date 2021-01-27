using FluentAssertions;
using Onboarding.Models;
using System;
using Xunit;

namespace Onboarding.ViewModels.Test
{
    public partial class BudgetItemViewModelTest
    {
        [Fact]
        public void Constructor_ValidModel_ExpectPresentation()
        {
            BudgetItem budgetItem = new BudgetItem("test name", BudgetItemType.Debt, 123.45m);

            BudgetItemViewModel viewModel = new BudgetItemViewModel(budgetItem);

            viewModel.Name.Should().Be(budgetItem.Name);
            viewModel.Amount.Should().Be(budgetItem.Amount.ToCurrencyString());
        }

        [Fact]
        public void Constructor_NullModel_ExpectScreams()
        {
            Action testAction = () => new BudgetItemViewModel(null);

            testAction.Should().Throw<ArgumentNullException>()
                .WithMessage("*budgetItem*");
        }

        [Fact]
        public void Amount_NegativeInBudgetItem_ExpectPositiveInString()
        {
            decimal value = -4325.32m;
            BudgetItemViewModel viewModel = new BudgetItemViewModel(new BudgetItem("test name", amount: value));

            string expectedValue = Math.Abs(value).ToCurrencyString();
            viewModel.Amount.Should().Be(expectedValue);
        }

        [Fact]
        public void Amount_Set_ExpectValueAsString()
        {
            BudgetItem budgetItem = new BudgetItem("test name");
            BudgetItemViewModel viewModel = new BudgetItemViewModel(budgetItem);
            decimal value = 4325.32m;

            viewModel.Amount = value.ToCurrencyString();

            viewModel.Amount.Should().Be(value.ToCurrencyString());
        }

        [Fact]
        public void Amount_Set_ExpectPropertyChanged()
        {
            BudgetItem budgetItem = new BudgetItem("test name");
            BudgetItemViewModel viewModel = new BudgetItemViewModel(budgetItem);

            decimal newAmount = -4325.32m;
            bool wasChanged = false;
            viewModel.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(viewModel.Amount))
                {
                    wasChanged = true;
                }
            };

            viewModel.Amount = newAmount.ToCurrencyString();

            wasChanged.Should().BeTrue();
        }

        [Fact]
        public void Name_Set_ExpectValueAsString()
        {
            BudgetItem budgetItem = new BudgetItem("test name");
            BudgetItemViewModel viewModel = new BudgetItemViewModel(budgetItem);
            string newName = "new name";

            viewModel.Name = newName;

            viewModel.Name.Should().Be(newName);
        }

        [Fact]
        public void Name_Set_ExpectPropertyChanged()
        {
            BudgetItem budgetItem = new BudgetItem("test name");
            BudgetItemViewModel viewModel = new BudgetItemViewModel(budgetItem);

            string newName = "new name";
            bool wasChanged = false;
            viewModel.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(viewModel.Name))
                {
                    wasChanged = true;
                }
            };

            viewModel.Name = newName;

            wasChanged.Should().BeTrue();
        }
    }
}