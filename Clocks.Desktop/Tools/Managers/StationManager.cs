using System;
using System.Windows;
using Clocks.Desktop.Services;
using Clocks.Shared.DtoModels;

namespace Clocks.Desktop.Tools.Managers
{
    internal static class StationManager
    {
        internal static UserDto CurrentUser { get; set; }
        internal static IServerClient ServerClient { get; private set; }

        internal static void InitializeServerClient(IServerClient client)
        {
            ServerClient = client;
        }

        internal static void CloseApp()
        {
            MessageBox.Show("ShutDown");
            Environment.Exit(1);
        }
    }
}
