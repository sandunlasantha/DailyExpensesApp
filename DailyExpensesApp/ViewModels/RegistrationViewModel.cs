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
     

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public String Name { get {return _name; }  set { _name = value; } }
        public String Email { get { return _email; } set { _email = value; } }
        public String Password { get { return _password; } set { _password = value; } }
        public String ConfirmPassword { get { return _confirmPassword; } set { _confirmPassword = value; } }

        public ICommand RegistrationCommand { get; set; }

        public RegistrationViewModel()
        {
            RegistrationCommand = new Command(Register);
        }

        private void Register()
        {
            try
            {
                if (_password == null || _confirmPassword == null || _email == null ||
                    _name== null)
                {
                    throw new ArgumentNullException();
                }

                if (validations.ValidateEmail(_email) == false)
                {
                    throw new AbandonedMutexException();
                }

                if (_password != _confirmPassword)
                {
                    throw new AmbiguousMatchException();
                }

                if (validations.ValidatePassword(_password) == false)
                {
                    throw new ArithmeticException();
                }




                Users user = new Users()
                {
                    Name = _name,
                    Email = _email,
                    Password = _password

                };
                using (SQLiteConnection connection = new SQLiteConnection(App.filepath1))
                {
                   // await PopupNavigation.Instance.PushAsync(new PopupView());

                    connection.CreateTable<Users>();




                    connection.Insert(user);

                    //await PopupNavigation.Instance.PopAsync();

                    //  await DisplayAlert("Message", "Registration successfull", "Ok");
                    Application.Current.MainPage.DisplayAlert("Alert", "Registration successfull", "OK");
                    _password = null;
                    _email = null;
                   _confirmPassword = null;
                    _name = null;




                }


            }
            catch (ArgumentNullException)
            {
                // LabelInformation.IsVisible = true;
                //  LabelInformation.Text = "Missing fields";
                Application.Current.MainPage.DisplayAlert("Alert", "Missing fields", "OK");
            }

            catch (AbandonedMutexException)
            {
                // LabelInformation.IsVisible = true;
                Application.Current.MainPage.DisplayAlert("Alert", "Email is not in the correct format", "OK");
                //LabelInformation.Text = "Email is not in the correct format";
                //EntryEmail.Text = "";
            }

            catch (ArithmeticException)
            {
                // LabelInformation.IsVisible = true;
                Application.Current.MainPage.DisplayAlert("Alert", "Password should contain both uppercase,lowercase characters,at least one number and more than 6 characters", "OK");
                // LabelInformation.Text = "Password should contain both uppercase,lowercase characters,at least one number and more than 6 characters";
            }

            catch (AmbiguousMatchException)
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Password doesn't match", "OK");
                // await DisplayAlert("Alert", "Password doesn't match", "OK");
                //  LabelInformation.IsVisible = true;
                //  LabelInformation.Text = "Password doesn't match";
                //  EntryPassword.Text = "";
                //  EntryConfirmPassword.Text = "";
            }
            catch (SQLiteException)
            {
                //  await DisplayAlert("Registration error", "Username is already taken", "OK");
                //  EntryEmail.Text = "";
                Application.Current.MainPage.DisplayAlert("Alert", "Username is already taken", "OK");
            }
        }
    }
}
