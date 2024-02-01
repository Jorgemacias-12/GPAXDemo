using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace GPAXDemo
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ShowGeoLocation();

            Btn_Record.Clicked += Btn_Record_Clicked;
        }

        private void Btn_Record_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Sexoo", "¿Cuando?", "Hoy");
        }

        private async Task ShowGeoLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location is null) return;

                Lbl_Location.Text = $"Location: {location.Latitude} {location.Longitude} {location.Speed} {location.Timestamp}";
            }
            catch
            {
                
            }
        }
    }
}
