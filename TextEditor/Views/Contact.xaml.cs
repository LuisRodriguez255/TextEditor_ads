using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TextEditor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contact : ContentPage
    {
        public Contact()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync("luisrodriguezbobadilla01@gmail.com");
            DependencyService.Get<Services.IMakeToastMessage>()
                .MakeLongMessage("Email copied");
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://www.instagram.com/fernando_lazyskull/");
        }
    }
}