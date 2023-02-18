using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Xamarin.Essentials;
using System.Threading;
using MarcTron.Plugin;

namespace TextEditor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReadOnly : ContentPage
    {
        private CancellationTokenSource cts;

        public ReadOnly()
        {
            InitializeComponent();
            CrossMTAdmob.Current.OnInterstitialLoaded += (s, e) =>
            {
                CrossMTAdmob.Current.ShowInterstitial();
            };
        }

        private async void ShowText()
        {
            FileResult fileResult = await FileManager.GetFilePath();

            if (fileResult != null)
            {
                if (fileResult.FileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    FileManager fileManager = new FileManager();
                    string content = fileManager.GetTextFromFile(fileResult.FullPath);
                    lbl_content.Text = content;
                    CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-6443580320687365/7720205826");
                }
                else
                    DependencyService.Get<Services.IMakeToastMessage>().MakeLongMessage("WRONG FILE!");
            }
        }

        private void btn_filePick_Clicked(object sender, EventArgs e)
        {
            ShowText();
        }

        private async void btn_play_Clicked(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();

            if (!string.IsNullOrWhiteSpace(lbl_content.Text))
                await TextToSpeech.SpeakAsync(lbl_content.Text, cts.Token);
        }

        private void btn_stop_Clicked(object sender, EventArgs e)
        {
            if (cts?.IsCancellationRequested ?? true)
                return;

            cts.Cancel();
        }

        private void btn_fMax_Clicked(object sender, EventArgs e)
        {
            if (lbl_content.FontSize <= 70)
                lbl_content.FontSize += 5;
        }

        private void btn_fMin_Clicked(object sender, EventArgs e)
        {
            if (lbl_content.FontSize >= 20)
                lbl_content.FontSize -= 5;
        }

        private void btn_reset_Clicked(object sender, EventArgs e)
        {
            lbl_content.Text = string.Empty;
            lbl_content.FontSize = 20;
        }
    }
}