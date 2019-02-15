using DailyExpensesApp.DBConnection;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyExpensesApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResetPasswordPage : ContentPage
	{
		public ResetPasswordPage ()
		{
			InitializeComponent ();
           
            ButtonUpdatePassword.Text = "Update password";
            EyeImage1.Source = ImageSource.FromResource("DailyExpensesApp.Images.eye.png");
            EyeImage2.Source = ImageSource.FromResource("DailyExpensesApp.Images.eye.png");

        }

        Validations validations = new Validations();
        

        private async void ButtonUpdatePassword_OnClicked(object sender, EventArgs e)
        {

            if (EntryEmail.Text == null || EntryPassword.Text == null || EntryConfirmPassword.Text == null)
            {
                LabelInformation.Text = "Empty fields";
                LabelInformation.IsVisible = true;
              
            }
            else
            {



                try
                {
                    Users user = new Users()
                    {

                        Email = EntryEmail.Text,
                        Password = EntryPassword.Text

                    };

                    using (SQLiteConnection connection = new SQLiteConnection(App.filepath1))
                    {
                        await PopupNavigation.Instance.PushAsync(new PopupView());
                        var userx = connection.Table<Users>();

                        var user2 = userx.Where(x => x.Email == EntryEmail.Text)
                            .FirstOrDefault(); //Linq Query 

                       
                            if (EntryPassword.Text != user2.Password)
                            {
                                if (user2.Email == EntryEmail.Text)
                                {
                                    if (EntryPassword.Text == EntryConfirmPassword.Text)
                                    {
                                    if (validations.ValidatePassword(EntryPassword.Text) == true &&
                                        validations.ValidatePassword(EntryConfirmPassword.Text))
                                    {
                                       
                                        LabelInformation.IsVisible = false;
                                      

                                       
                                        user2.Email = EntryEmail.Text;
                                        user2.Password = EntryPassword.Text;
                                        connection.Update(user2);
                                        await PopupNavigation.Instance.PopAsync();
                                        var updated = await DisplayAlert("Success", "Password successfully updated. Return to login?", "ok", "cancel");
                                        if (updated)
                                        {
                                            await Navigation.PushAsync(new LoginPage());
                                        }
                                    }
                                    else
                                    {
                                        await PopupNavigation.Instance.PopAsync();
                                        LabelInformation.Text = "Password fields are not in the correct format";
                                        LabelInformation.IsVisible = true;
                                       
                                    }

                                }
                                else
                                {
                                    await PopupNavigation.Instance.PopAsync();
                                    LabelInformation.Text = "Password doesnt match";
                                    LabelInformation.IsVisible = true;
                                   
                                }

                            }



                                else
                            {
                                await PopupNavigation.Instance.PopAsync();
                                LabelInformation.IsVisible = false;
                                    await DisplayAlert("Reset error", "User not available", "OK");
                                    
                            }
                            }
                            else
                        {
                            await PopupNavigation.Instance.PopAsync();
                            LabelInformation.IsVisible = false;
                                await DisplayAlert("Reset error", "You've entered the previous password", "OK");
                                
                        }
                       

                    }


                }
                catch (Exception)
                {
                    await PopupNavigation.Instance.PopAsync();
                    LabelInformation.IsVisible = false;
                    EntryEmail = null;
                    await DisplayAlert("Reset error", "User not available", "OK");
                }


               
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

        private void LoginButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
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