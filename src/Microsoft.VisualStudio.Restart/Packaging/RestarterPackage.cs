using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Commands;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace Microsoft.VisualStudio.Packaging
{
    [Guid(GuidSymbols.PackageString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true, RegisterUsing = RegistrationMethod.CodeBase)]
    public sealed class RestarterPackage : AsyncPackage, IVsInstalledProduct
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await base.InitializeAsync(cancellationToken, progress);

            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            Logger.Initialize(this, "Visual Studio Restarter");

            var service = await GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            var handler = new CommandHandler();
            CommandRegistrar.RegisterCommands(service, handler);
        }

        public int IdBmpSplash(out uint pIdBmp)
        {
            pIdBmp = 0U;
            return VSConstants.S_OK;
        }

        public int OfficialName(out string pbstrName)
        {
            pbstrName = "Visual Studio Restarter";
            return VSConstants.S_OK;
        }

        public int ProductID(out string pbstrPID)
        {
            pbstrPID = "VSRestarter";
            return VSConstants.S_OK;
        }

        public int ProductDetails(out string pbstrProductDetails)
        {
            pbstrProductDetails = "Restarts the Visual Studio IDE.";
            return VSConstants.S_OK;
        }

        public int IdIcoLogoForAboutbox(out uint pIdIco)
        {
            pIdIco = 0U;
            return VSConstants.S_OK;
        }
    }
}
