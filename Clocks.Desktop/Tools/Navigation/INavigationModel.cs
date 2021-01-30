namespace Clocks.Desktop.Tools.Navigation
{
    internal enum ViewType
    {
        SignIn,
        SignUp,
        Main
    }

    internal interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
