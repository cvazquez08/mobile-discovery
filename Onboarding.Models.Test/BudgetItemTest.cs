using FluentAssertions;
using System;
using Xunit;

namespace Onboarding.Models.Test
{
    public class BudgetItemTest
    {
        [Fact]
        public void Constructor_ValidProperty_ExpectAssignment()
        {
            string name = "test name";
            decimal amount = 123.45m;
            BudgetItemType type = BudgetItemType.Expense;

            BudgetItem budgetItem = new BudgetItem(name, type, amount);

            budgetItem.Should().NotBeNull();
            budgetItem.Name.Should().Be(name);
            budgetItem.Amount.Should().Be(amount);
            budgetItem.Type.Should().Be(type);
        }
    }
}