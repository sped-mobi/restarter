using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using Task = System.Threading.Tasks.Task;

namespace Microsoft.VisualStudio.Restart
{
    internal static class Restarter
    {
        public static void Restart() =>
            ThreadHelper.JoinableTaskFactory.Run(RestartAsync);

        public static void RestartElevated() => 
            ThreadHelper.JoinableTaskFactory.Run(RestartElevatedAsync);

        public static async Task RestartAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            await Logger.LogAsync("Restarting Visual Studio IDE with normal privileges.");

            if (ErrorHandler.Failed(MefComponents.Shell4.Restart((uint)__VSRESTARTTYPE.RESTART_Normal)))
            {
                await Logger.LogAsync("Failed to restart Visual Studio IDE.");
            }
        }

        public static async Task RestartElevatedAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            if (ErrorHandler.Failed(MefComponents.Shell3.RestartElevated()))
            {
                await Logger.LogAsync("Failed to restart Visual Studio IDE.");
            }
        }
    }
}
