
using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using System.IO;

namespace DailyExpensesApp.Droid
{
    [Activity(Label = "DailyExpensesApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                Window.SetStatusBarColor(Android.Graphics.Color.Rgb(32,29,48));
            }

           

            string filename = "contact_db.db3";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string completePath = Path.Combine(folderPath, filename);
            LoadApplication(new App(completePath));
        }
    }
}