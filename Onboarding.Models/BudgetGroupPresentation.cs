namespace Onboarding.Models
{
    public class BudgetGroupPresentation
    {
        public const string DefaultColorKey = "OnboardingBlue";
        public const string DefaultSubtitle = "Don't worry, you can edit these later";
        public const string DefaultAddItemText = "+ Add Item";

        public readonly string ColorKey; // TODO: this should be an enumertor or typed object
        public readonly string IconSource;
        public readonly string Description;
        public readonly string Subtitle;
        public readonly string AddItemText;
        public readonly string CustomTitle;
        public readonly bool HasSecondaryHeader;
        public readonly bool HasDateOnHeader;

        public BudgetGroupPresentation(
            string colorKey = DefaultColorKey,
            string iconSource = "",
            string description = "",
            string subtitle = DefaultSubtitle,
            string addItemText = DefaultAddItemText,
            string customTitle = "",
            bool hasSecondaryHeader = false,
            bool hasDateOnHeader = false)
        {
            ColorKey = colorKey;
            IconSource = iconSource;
            Description = description;
            Subtitle = subtitle;
            AddItemText = addItemText;
            CustomTitle = customTitle;
            HasSecondaryHeader = hasSecondaryHeader;
            HasDateOnHeader = hasDateOnHeader;
        }
    }
}