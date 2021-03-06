﻿using MassTransit;
using MassTransit.Configurators;
using MassTransit.PipeBuilders;
using MassTransit.PipeConfigurators;
using System.Collections.Generic;
using System.Linq;
using Serilog;

namespace AppChat.Facade.LogExt
{
    public class ExceptionLoggerSpecification<T> : IPipeSpecification<T> where T : class, PipeContext
    {
        private readonly ILogger _logger;

        public ExceptionLoggerSpecification(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            return Enumerable.Empty<ValidationResult>();
        }

        public void Apply(IPipeBuilder<T> builder)
        {
            builder.AddFilter(new ExceptionLoggerFilter<T>(_logger));
        }
    }
}
