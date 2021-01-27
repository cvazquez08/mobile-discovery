using System;
using System.ComponentModel;
using System.IO;
using Onboarding.Models;
using PropertyChanged;

namespace Onboarding.ViewModels
{
    public class TitledIconViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal readonly TitledIcon TitledIcon;

        public string Title => TitledIcon.Title;
        public string ImageSource => TitledIcon.ImageSource;

        public bool IsSelected
        {
            get => TitledIcon.IsSelected;
            set => TitledIcon.IsSelected = value;
        }

        [DependsOn(nameof(IsSelected))] public bool IsNotSelected => !IsSelected;

        public TitledIconViewModel(TitledIcon titledIcon)
        {
            TitledIcon = titledIcon ?? throw new ArgumentNullException(nameof(titledIcon));
            
            // NOTE: this only works as the names in this VM are the same as the model
            titledIcon.PropertyChanged += (o, e) => PropertyChanged?.Invoke(this, e); 
        }
    }
}