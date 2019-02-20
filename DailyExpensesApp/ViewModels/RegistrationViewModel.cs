using DailyExpensesApp.DBConnection;
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
using DailyExpensesApp.Models;
using Xamarin.Forms;

namespace DailyExpensesApp.ViewModels
{
    class RegistrationViewModel : INotifyPropertyChanged
    {
        Validations validations = new Validations();
        private string _name;
        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _labelTextMessage;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public String Name { get { return _name; } set { _name = value; } }
        public String Email { get { return _email; } set { _email = value; } }
        public String Password { get { return _password; } set { _password = value; } }
        public String ConfirmPassword { get { return _confirmPassword; } set { _confirmPassword = value; } }
        public string LabelTextMessage { get { return _labelTextMessage; } set { _labelTextMessage = value; OnPropertyChanged();} }
        public ICommand RegistrationCommand { get; set; }

        public RegistrationViewModel()
        {
            RegistrationCommand = new Command(Register);
        }

        private async void Register()
        {
            try
            {
                if (_password == null || _confirmPassword == null || _email == null ||
                    _name == null)
                {
                    throw new EmailNullPasswordNullException();
                }

                if (validations.ValidateEmail(_email) == false)
                {
                    throw new InvalidEmailException();
                }

                if (_password != _confirmPassword)
                {
                    throw new PasswordNotMatchException();
                }

                if (validations.ValidatePassword(_password) == false)
                {
                    throw new InvalidPasswordException();
                }




                Users user = new Users()
                {
                    Name = _name,
                    Email = _email,
                    Password = _password

                };

                using (SQLiteConnection connection = new SQLiteConnection(App.filepath1))
                {
                    await PopupNavigation.Instance.PushAsync(new PopupView());

                    connection.CreateTable<Users>();




                    connection.Insert(user);

                    await PopupNavigation.Instance.PopAsync();


                    await Application.Current.MainPage.DisplayAlert("Alert", "Registration successfull", "OK");
                    LabelTextMessage = "";
                    _password = null;
                    _email = null;
                    _confirmPassword = null;
                    _name = null;




                }


            }
            catch (EmailNullPasswordNullException)
            {
                // LabelInformation.IsVisible = true;
                LabelTextMessage = "Missing fields";
               // Application.Current.MainPage.DisplayAlert("Alert", "Missing fields", "OK");
            }

            catch (InvalidEmailException)
            {
                // LabelInformation.IsVisible = true;
              //  Application.Current.MainPage.DisplayAlert("Alert", "Email is not in the correct format", "OK");
                LabelTextMessage = "Email is not in the correct format";
                //EntryEmail.Text = "";
            }

            catch (InvalidPasswordException)
            {
                // LabelInformation.IsVisible = true;
              //  Application.Current.MainPage.DisplayAlert("Alert", "Password should contain both uppercase,lowercase characters,at least one number and more than 6 characters", "OK");
                LabelTextMessage = "Password should contain both uppercase,lowercase characters,at least one number and more than 6 characters";
            }

            catch (PasswordNotMatchException)
            {
               // Application.Current.MainPage.DisplayAlert("Alert", "Password doesn't match", "OK");
                // await DisplayAlert("Alert", "Password doesn't match", "OK");
                //  LabelInformation.IsVisible = true;
                LabelTextMessage = "Password doesn't match";
                //  EntryPassword.Text = "";
                //  EntryConfirmPassword.Text = "";
            }
            catch (SQLiteException)
            {
                //  await DisplayAlert("Registration error", "Username is already taken", "OK");
                //  EntryEmail.Text = "";
                await Application.Current.MainPage.DisplayAlert("Alert", "Username is already taken", "OK");
            }
        }
    }
}
