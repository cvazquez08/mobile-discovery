using System;
using System.Collections.Generic;
using System.Linq;
using Onboarding.Models;

namespace Onboarding.ViewModels
{
    public class OnboardingProfileViewModel
    {
        internal readonly OnboardingProfile Profile;
        
        public BudgetViewModel Budget { get; private set; }
        public List<TitledIconViewModel> Goals { get; } = new List<TitledIconViewModel>();
        public List<TitledIconViewModel> Status { get; } = new List<TitledIconViewModel>();

        public OnboardingProfileViewModel(OnboardingProfile profile, BudgetViewModel budgetViewModel)
        {
            Profile = profile ?? throw new ArgumentNullException(nameof(profile));
            Budget = budgetViewModel ?? throw new ArgumentNullException(nameof(budgetViewModel));
            
            // Create VMS for all goal, status and step items
            profile.Goals.ToList().ForEach(goal => Goals.Add(new TitledIconViewModel(goal)));
            profile.Status.ToList().ForEach(status => Status.Add(new TitledIconViewModel(status)));
        }

        public void LogSelectedStatus() => Console.WriteLine("We will log these later");
        public void LogSelectedGoals() => Console.WriteLine("We will log these later");

    }
}