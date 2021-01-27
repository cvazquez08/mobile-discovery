﻿using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Onboarding.Android;

namespace Onboarding.Droid
{

    [Activity(Label = "Onboarding", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, Application.IActivityLifecycleCallbacks
    {
        public static Activity CurrentActivity { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
        
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            // Status bar color changes
            // TODO: investigate easier way
            Window.SetStatusBarColor(Color.White);
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                var flag = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
                Window.DecorView.SystemUiVisibility = true ? flag : 0;
            }

            Application.RegisterActivityLifecycleCallbacks(this);

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        // From https://montemagno.com/access-the-current-android-activity-from-anywhere/
        #region Activity Lifecycle Callbacks

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CurrentActivity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
            MainActivity.CurrentActivity = activity;
        }

        public void OnActivityResumed(Activity activity)
        {
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            MainActivity.CurrentActivity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }

        #endregion Activity Lifecycle Callbacks
    }
}