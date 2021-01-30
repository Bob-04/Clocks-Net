using System.Collections.Generic;

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
            if (!ViewsDictionary.ContainsKey(viewType))
            {
                InitializeView(viewType);
            }

            ContentOwner.ContentControl.Content = ViewsDictionary[viewType];
        }

        protected abstract void InitializeView(ViewType viewType);
    }
}