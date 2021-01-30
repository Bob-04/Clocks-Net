using System.Windows.Controls;
using Clocks.Desktop.ViewModels.Account;

namespace Clocks.Desktop.Views.Account
{
    public partial class SignInView : UserControl
    {
        public SignInView()
        {
            InitializeComponent();
            DataContext = new SignInViewModel();
        }
    }
}
