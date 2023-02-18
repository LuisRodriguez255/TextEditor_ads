using Android.Widget;
using TextEditor.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(TextEditor.Droid.MakeToastMessage))]
namespace TextEditor.Droid
{
    public class MakeToastMessage : IMakeToastMessage
    {
        public void MakeLongMessage(string text)
        {
            Toast.MakeText(Android.App.Application.Context.ApplicationContext,
                text, ToastLength.Long).Show();
        }
    }
}