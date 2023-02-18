using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android;
using Android.Gms.Ads;
using Android.Content;
using Android.Net;
using Android.Provider;

namespace TextEditor.Droid
{
    [Activity(Label = "Simple Text File Editor", Icon = "@mipmap/textIcon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        int RequestID = 0;
        readonly string[] StoragePermission =
        {
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.ReadExternalStorage
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#8790d6"));
            //Window.SetNavigationBarColor(Android.Graphics.Color.ParseColor("#eeaa7b"));
            MobileAds.Initialize(ApplicationContext);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnStart()
        {
            base.OnStart();
            if ((int)Build.VERSION.SdkInt >= 21)
            {
                if (CheckSelfPermission(Manifest.Permission.WriteExternalStorage) !=
                    Permission.Granted)
                {
                    RequestPermissions(StoragePermission, RequestID);
                }
            }
        }
    }
}