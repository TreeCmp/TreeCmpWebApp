﻿using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Behaviors
{
    internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
        ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            logger.LogInformation("Processing request {RequestName}", requestName);

            TResponse response;
            try
            {
                response = await next();
            }
            catch (Exception e)
            {
                using (LogContext.PushProperty("Exception", e, true))
                {
                    logger.LogError("Error while handling request {RequestName}", requestName);
                }
                throw;
            }

            return response;
        }
    }
}
