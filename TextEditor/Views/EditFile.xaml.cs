using System;
using TextEditor.Data;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MarcTron.Plugin;

namespace TextEditor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditFile : ContentPage
    {
        private static string fileName;

        public EditFile()
        {
            InitializeComponent();
            CrossMTAdmob.Current.OnInterstitialLoaded += (s, e) =>
            {
                CrossMTAdmob.Current.ShowInterstitial();
            };
        }

        private async void btn_pickFile_Clicked(object sender, EventArgs e)
        {
            FileResult result = await FileManager.GetFilePath();

            if (result != null)
            {
                if (result.FileName.EndsWith(".txt"))
                {
                    FileManager fileManager = new FileManager();
                    string text = fileManager.GetTextFromFile(result.FullPath);
                    DateTime dt = DateTime.Now;
                    fileName = string.Format(result.FileName.Substring(0, result.FileName.LastIndexOf('.')) + "_{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}{6:D3}.txt",
                                               dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
                    txt_content.Text = text;
                    btn_save.IsEnabled = true;
                    btn_pickFile.IsEnabled = false;
                    btn_reset.IsEnabled = true;
                }
                else
                {
                    DependencyService.Get<Services.IMakeToastMessage>().MakeLongMessage("WRONG FILE!");
                }
            }
        }

        private void btn_save_Clicked(object sender, EventArgs e)
        {
            FileManager fileManager = new FileManager();

            if (fileManager.CreateTextFileExisting(fileName, txt_content.Text))
            {
                DependencyService.Get<Services.IMakeToastMessage>().
                    MakeLongMessage("*Saved at Downloads directory*");
                if (CrossMTAdmob.IsSupported)
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-6443580320687365/7720205826");
            }
            else
                DependencyService.Get<Services.IMakeToastMessage>().
                    MakeLongMessage("Something was wrong :(");
        }

        private void btn_reset_Clicked(object sender, EventArgs e)
        {
            fileName = null;
            txt_content.Text = string.Empty;
            btn_save.IsEnabled = false;
            btn_pickFile.IsEnabled = true;
            btn_reset.IsEnabled = false;
        }
    }
}