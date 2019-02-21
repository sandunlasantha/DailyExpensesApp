using DailyExpensesApp.DBConnection;
using DailyExpensesApp.Views;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Messaging;

namespace DailyExpensesApp.ViewModels
{
    class ResetPasswordViewModel : INotifyPropertyChanged
    {
        private string _email;
        private string _newPassword;
        private string _newConfirmPassword;
        private string _labelMessage;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));



        }

        public String Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public String NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value; OnPropertyChanged(); }

        }
        public String NewConfirmPassword
        {
            get { return _newConfirmPassword; }
            set { _newConfirmPassword = value; OnPropertyChanged(); }
        }

       

        public string LabelMessage { get { return _labelMessage; } set { _labelMessage = value; OnPropertyChanged(); } }




        public ICommand UpdatePasswordCommand { get; set; }

        public ResetPasswordViewModel()
        {
            UpdatePasswordCommand = new Command(UpdatePassword);
        }

        public async void UpdatePassword()
        {
            Validations validations = new Validations();

            if (_email == null || _newPassword == null || _newConfirmPassword == null)
            {
                
            
              
                LabelMessage = "Empty Fields";

            }
            else
            {



                try
                {
                    Users user = new Users()
                    {

                        Email = _email,
                        Password = _newPassword

                    };

                    using (SQLiteConnection connection = new SQLiteConnection(App.filepath1))
                    {
                        await PopupNavigation.Instance.PushAsync(new PopupView());
                        var userx = connection.Table<Users>();

                        var user2 = userx.Where(x => x.Email == _email)
                            .FirstOrDefault(); 


                        if (_newPassword != user2.Password)
                        {
                            if (user2.Email== _email)
                            {
                                if (_newPassword == NewConfirmPassword)
                                {
                                    if (validations.ValidatePassword(_newPassword) == true &&
                                        validations.ValidatePassword(_newConfirmPassword))
                                    {

                                                user2.Email = _email;
                                            user2.Password = _newPassword;
                                            connection.Update(user2);
                                            await PopupNavigation.Instance.PopAsync();

                                            var updated =await Application.Current.MainPage.DisplayAlert("Success", "Password successfully updated. Return to login?", "ok", "cancel");

                                            if (updated)
                                            {
                                                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                                            }
                                 

                                        
                                    }
                                    else
                                    {
                                        await PopupNavigation.Instance.PopAsync();
                                        LabelMessage = "Password fields are not in the correct format";
                                       
                                      
                                    }

                                }
                                else
                                {
                                    await PopupNavigation.Instance.PopAsync();
                                    LabelMessage = "Password doesnt match";
                                  
                                 
                                }

                            }



                            else
                            {
                                await PopupNavigation.Instance.PopAsync();
                               LabelMessage = "";
                                await Application.Current.MainPage.DisplayAlert("Alert", "User not available", "OK");
                            }
                        }
                        else
                        {
                            await PopupNavigation.Instance.PopAsync();
                             LabelMessage = "";
                            await Application.Current.MainPage.DisplayAlert("Alert", "You've entered the previous password", "OK");
                        }


                    }


                }
                catch (NullReferenceException)
                {
                    await PopupNavigation.Instance.PopAsync();
                    await Application.Current.MainPage.DisplayAlert("Update error", "User not available", "Ok");

                   
                }
                catch (SQLiteException)
                {
                    var alert = await Application.Current.MainPage.DisplayAlert("Login error", "You have not registered yet", "Register", "Cancel");

                    if (alert)
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new RegistrationPage());
                    }
                }
                catch (Exception excion)
                {
                    await PopupNavigation.Instance.PopAsync();
                  
                    Email = null;
                    LabelMessage = "";
                    await Application.Current.MainPage.DisplayAlert("Alert", excion.ToString(), "OK");
                }



            }
        }

    }
}
