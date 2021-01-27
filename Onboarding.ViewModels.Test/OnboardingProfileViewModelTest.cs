using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Onboarding.Models;
using Onboarding.Models.Builders;
using Xunit;

namespace Onboarding.ViewModels.Test
{
    public class OnboardingProfileViewModelTest
    {
        readonly Budget budget;
        readonly BudgetViewModel budgetViewModel;
        readonly OnboardingProfile profile;
        
        public OnboardingProfileViewModelTest()
        {
            budget = BudgetBuilder.Build();
            budgetViewModel = new BudgetViewModel(budget);
            profile = OnboardingProfileBuilder.Build(budget);
        }

        [Fact]
        public void Constructor_ValidModel_ExpectPresentation()
        {
            // create VMs for all goal and status items
            var expectedGoals = new List<TitledIconViewModel>();
            var expectedStatus = new List<TitledIconViewModel>();
            profile.Goals.ToList().ForEach(goal => expectedGoals.Add(new TitledIconViewModel(goal)));
            profile.Status.ToList().ForEach(status => expectedStatus.Add(new TitledIconViewModel(status)));
            
            //Test: Construct View Model
            var viewModel = new OnboardingProfileViewModel(profile, budgetViewModel);

            viewModel.Budget.Should().Be(budgetViewModel);
            viewModel.Status.Should().BeEquivalentTo(expectedStatus);
            viewModel.Goals.Should().BeEquivalentTo(expectedGoals);
            
        }

        [Fact]
        public void LogSelectedStatus_ExpectStatusSentToSync()
        {
            var viewModel = new OnboardingProfileViewModel(profile, budgetViewModel);
            
            viewModel.LogSelectedStatus();
        }

        [Fact]
        public void LogSelectedGoals_ExpectGoalsSentToSync()
        {
            var viewModel = new OnboardingProfileViewModel(profile, budgetViewModel);
            
            viewModel.LogSelectedGoals();
        }

    }
}