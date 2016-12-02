using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Journalist;
using MovieTrailersSearcher.Models;
using TmdbGateway;
using YoutubeGateway;

namespace MovieTrailersSearcher.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Require.NotNull(actionExecutedContext, nameof(actionExecutedContext));

            var generalException = actionExecutedContext.Exception;
            if (generalException is YoutubeAccessException)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
                    HttpStatusCode.BadGateway, 
                    new QueryError(ErrorCode.YoutubeError, "Youtube connection failed"));
                return;
            }

            if (generalException is TmdbException)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
                    HttpStatusCode.BadGateway,
                    new QueryError(ErrorCode.TmdbError, "Tmdb connection failed"));
                return;
            }

            if (generalException is ArgumentException || generalException is FormatException)
            {
                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid request");
                return;
            }

            actionExecutedContext.Response =
                actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unknown error");
        }
    }
}