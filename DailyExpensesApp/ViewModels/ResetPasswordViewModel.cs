using DailyExpensesApp.DBConnection;
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

       

        public string LabelMessage { get { return _labelMessage; } set { _labelMessage = value; OnPropertyChanged("ytfytfytf"); } }




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
                // LabelInformation.Text = "Empty fields";
                // LabelInformation.IsVisible = true;
                Application.Current.MainPage.DisplayAlert("Alert", "Empty fields", "OK");

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
                        //await PopupNavigation.Instance.PushAsync(new PopupView());
                        var userx = connection.Table<Users>();

                        var user2 = userx.Where(x => x.Email == _email)
                            .FirstOrDefault(); //Linq Query 


                        if (_newPassword != user2.Password)
                        {
                            if (user2.Email == _email)
                            {
                                if (_newPassword == NewConfirmPassword)
                                {
                                    if (validations.ValidatePassword(_newPassword) == true &&
                                        validations.ValidatePassword(_newConfirmPassword))
                                    {

                                        //LabelInformation.IsVisible = false;



                                        user2.Email = _email;
                                        user2.Password = _newPassword;
                                        connection.Update(user2);
                                        //await PopupNavigation.Instance.PopAsync();
                                        // var updated = await DisplayAlert("Success", "Password successfully updated. Return to login?", "ok", "cancel");
                                        //if (updated)
                                        // {
                                        //     await Navigation.PushAsync(new LoginPage());
                                        // }
                                        Application.Current.MainPage.DisplayAlert("Alert", "Password successfully updated. Return to login?", "OK");
                                    }
                                    else
                                    {
                                        // await PopupNavigation.Instance.PopAsync();
                                        // LabelInformation.Text = "Password fields are not in the correct format";
                                        // LabelInformation.IsVisible = true;
                                        Application.Current.MainPage.DisplayAlert("Alert", "Password fields are not in the correct format", "OK");
                                    }

                                }
                                else
                                {
                                    //  await PopupNavigation.Instance.PopAsync();
                                    //   LabelInformation.Text = "Password doesnt match";
                                    //  LabelInformation.IsVisible = true;
                                    Application.Current.MainPage.DisplayAlert("Alert", "Password doesnt match", "OK");
                                }

                            }



                            else
                            {
                                //   await PopupNavigation.Instance.PopAsync();
                                //   LabelInformation.IsVisible = false;
                                //   await DisplayAlert("Reset error", "User not available", "OK");
                                Application.Current.MainPage.DisplayAlert("Alert", "User not available", "OK");
                            }
                        }
                        else
                        {
                            //   await PopupNavigation.Instance.PopAsync();
                            //   LabelInformation.IsVisible = false;
                            //   await DisplayAlert("Reset error", "You've entered the previous password", "OK");
                            Application.Current.MainPage.DisplayAlert("Alert", "You've entered the previous password", "OK");
                        }


                    }


                }
                catch (Exception)
                {
                    //  await PopupNavigation.Instance.PopAsync();
                    //  LabelInformation.IsVisible = false;
                    //  EntryEmail = null;
                    //   await DisplayAlert("Reset error", "User not available", "OK");
                    Application.Current.MainPage.DisplayAlert("Alert", "User not available", "OK");
                }



            }
        }

    }
}
