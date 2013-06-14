using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Microsoft.Practices.Prism.Events;

namespace SpeakToMe.Core
{
    // Provides the resolution of instances from the MEF Catalog
    public static class ServiceLocator
    {
        private static CompositionContainer _container;

        public static void Initialize(AggregateCatalog catalog)
        {
            _container = new CompositionContainer(catalog);

            CompositionBatch batch = new CompositionBatch();

            foreach (var part in catalog.Parts)
            {
                batch.AddPart(part.CreatePart());
            }

            batch.AddExport(new Export(
                new ExportDefinition(
                    "Microsoft.Practices.Prism.Events.IEventAggregator",
                    new Dictionary<string, object>() { { "ExportTypeIdentity", "Microsoft.Practices.Prism.Events.IEventAggregator" } }),
                () => new EventAggregator()));

            _container.Compose(batch);
        }

        public static T GetInstance<T>()
        {
            return _container.GetExportedValue<T>();
        }
    }
}