using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TextEditor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutControlFlyout : ContentPage
    {
        public ListView ListView;

        public FlyoutControlFlyout()
        {
            InitializeComponent();

            BindingContext = new FlyoutControlFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class FlyoutControlFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<FlyoutControlFlyoutMenuItem> MenuItems { get; set; }

            public FlyoutControlFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<FlyoutControlFlyoutMenuItem>(new[]
                {
                    new FlyoutControlFlyoutMenuItem { Id = 0, Title = "Create", TargetType = typeof(CreateFile) },
                    new FlyoutControlFlyoutMenuItem { Id = 1, Title = "Edit", TargetType = typeof(EditFile)},
                    new FlyoutControlFlyoutMenuItem { Id = 2, Title = "Read-only", TargetType = typeof(ReadOnly) },
                    new FlyoutControlFlyoutMenuItem { Id = 3, Title = "Contact", TargetType = typeof(Contact) },
                    new FlyoutControlFlyoutMenuItem { Id = 4, Title = "Share", TargetType = typeof(ShareFiles) }
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}