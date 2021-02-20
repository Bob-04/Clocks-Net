using System.Windows.Controls;
using Clocks.Desktop.Tools.Navigation;
using Clocks.Desktop.ViewModels.Account;

namespace Clocks.Desktop.Views.Account
{
    public partial class SignUpView : UserControl, INavigable
    {
        public SignUpView()
        {
            InitializeComponent();
            DataContext = new SignUpViewModel();
        }
    }
}
