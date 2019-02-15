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
        Validations validations = new Validations();
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

         private async void RegisterButton_OnClicked(object sender, EventArgs e)
         {
             try
             {
                 if (EntryPassword.Text == null || EntryConfirmPassword.Text == null || EntryEmail.Text == null ||
                     EntryName.Text == null)
                 {
                     throw new ArgumentNullException();
                 }

                 if (validations.ValidateEmail(EntryEmail.Text) == false)
                 {
                     throw new AbandonedMutexException();
                 }

                 if (EntryPassword.Text != EntryConfirmPassword.Text)
                 {
                     throw new AmbiguousMatchException();
                 }

                 if (validations.ValidatePassword(EntryPassword.Text) == false)
                 {
                     throw new ArithmeticException();
                 }




                 Users user = new Users()
                 {
                     Name = EntryName.Text,
                     Email = EntryEmail.Text,
                     Password = EntryPassword.Text

                 };
                 using (SQLiteConnection connection = new SQLiteConnection(App.filepath1))
                {
                    await PopupNavigation.Instance.PushAsync(new PopupView());

                    connection.CreateTable<Users>();


                   

                    connection.Insert(user);

                    await PopupNavigation.Instance.PopAsync();

                    await DisplayAlert("Message", "Registration successfull", "Ok");
                    EntryPassword.Text = null;
                    EntryEmail.Text = null;
                    EntryConfirmPassword.Text = null;
                    EntryName.Text = null;




                }


             }
             catch (ArgumentNullException)
             {
                 LabelInformation.IsVisible = true;
                LabelInformation.Text = "Missing fields";
            }

             catch (AbandonedMutexException)
             {
                 LabelInformation.IsVisible = true;
                LabelInformation.Text = "Email is not in the correct format";
                EntryEmail.Text = "";
             }

             catch (ArithmeticException)
             {
                 LabelInformation.IsVisible = true;

                LabelInformation.Text = "Password should contain both uppercase,lowercase characters,at least one number and more than 6 characters";
            }

             catch (AmbiguousMatchException)
             {
                // await DisplayAlert("Alert", "Password doesn't match", "OK");
                LabelInformation.IsVisible = true;
                 LabelInformation.Text = "Password doesn't match";
                 EntryPassword.Text = "";
                 EntryConfirmPassword.Text = "";
             }
             catch (SQLiteException)
             {
                 await DisplayAlert("Registration error","Username is already taken" , "OK");
                 EntryEmail.Text = "";
             }

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