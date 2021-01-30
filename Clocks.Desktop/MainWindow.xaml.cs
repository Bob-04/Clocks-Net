using System.Windows;
using Clocks.Desktop.ViewModels;

namespace Clocks.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
