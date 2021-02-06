using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Clocks.Desktop.Services;
using Clocks.Desktop.Tools.Managers;
using Clocks.Desktop.Tools.Navigation;
using Clocks.Desktop.ViewModels;

namespace Clocks.Desktop
{
    public partial class MainWindow : Window, IContentOwner
    {
        public ContentControl ContentControl => _contentControl;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            StationManager.InitializeServerClient(new ServerClient());
            NavigationManager.Instance.Initialize(new InitializationNavigationModel(this));

            NavigationManager.Instance.Navigate(ViewType.SignIn);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            StationManager.CloseApp();
        }
    }
}
