using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Restart;

namespace Microsoft.VisualStudio.Commands
{
    internal partial class CommandHandler
    {
        public override void OnExecuteRestartCommand(object sender, EventArgs e)
        {
            Restarter.Restart();
        }

        public override void OnExecuteRestartElevatedCommand(object sender, EventArgs e)
        {
            Restarter.RestartElevated();
        }
    }
}
