using MarcTron.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TextEditor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShareFiles : ContentPage
    {
        private string editedDir = DependencyService
            .Get<IGetExternalDirectory>().GetEditedDirPath();
        
        private string createdDir = DependencyService
            .Get<IGetExternalDirectory>().GetCreateDirPath();

        public ShareFiles()
        {
            InitializeComponent();
            CrossMTAdmob.Current.OnInterstitialLoaded += (s, e) =>
            {
                CrossMTAdmob.Current.ShowInterstitial();
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ShowFiles();
        }

        private void ShowFiles()
        {
            filesLayout.Children.Clear();

            var filesCreated = DependencyService
                .Get<IGetExternalDirectory>().GetFiles(createdDir);
            
            var filesEdited = DependencyService
                .Get<IGetExternalDirectory>().GetFiles(editedDir);

            var union = filesCreated.Union(filesEdited);

            if (union != null)
            {
                foreach (var path in union)
                {
                    TapGestureRecognizer tapGestureRecognizer =
                        new TapGestureRecognizer();

                    Label lblFilePath = new Label
                    {
                        FontSize = 20,
                        Text = Path.GetFileName(path),
                        FontFamily = "lucida-console.ttf",
                        TextColor = Color.Black,
                        VerticalTextAlignment = TextAlignment.Center
                    };

                    Image imgShareIcon = new Image
                    {
                        WidthRequest = 30,
                        HeightRequest = 30,
                        Source = "shareIcon.png",
                        Aspect = Aspect.AspectFit,
                        Margin = new Thickness(10, 0, 0, 0),
                        VerticalOptions = LayoutOptions.Center
                    };

                    StackLayout mainLayout = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Margin = new Thickness(20)
                    };

                    imgShareIcon.GestureRecognizers.Add(tapGestureRecognizer);
                    mainLayout.Children.Add(lblFilePath);
                    mainLayout.Children.Add(imgShareIcon);
                    filesLayout.Children.Add(mainLayout);

                    tapGestureRecognizer.Tapped += async (s, e) =>
                    {
                        await ((Image)s).ScaleTo(0.8, 0050, Easing.Linear);
                        await Task.Delay(100);
                        await ((Image)s).ScaleTo(1, 0050, Easing.Linear);

                        ShareFileRequest fileRequest = new ShareFileRequest
                        {
                            Title = "Share document",
                            File = new ShareFile(path)
                        };

                        CrossMTAdmob.Current
                        .LoadInterstitial("ca-app-pub-6443580320687365/7720205826");

                        await Share.RequestAsync(fileRequest);
                    };
                }
            }
        }

        private async void refreshControl_Refreshing(object sender, EventArgs e)
        {
            ShowFiles();
            await Task.Delay(0500);
            refreshControl.IsRefreshing = false;
        }
    }
}