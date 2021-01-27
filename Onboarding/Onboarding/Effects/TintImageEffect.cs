using Xamarin.Forms;

namespace Onboarding.Effects
{
    /// <summary>
    /// From https://www.c-sharpcorner.com/article/xamarin-forms-tint-image-using-effects/ and https://www.c-sharpcorner.com/article/xamarin-forms-use-bindableproperty-in-effects/
    /// </summary>
    public class TintImage : RoutingEffect
    {
        public Color TintColor { get; private set; }
        public TintImage(Color color) : base($"OnboardingEffects.{nameof(TintImage)}")
        {
            TintColor = color;
        }
    }
}