using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.VisualStudio
{
    internal static class MefComponents
    {
        public static TServiceInterface GetService<TService, TServiceInterface>()
        {
            return (TServiceInterface)Shell.Package.GetGlobalService(typeof(TService));
        }

        public static TServiceInterface GetComponentModelService<TServiceInterface>() where TServiceInterface : class
        {
            return ComponentModel.GetService<TServiceInterface>();
        }

        public static IComponentModel ComponentModel
            => GetService<SComponentModel, IComponentModel>();

        public static IVsShell3 Shell3 => GetService<SVsShell, IVsShell3>();

        public static IVsShell4 Shell4 => GetService<SVsShell, IVsShell4>();
    }
}
