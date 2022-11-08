﻿using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MySpot.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infrastructure.Exceptions
{
    internal sealed class ExceptionMiddleware : IMiddleware
    {
        public ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
		{
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
				Console.WriteLine(exception.ToString());
				await HandleExceptionAsync(exception, context);
            }
        }

		private async Task HandleExceptionAsync(Exception exception, HttpContext context)
		{
			var (statusCode, error) = exception switch
			{
				CustomException => (StatusCodes.Status400BadRequest, 
					new Error(exception.GetType().Name.Underscore().Replace("_exception", String.Empty), exception.Message)),
				_ => (StatusCodes.Status500InternalServerError, 
					new Error("error", "There was an error."))
			};

			context.Response.StatusCode = statusCode;
			await context.Response.WriteAsJsonAsync(error);
        }

		private record Error(string Code, string Reason);
    }
}
