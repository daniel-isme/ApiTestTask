using ApiTestTask.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ApiTestTask
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var error = DataViewModel.Error;
            if (error != null)
            {
                DisplayAlert(error.Title, error.Message, error.Cancel);
            }
        }
    }
}
