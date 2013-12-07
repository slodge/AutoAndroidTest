using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using Thing.Core.ViewModels;

namespace Thing.Droid.Views
{
    [Activity(Label = "View for FirstViewModel")]
    public class FirstView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FirstView);
        }

        protected override void OnResume()
        {
            ((FirstViewModel)ViewModel).OnShow();
            base.OnResume();
        }
    }

    [Activity(Label = "View for SecondViewModel")]
    public class SecondView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SecondView);
        }
    }
}