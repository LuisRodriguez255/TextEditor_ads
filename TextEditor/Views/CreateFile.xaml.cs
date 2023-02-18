using MarcTron.Plugin;
using System;
using TextEditor.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TextEditor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateFile : ContentPage
    {
        public CreateFile()
        {
            InitializeComponent();
            CrossMTAdmob.Current.OnInterstitialLoaded += (s, e) =>
            {
                CrossMTAdmob.Current.ShowInterstitial();
            };
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            FileManager fileManager = new FileManager();

            if (fileManager.CreateTextFile(txt_content.Text))
            {
                DependencyService.Get<Services.IMakeToastMessage>().
                    MakeLongMessage("*Saved at Downloads directory*");
                if (CrossMTAdmob.IsSupported)
                    CrossMTAdmob.Current
                        .LoadInterstitial("ca-app-pub-6443580320687365/7720205826");
            }
            else
                DependencyService.Get<Services.IMakeToastMessage>().
                    MakeLongMessage("Something was wrong :(");
        }
    }
}