using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpeakToMe.Core;
using SpeakToMe.Presence;
using SpeakToMe.Speech;
using SpeakToMe.Speech.Utility;
using System.ComponentModel.Composition.Hosting;

namespace Test.WinForm
{
    class BootStrapper : BootStrapperBase
    {
        protected override void AddCustomAssembliesToCatalog(System.ComponentModel.Composition.Hosting.AggregateCatalog catalog)
        {
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(TestPresence).Assembly));
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
