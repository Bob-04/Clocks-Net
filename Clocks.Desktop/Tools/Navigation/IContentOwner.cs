using System.Windows.Controls;

namespace Clocks.Desktop.Tools.Navigation
{
    internal interface IContentOwner
    {
        ContentControl ContentControl { get; }
    }
}
