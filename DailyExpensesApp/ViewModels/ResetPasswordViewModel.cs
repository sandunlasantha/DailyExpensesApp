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

        public void UpdatePassword()
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
                         PopupNavigation.Instance.PushAsync(new PopupView());
                        var userx = connection.Table<Users>();

                        var user2 = userx.Where(x => x.Email == _email)
                            .FirstOrDefault(); 


                        if (_newPassword != user2.Password)
                        {
                            if (user2.Email!=null)
                            {
                                if (_newPassword == NewConfirmPassword)
                                {
                                    if (validations.ValidatePassword(_newPassword) == true &&
                                        validations.ValidatePassword(_newConfirmPassword))
                                    {

                                        user2.Email = _email;
                                        user2.Password = _newPassword;
                                        connection.Update(user2);
                                         PopupNavigation.Instance.PopAsync();
                                        
                                        var updated =  Application.Current.MainPage.DisplayAlert("Success", "Password successfully updated. Return to login?", "ok", "cancel");

                                        if (updated!=null)
                                         {
                                             Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                                         }
                                    }
                                    else
                                    {
                                        PopupNavigation.Instance.PopAsync();
                                        LabelMessage = "Password fields are not in the correct format";
                                       
                                      
                                    }

                                }
                                else
                                {
                                   PopupNavigation.Instance.PopAsync();
                                    LabelMessage = "Password doesnt match";
                                  
                                 
                                }

                            }



                            else
                            {
                               PopupNavigation.Instance.PopAsync();
                               LabelMessage = "";
                                Application.Current.MainPage.DisplayAlert("Alert", "User not available", "OK");
                            }
                        }
                        else
                        {
                             PopupNavigation.Instance.PopAsync();
                             LabelMessage = "";
                            Application.Current.MainPage.DisplayAlert("Alert", "You've entered the previous password", "OK");
                        }


                    }


                }
                catch (Exception)
                {
                    PopupNavigation.Instance.PopAsync();
                  
                    Email = null;
                    LabelMessage = "";
                    Application.Current.MainPage.DisplayAlert("Alert", "User not available", "OK");
                }



            }
        }

    }
}
