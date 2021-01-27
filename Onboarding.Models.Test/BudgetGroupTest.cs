using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Onboarding.Models.Test
{
    public class BudgetGroupTest
    {
        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            string name = "test group name";
            BudgetItemType defaultType = BudgetItemType.Expense;
            List<BudgetItem> items = new List<BudgetItem>()
            {
                new BudgetItem("test item 1"),
                new BudgetItem("test item 2", BudgetItemType.Debt, 12.34m)
            };

            var group = new BudgetGroup(name, defaultType, items);

            group.DefaultItemType.Should().Be(defaultType);
            group.Name.Should().Be(name);
            group.BudgetItems.Should().BeEquivalentTo(items);
            group.TotalAmount.Should().Be(items.Sum(x => x.Amount));
        }

        [Fact]
        public void AddNewBudgetItem_ExpectAddedWithDefaultValues()
        {
            var expectedType = BudgetItemType.Debt;
            var group = new BudgetGroup("test group", defaultItemType: expectedType);
            var expectedName = group.NewBudgetItemName;

            group.AddNewBudgetItem();

            group.BudgetItems.Count().Should().Be(1);
            group.BudgetItems.Last().Name.Should().Be(expectedName);
            group.BudgetItems.Last().Type.Should().Be(expectedType);
        }

        [Fact]
        public void AddNewBudgetItem_ExpectNewNameOnEachAddition()
        {
            var group = new BudgetGroup("test group");
            var firstNewItemName = group.NewBudgetItemName;

            group.AddNewBudgetItem();

            group.NewBudgetItemName.Should().NotBe(firstNewItemName);
        }

        [Fact]
        public void TotalAmount_ItemAmountSet_ExpectListenerNotified()
        {
            var items = new List<BudgetItem>()
            {
                new BudgetItem("item name", BudgetItemType.Income, 12.34m)
            };

            var group = new BudgetGroup("test group", budgetItems: items);

            bool wasChanged = false;
            group.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(group.TotalAmount)) wasChanged = true;
            };

            var newAmount = 99.99m;
            items[0].Amount = newAmount;
            wasChanged.Should().BeTrue();
        }

        [Fact]
        public void TotalAmount_SeveralItemAmounts_ExpectTotalAmountCorrect()
        {
            var firstItemAmount = 12.34m;
            var secondItemAmount = 99.99m;
            var thirdItemAmount = 88.88m;
            var expectedTotal = firstItemAmount + secondItemAmount + thirdItemAmount;
            var items = new List<BudgetItem>()
            {
                new BudgetItem("item name", amount: firstItemAmount),
                new BudgetItem("item name"),
                new BudgetItem("item name"),
            };

            var group = new BudgetGroup("test group", budgetItems: items);

            // TEST: set several items with various amounts
            items[1].Amount = secondItemAmount;
            items[2].Amount = thirdItemAmount;

            // Expect total is correct
            group.TotalAmount.Should().Be(expectedTotal);
        }

        [Fact]
        public void TotalAmount_AddNewBudgetItem_ExpectGroupListenerNotified()
        {
            var items = new List<BudgetItem>()
            {
                new BudgetItem("item name", BudgetItemType.Debt , 12.34m)
            };

            var group = new BudgetGroup("test group", budgetItems: items);

            group.AddNewBudgetItem();

            bool wasGroupNotified = false;
            group.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(group.TotalAmount)) wasGroupNotified = true;
            };

            var newAmount = 99.99m;
            group.BudgetItems.Last().Amount = newAmount;
            wasGroupNotified.Should().BeTrue();
        }
    }
}
