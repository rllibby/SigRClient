using SpeakToMe.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakToMe.Speech.Utility
{
    public class BootStrapperBase
    {
        public BootStrapperBase()
        {
            
        }

        public void Initialize()
        {
            var catalog = InitializeCatalog();
            AddCustomAssembliesToCatalog(catalog);
            ServiceLocator.Initialize(catalog);
        }

        private AggregateCatalog InitializeCatalog()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ServiceLocator).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(CommandProcessor).Assembly));

            return catalog;
        }

        protected virtual void AddCustomAssembliesToCatalog(AggregateCatalog catalog)
        {
            
        }
    }
}
