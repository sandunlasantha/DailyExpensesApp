using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DailyExpensesApp.DBConnection;
using Rg.Plugins.Popup.Services;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyExpensesApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrationPage : ContentPage
	{
       
        public RegistrationPage()
        {
            InitializeComponent();
            EyeImage1.Source = ImageSource.FromResource("DailyExpensesApp.Images.eye.png");
            EyeImage2.Source = ImageSource.FromResource("DailyExpensesApp.Images.eye.png");
            ImageLogo.Source = ImageSource.FromResource("DailyExpensesApp.Images.logo.png");

        }

        public RegistrationPage (string v)
		{
			InitializeComponent ();
            EyeImage1.Source = ImageSource.FromResource("DailyExpensesApp.Images.eye.png");
            EyeImage2.Source = ImageSource.FromResource("DailyExpensesApp.Images.eye.png");
            ImageLogo.Source = ImageSource.FromResource("DailyExpensesApp.Images.logo.png");

            EntryEmail.Text = v;
        }

         async void Button_OnClicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new LoginPage());
        }

         private void TapGestureRecognizer1_OnTapped(object sender, EventArgs e)
         {
             if (EntryPassword.IsPassword == true)
             {
                 EntryPassword.IsPassword = false;
             }
             else
             {
                 EntryPassword.IsPassword = true;
             }
         }
         private void TapGestureRecognizer2_OnTapped(object sender, EventArgs e)
         {
             if (EntryConfirmPassword.IsPassword == true)
             {
                 EntryConfirmPassword.IsPassword = false;
             }
             else
             {
                 EntryConfirmPassword.IsPassword = true;
             }
         }

         private void EntryPassword_OnTextChanged(object sender, TextChangedEventArgs e)
         {
            if (EntryPassword.Text != "")
            {
                EyeImage1.IsVisible = true;
            }
            else
            {
                EyeImage1.IsVisible = false;
            }
        }

         private void EntryConfirmPassword_OnTextChanged(object sender, TextChangedEventArgs e)
         {
             if (EntryConfirmPassword.Text != "")
             {
                 EyeImage2.IsVisible = true;
             }
             else
             {
                 EyeImage2.IsVisible = false;
             }
        }
    }
}