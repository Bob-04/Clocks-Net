using System;
using Clocks.Desktop.Views;
using Clocks.Desktop.Views.Account;

namespace Clocks.Desktop.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {
        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Main:
                    ViewsDictionary.Add(viewType, new MainView());
                    break;
                case ViewType.SignIn:
                    ViewsDictionary.Add(viewType, new SignInView());
                    break;
                case ViewType.SignUp:
                    // TODO
                    break;
                default:
                    throw new ArgumentNullException();
            }
        }
    }
}