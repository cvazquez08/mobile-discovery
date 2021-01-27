using FluentAssertions;
using Xunit;

namespace Onboarding.Models.Test
{
    public class BudgetGroupPresentationTest
    {
        [Fact]
        public void Constructor_DefaultValues_ExpectAssignment()
        {
            BudgetGroupPresentation presentation = new BudgetGroupPresentation();

            presentation.ColorKey.Should().Be(BudgetGroupPresentation.DefaultColorKey);
            presentation.IconSource.Should().Be("");
            presentation.Description.Should().Be("");
            presentation.Subtitle.Should().BeEquivalentTo(BudgetGroupPresentation.DefaultSubtitle);
            presentation.AddItemText.Should().Be(BudgetGroupPresentation.DefaultAddItemText);
            presentation.CustomTitle.Should().Be("");
            presentation.HasSecondaryHeader.Should().Be(false);
            presentation.HasDateOnHeader.Should().Be(false);
        }

        [Fact]
        public void Constructor_ValidParams_ExpectAssignment()
        {
            string colorKey = "color key";
            string iconSource = "icon path";
            string description = "a description";
            string subtitle = "great subtitle";
            string addItemText = "different text in the add item area";
            string customTitle = "a custom title";
            bool hasSecondaryHeader = true;
            bool hasDateOnHeader = true;

            BudgetGroupPresentation presentation = new BudgetGroupPresentation(
                colorKey, 
                iconSource, 
                description,
                subtitle, 
                addItemText, 
                customTitle, 
                hasSecondaryHeader, 
                hasDateOnHeader);

            presentation.ColorKey.Should().Be(colorKey);
            presentation.IconSource.Should().Be(iconSource);
            presentation.Description.Should().Be(description);
            presentation.Subtitle.Should().BeEquivalentTo(subtitle);
            presentation.AddItemText.Should().Be(addItemText);
            presentation.CustomTitle.Should().Be(customTitle);
            presentation.HasSecondaryHeader.Should().Be(hasSecondaryHeader);
            presentation.HasDateOnHeader.Should().Be(hasDateOnHeader);
        }
    }
}