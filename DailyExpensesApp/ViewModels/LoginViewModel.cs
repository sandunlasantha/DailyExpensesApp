using DailyExpensesApp.DBConnection;
using DailyExpensesApp.Models;
using DailyExpensesApp.Views;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace DailyExpensesApp.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        Validations validations = new Validations();
        private string _email;
        private string _password;
        private string _labelMessage;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public String Email { get { return _email; } set { _email = value; } }
        public String Password { get { return _password; } set { _password = value;} }
        public string LabelMessage { get {  return _labelMessage;} set { _labelMessage = value; OnPropertyChanged(); } }
        public ICommand LoginCommand { get; set; }
        public ICommand SignupCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
            SignupCommand = new Command(SignUp);
        }

        private void SignUp()
        {
            Application.Current.MainPage.Navigation.PushAsync(new RegistrationPage());
        }

        private async void  Login()
        {
            try
            {
                if (_email == null || _password == null)
                {
                    throw new EmailNullPasswordNullException();
                }

                if (validations.ValidateEmail(_email) == false)
                {
                    throw new InvalidEmailException();
                }

                if (validations.ValidatePassword(_password) == false)
                {
                    throw new InvalidPasswordException();
                }

                using (SQLiteConnection connection = new SQLiteConnection(App.filepath1))
                {

                    var user = connection.Table<Users>();
                    var user1 = user.Where(x => x.Email == _email && x.Password == _password).FirstOrDefault();
                    var user2 = user.Where(x => x.Email == _email).FirstOrDefault();

                    if (user2.Email==_email && user2.Password != _password)
                    {

                        throw new EmailTruePasswordFalseException();

                    }

                    if (user1 != null) 
                    {
                        await PopupNavigation.Instance.PushAsync(new PopupView());
                        await Application.Current.MainPage.Navigation.PushAsync(new DailyExpenses());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Error", "OK");
                    }



                }
            }
            catch (EmailNullPasswordNullException)
            {
                LabelMessage = "Username or password is empty";
            }
            catch (InvalidEmailException)
            {
                LabelMessage = "Email is not in the correct format";
                await Application.Current.MainPage.DisplayAlert("Login error", "Email is not in the correct format", "Ok");
            }
            catch (InvalidPasswordException)
            {
                await Application.Current.MainPage.DisplayAlert("Login error", "Password is not in the correct format", "Ok");
                LabelMessage = "Password is not in the correct format";
            }
            catch (NullReferenceException)
            {
                LabelMessage = "User not available";
               

                var alert = await Application.Current.MainPage.DisplayAlert("Login error", "User not available", "Sign up", "Cancel");

                if (alert)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new RegistrationPage());
                }




            }
            catch (EmailTruePasswordFalseException)
            {
                var alert = await Application.Current.MainPage.DisplayAlert("Login error", "Forgot password?", "Reset", "Cancel");

                if (alert)
                {
                   await Application.Current.MainPage.Navigation.PushAsync(new ResetPasswordPage());
                }
            }
            catch (SQLiteException)
            {
                var alert = await Application.Current.MainPage.DisplayAlert("Login error", "You have not registered yet", "Register", "Cancel");

                if (alert)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new RegistrationPage());
                }
            }


        }
    }
}
