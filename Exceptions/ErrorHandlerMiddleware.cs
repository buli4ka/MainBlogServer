using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Server.Exceptions
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        break;
                    case GoodException e:
                        // custom application error
                        response.StatusCode = (int) HttpStatusCode.OK;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        break;
                    case ForbbidenEcxeption e:
                        response.StatusCode = (int) HttpStatusCode.Forbidden;
                        break;

                    default:
                        // unhandled error
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new {message = error?.Message});
                await response.WriteAsync(result);
            }
        }
    }
}