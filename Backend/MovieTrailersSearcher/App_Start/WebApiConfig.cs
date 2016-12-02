using System.Web.Http;
using System.Web.Http.Cors;
using MovieTrailersSearcher.Filters;

namespace MovieTrailersSearcher
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new ExceptionFilter());

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
        }
    }
}
