using Xamarin.Forms;
using Xamarin.Essentials;
using TextEditor.Views;

namespace TextEditor
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new FlyoutControl();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
