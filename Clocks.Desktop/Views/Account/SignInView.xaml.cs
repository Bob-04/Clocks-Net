using System.Windows.Controls;
using Clocks.Desktop.Tools.Navigation;
using Clocks.Desktop.ViewModels.Account;

namespace Clocks.Desktop.Views.Account
{
    public partial class SignInView : UserControl, INavigable
    {
        public SignInView()
        {
            InitializeComponent();
            DataContext = new SignInViewModel();
        }
    }
}
