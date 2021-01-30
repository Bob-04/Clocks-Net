using System.Windows.Controls;
using Clocks.Desktop.Tools.Navigation;
using Clocks.Desktop.ViewModels;

namespace Clocks.Desktop.Views
{
    public partial class MainView : UserControl, INavigable
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
