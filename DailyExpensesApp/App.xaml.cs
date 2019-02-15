using DailyExpensesApp.Views;
using System;
using DailyExpensesApp.DBConnection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DailyExpensesApp
{
    public partial class App : Application
    {
        public static String filepath1;

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }
        public App(String path)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            filepath1 = path;
        }

        


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
