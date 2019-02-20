using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DailyExpensesApp.DBConnection;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using System.Runtime.Serialization;
using DailyExpensesApp.Models;

namespace DailyExpensesApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
    {
       

		public LoginPage ()
		{

			InitializeComponent ();
         
            EyeImage.Source = ImageSource.FromResource("DailyExpensesApp.Images.eye.png");
            ImageLogo.Source = ImageSource.FromResource("DailyExpensesApp.Images.logo.png");


        }
        public LoginPage(String username, String password)
        {
           
            InitializeComponent();
            
            EntryEmail.Text = username;
            EntryPassword.Text = password;
            EyeImage.Source = ImageSource.FromResource("DailyExpensesApp.Images.eye.png");
            ImageLogo.Source = ImageSource.FromResource("DailyExpensesApp.Images.logo.png");

        }

        

         private void ForgotButton_OnClicked(object sender, EventArgs e)
         {
             Navigation.PushAsync(new ResetPasswordPage());
         }




        

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
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

        private void EntryPassword_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            LabelInformation.IsVisible = false;

            if (EntryPassword.Text != "")
            {
                EyeImage.IsVisible = true;
            }
            else
            {
                EyeImage.IsVisible = false;
            }
        }

        private void EntryEmail_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            LabelInformation.IsVisible = false;
        }
    }

   
}
