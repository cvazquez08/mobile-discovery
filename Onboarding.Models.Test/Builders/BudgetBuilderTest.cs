using System.Runtime.InteropServices;
using FluentAssertions;
using Onboarding.Models.Builders;
using Xunit;

namespace Onboarding.Models.Test.Builders
{
    public class BudgetBuilderTest
    {
        [Fact]
        public void Build_ExpectPopulatedBudget()
        {
            var budget = BudgetBuilder.Build();

            budget.IncomeGroup.Should().NotBeNull();
        }
        
    }
}