using System.ComponentModel;

namespace Onboarding.Models
{
    public class TitledIcon : INotifyPropertyChanged
    {
        public readonly string Id;
        public readonly string Title;
        public readonly string ImageSource;

        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsSelected { get; set; }

        public TitledIcon(string id, string title, string imageSource)
        {
            Id = id;
            Title = title;
            ImageSource = imageSource;
        }

    }
}