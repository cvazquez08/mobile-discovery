using FluentAssertions;
using Xunit;

namespace Onboarding.Models.Test
{
    public class TitledIconTest
    {
        [Fact]
        public void Constructor_ValidParameters_ExpectAssignment()
        {
            string imageSource = "some uri";
            string title = "test name";
            string id = "id_that_goes_remote";

            TitledIcon titledIcon = new TitledIcon(id, title, imageSource);

            titledIcon.Id.Should().Be(id);
            titledIcon.Title.Should().Be(title);
            titledIcon.ImageSource.Should().Be(imageSource);
        }

        [Fact]
        public void IsSelected_ValueChange_ExpectPropertyChanged()
        {
            string imageSource = "some uri";
            string title = "test name";
            string id = "id_that_goes_remote";
            TitledIcon titledIcon = new TitledIcon(id, title, imageSource);
            titledIcon.IsSelected.Should().BeFalse();

            bool wasChanged = false;
            titledIcon.PropertyChanged += (o, e) =>
            {
                if (e.PropertyName == nameof(titledIcon.IsSelected))
                {
                    wasChanged = true;
                }
            };

            titledIcon.IsSelected = true;

            wasChanged.Should().BeTrue();
        }

    }
}