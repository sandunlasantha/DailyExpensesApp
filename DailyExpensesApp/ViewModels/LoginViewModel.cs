using DailyExpensesApp.DBConnection;
using DailyExpensesApp.Views;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace DailyExpensesApp.Models
{
    class LoginViewModel : INotifyPropertyChanged
    {

        private string _email;
        private string _password;
        private string _labelMessage;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Email { get { return _email; } set { _email = value;
            OnPropertyChanged();
        } }
        public string Password { get { return _password; } set { _password = value;
            OnPropertyChanged();
        } }
        public string LabelMessage { get { OnPropertyChanged(); return _labelMessage;} set { _labelMessage = value;  } }

        public ICommand LoginCommand { get; set; }
       

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
        }

        private void Login()
        {
            {

                try
                {

                    if (_password == null || _email == null)
                    {
                        throw new ArgumentNullException();
                    }
                    Validations validations = new Validations();
                    if (validations.ValidateEmail(_email) == false)
                    {
                        throw new AccessViolationException();
                    }

                    if (validations.ValidatePassword(_password))
                    {


                        using (SQLiteConnection connection = new SQLiteConnection(App.filepath1))
                        {



                            var user = connection.Table<Users>();

                            var user1 = user.Where(x => x.Email == _email && x.Password == _password)
                                .FirstOrDefault();

                            var user2 = user.Where(x => x.Email == _email)
                                .FirstOrDefault();

                            if (user2.Email == _email && user2.Password != _password)
                            {

                                throw new EmailTruePasswordIncorrectException();

                            }

                            if (user1.Email == _email && user1.Password == _password)
                            {

                                PopupNavigation.Instance.PushAsync(new PopupView());

                                // Navigation.PushAsync(new DailyExpenses());

                            }
                            else
                            {
                                Application.Current.MainPage.DisplayAlert("Alert", "Error", "OK");
                                //   DisplayAlert("Alert", "Error", "OK");

                            }

                        }
                    }


                    else
                    {

                        _labelMessage = "Password should contain both uppercase, lowercase characters and at least one decimal number and more than 6 characters";

                    }
                }


                catch (ArgumentNullException)
                {

                    _labelMessage = "Username or password is empty";
                    Application.Current.MainPage.DisplayAlert("Alert", "Username or password is empty", "Ok");

                }
                catch (AccessViolationException)
                {

                    _labelMessage = "Email is not in the correct format";

                }
                catch (AbandonedMutexException)
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Forgot password?", "Reset", "Cancel");
                    //DisplayAlert("Alert", "Forgot password?", "Reset", "Cancel");
                }
                catch (NullReferenceException)
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "You have not registered yet", "Register", "Cancel");




                    //var alert1 = await DisplayAlert("Alert", "You have not registered yet", "Register", "Cancel");
                    //if (alert1)
                    //{
                    //    await Navigation.PushAsync(new RegistrationPage(EntryEmail.Text));
                    //}


                }

                catch (EmailTruePasswordIncorrectException)
                {
                    Application.Current.MainPage.DisplayAlert("Login error", "Forgot password?", "Yes", "Cancel");







                    //var alert = await DisplayAlert("Login error", "Forgot password?", "Yes", "Cancel");
                    //if (alert)
                    //{
                    //    await Navigation.PushAsync(new ResetPasswordPage());
                    //}
                }
                catch (SQLiteException)
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "You have not registered yet", "Register", "Cancel");







                    //var alert1 = await DisplayAlert("Alert", "You have not registered yet", "Register", "Cancel");
                    //if (alert1)
                    //{
                    //    await Navigation.PushAsync(new RegistrationPage(EntryEmail.Text));
                    //}
                }




            }
        }
    }
}
