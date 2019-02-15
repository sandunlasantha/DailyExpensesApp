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

        async void Button_OnClicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new RegistrationPage());
        }

         private void ForgotButton_OnClicked(object sender, EventArgs e)
         {
             Navigation.PushAsync(new ResetPasswordPage());
         }
         

         

        private async void LoginButton_OnClicked(object sender, EventArgs e)
         {
             Validations validations = new Validations();
             
             {

                 try
                 {

                     if (EntryPassword.Text == null || EntryEmail.Text == null)
                     {
                         throw new ArgumentNullException();
                     }
                     if (validations.ValidateEmail(EntryEmail.Text)==false)
                     {
                         throw new AccessViolationException();
                     }

                    if (validations.ValidatePassword(EntryPassword.Text))
                     {
                         

                         using (SQLiteConnection connection = new SQLiteConnection(App.filepath1))
                         {


                            
                             var user = connection.Table<Users>();

                             var user1 = user.Where(x => x.Email == EntryEmail.Text && x.Password == EntryPassword.Text)
                                 .FirstOrDefault(); //Linq Query 

                             var user2 = user.Where(x => x.Email == EntryEmail.Text)
                                 .FirstOrDefault(); //Linq Query 

                             if (user2.Email == EntryEmail.Text && user2.Password != EntryPassword.Text)
                             {
                                 throw new EmailTruePasswordIncorrectException();
                                
                            }

                            if (user1.Email == EntryEmail.Text && user1.Password == EntryPassword.Text)
                            {
                            
                                 await PopupNavigation.Instance.PushAsync(new PopupView());

                                await Navigation.PushAsync(new DailyExpenses());

                                }
                                else
                                {
                                    await DisplayAlert("Alert", "Error", "OK");
                                }
                          
                             

                            
                             


                         }
                     }


                     else
                     {
                         LabelInformation.IsVisible = true;
                         LabelInformation.Text = "Password should contain both uppercase, lowercase characters and at least one decimal number and more than 6 characters";
                       
                     }
                 }


                 catch (ArgumentNullException)
                 {
                     LabelInformation.IsVisible = true;
                     LabelInformation.Text = "Username or password is empty";
                    
                 }
                 catch(AccessViolationException)
                 {
                     LabelInformation.IsVisible = true;
                     LabelInformation.Text = "Email is not in the correct format";
                   
                }
                 catch (AbandonedMutexException)
                 {
                     await DisplayAlert("Alert", "Forgot password?", "Reset","Cancel");
                 }
                 catch (NullReferenceException)
                 {
                    var alert1 = await DisplayAlert("Alert", "You have not registered yet", "Register", "Cancel");
                    if (alert1)
                    {
                        await Navigation.PushAsync(new RegistrationPage(EntryEmail.Text));
                    }

                   
                 }
                
                     catch (EmailTruePasswordIncorrectException)
                 {
                     var alert = await DisplayAlert("Login error", "Forgot password?", "Yes","Cancel");
                     if (alert)
                     {
                        await Navigation.PushAsync(new ResetPasswordPage());
                     }
                }
                 catch (SQLiteException)
                 {
                    var alert1 = await DisplayAlert("Alert", "You have not registered yet", "Register", "Cancel");
                    if (alert1)
                    {
                        await Navigation.PushAsync(new RegistrationPage(EntryEmail.Text));
                    }
                }




            }
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

    [Serializable]
    internal class EmailTruePasswordIncorrectException : Exception
    {
        public EmailTruePasswordIncorrectException()
        {
        }

        public EmailTruePasswordIncorrectException(string message) : base(message)
        {
        }

        public EmailTruePasswordIncorrectException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmailTruePasswordIncorrectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
