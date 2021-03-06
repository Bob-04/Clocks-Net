using System.Collections.Generic;
using Clocks.Desktop.Views;

namespace Clocks.Desktop.Tools.Navigation
{
    internal abstract class BaseNavigationModel : INavigationModel
    {
        protected BaseNavigationModel(IContentOwner contentOwner)
        {
            ContentOwner = contentOwner;
            ViewsDictionary = new Dictionary<ViewType, INavigable>();
        }

        protected IContentOwner ContentOwner { get; }
        protected Dictionary<ViewType, INavigable> ViewsDictionary { get; }

        public void Navigate(ViewType viewType)
        {
            INavigable content;
            if (viewType == ViewType.Main)
            {
                content = new MainView();
            }
            else
            {
                if (!ViewsDictionary.ContainsKey(viewType))
                {
                    InitializeView(viewType);
                }

                content = ViewsDictionary[viewType];
            }

            ContentOwner.ContentControl.Content = content;
        }

        protected abstract void InitializeView(ViewType viewType);
    }
}