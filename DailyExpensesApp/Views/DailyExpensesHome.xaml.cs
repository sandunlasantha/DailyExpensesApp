using Rg.Plugins.Popup.Services;
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
	public partial class DailyExpenses : ContentPage
	{
		public DailyExpenses ()
		{
			InitializeComponent ();

            PopupNavigation.Instance.PopAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}