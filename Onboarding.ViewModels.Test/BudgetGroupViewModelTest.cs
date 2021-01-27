using FluentAssertions;
using Onboarding.Models;
using Onboarding.Models.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Onboarding.ViewModels.Test
{
    public partial class BudgetGroupViewModelTest
    {
        [Theory]
        [InlineData("Income")]
        [InlineData("Housing")]
        [InlineData("Transportation")]
        [InlineData("Food")]
        [InlineData("Personal")]
        [InlineData("Debt")]
        [InlineData("Giving")]
        [InlineData("Basic Expenses")]
        public void Constructor_ValidParameters_PresentionSetupCorrectly(string groupName)
        {
            var group = new BudgetGroup(groupName);
            var expectedPresentation = BudgetGroupPresentationBuilder.Build(group);

            // TEST: Construct a view model
            BudgetGroupViewModel viewModel = new BudgetGroupViewModel(group);

            // Confirm properties and items created in view model          
            viewModel.Name.Should().Be(groupName);
            viewModel.BudgetItems.Should().BeEmpty();
            viewModel.ColorKey.Should().Be(expectedPresentation.ColorKey);
            viewModel.IconSource.Should().Be(expectedPresentation.IconSource);
            viewModel.Description.Should().Be(expectedPresentation.Description);
            viewModel.Subtitle.Should().Be(expectedPresentation.Subtitle);
            viewModel.AddItemText.Should().Be(expectedPresentation.AddItemText);
            viewModel.CustomTitle.Should().Be(expectedPresentation.CustomTitle);
            viewModel.HasSecondaryHeader.Should().Be(expectedPresentation.HasSecondaryHeader);            
            viewModel.IsTitleVisible.Should().Be(string.IsNullOrWhiteSpace(expectedPresentation.CustomTitle));
            viewModel.IsCustomTitleVisible.Should().Be(!viewModel.IsTitleVisible);
            viewModel.HeaderTextWithoutDate.Should().Be(groupName.ToUpper());
            viewModel.HeaderText.Should().Be(expectedPresentation.HasDateOnHeader ? viewModel.HeaderTextWithDate : viewModel.HeaderTextWithoutDate);       
        }

        [Fact]
        public void Constructor_UnrecognizedBudgetGroup_ThrowsException()
        {
            var groupName = "an unknown group";
            var group = new BudgetGroup(groupName);
            Action testAction = () => new BudgetGroupViewModel(group);

            testAction.Should().Throw<ArgumentException>().WithMessage($"*{groupName}*");
        }

        [Fact]
        public void Constructor_ModelWithBudgetItems_PresentsGroupAndItems()
        {
            // Create group with budget items
            string name = "Housing";
            List<BudgetItem> items = new List<BudgetItem>()
            {
                new BudgetItem("test item 1"),
                new BudgetItem("test item 2", BudgetItemType.Debt, 12.34m)
            };
            var group = new BudgetGroup(name, budgetItems: items);

            // TEST: Construct a view model
            BudgetGroupViewModel viewModel = new BudgetGroupViewModel(group);

            // Confirm properties and items created in view model
            viewModel.Name.Should().Be(group.Name);
            viewModel.NameLower.Should().Be(group.Name.ToLower());
            viewModel.TotalAmount.Should().Be(group.TotalAmount.ToCurrencyString());
            viewModel.BudgetItems.Count.Should().Be(items.Count);
            for (int i = 0; i < items.Count; ++i)
            {
                viewModel.BudgetItems[i].BudgetItem.Should().Be(items[i]);
            }
        }

        [Fact]
        public void AddNewBudgetItem_ExpectNewItemInViewModel()
        {
            // Create a group and view model with no budget items
            var group = new BudgetGroup("Housing");
            BudgetGroupViewModel viewModel = new BudgetGroupViewModel(group);

            // TEST: add new item from the view model
            viewModel.AddNewBudgetItem();

            // Confirm a new budget item as created in the backing group, and in the view model
            group.BudgetItems.Count.Should().Be(1);
            viewModel.BudgetItems.Count.Should().Be(1);
            viewModel.BudgetItems[0].BudgetItem.Should().Be(group.BudgetItems[0]);
        }

        [Fact]
        public void BudgetItems_DeleteItemInGroup_ViewModelDeleted()
        {
            // Create group with budget items
            List<BudgetItem> items = new List<BudgetItem>()
            {
                new BudgetItem("test item 1"),
                new BudgetItem("test item 2", BudgetItemType.Debt, 12.34m)
            };
            var group = new BudgetGroup("Housing", budgetItems: items);
            BudgetGroupViewModel viewModel = new BudgetGroupViewModel(group);

            // TEST: delete an item from the backing group
            var itemDeleted = group.BudgetItems[0];
            group.BudgetItems.RemoveAt(0);

            viewModel.BudgetItems.Count().Should().Be(1);
            viewModel.BudgetItems
                .Any(itemViewModel => itemViewModel.BudgetItem == itemDeleted)
                .Should().BeFalse();
        }

        [Fact]
        public void IsTotalAmountZero_AllZeroItemAmounts_ExpectTrue()
        {
            // Create group with budget items
            List<BudgetItem> items = new List<BudgetItem>()
            {
                new BudgetItem("test item 1"),
                new BudgetItem("test item 2"),
                new BudgetItem("test item 3")
            };
            var group = new BudgetGroup("Housing", budgetItems: items);

            // TEST: a view model representing a group with all zero items
            BudgetGroupViewModel viewModel = new BudgetGroupViewModel(group);

            viewModel.IsTotalAmountZero.Should().BeTrue();
        }

        [Fact]
        public void IsTotalAmountZero_NonZeroItemAmounts_ExpectFalse()
        {
            // Create group with budget items
            List<BudgetItem> items = new List<BudgetItem>()
            {
                new BudgetItem("test item 1"),
                new BudgetItem("test item 2", amount: 123.45m),
                new BudgetItem("test item 3")
            };
            var group = new BudgetGroup("Housing", budgetItems: items);

            // TEST: a view model representing a group with all zero items
            BudgetGroupViewModel viewModel = new BudgetGroupViewModel(group);

            viewModel.IsTotalAmountZero.Should().BeFalse();
        }

        [Fact]
        public void IsTotalAmountZero_ChangingItemAmounts_ExpectChangingTrueFalse()
        {
            // Create group with budget items
            List<BudgetItem> items = new List<BudgetItem>()
            {
                new BudgetItem("test item 1"),
                new BudgetItem("test item 2"),
                new BudgetItem("test item 3")
            };
            var group = new BudgetGroup("Housing", budgetItems: items);
            BudgetGroupViewModel viewModel = new BudgetGroupViewModel(group);

            // TEST: changing item amounts in the groupshould transition the IsTotalAmountZero
            viewModel.IsTotalAmountZero.Should().BeTrue();
            group.BudgetItems[1].Amount = 234.56m;
            viewModel.IsTotalAmountZero.Should().BeFalse();
            group.BudgetItems[1].Amount = 0;
            viewModel.IsTotalAmountZero.Should().BeTrue();
        }

        [Fact]
        public void TotalAmount_ChangingItemAmounts_ExpectTotalAmountChangedEvent()
        {
            // Create group with budget items
            List<BudgetItem> items = new List<BudgetItem>()
            {
                new BudgetItem("test item 1"),
                new BudgetItem("test item 2"),
                new BudgetItem("test item 3")
            };
            var group = new BudgetGroup("Housing", budgetItems: items);
            BudgetGroupViewModel viewModel = new BudgetGroupViewModel(group);

            bool wasChanged = false;
            viewModel.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(viewModel.TotalAmount)) wasChanged = true;
            };

            // TEST: changing item amounts should trigger property changed
            group.BudgetItems[1].Amount = 234.56m;

            // Validate event and final value
            wasChanged.Should().BeTrue();
            viewModel.TotalAmount.Should().Be(group.TotalAmount.ToCurrencyString());
        }
    }
}