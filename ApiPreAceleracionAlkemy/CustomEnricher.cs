using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiPreAceleracionAlkemy
{
    public class CustomEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var property = propertyFactory.CreateProperty("Custom Enricher","");
            logEvent.AddPropertyIfAbsent(property);
        }
    }
}
