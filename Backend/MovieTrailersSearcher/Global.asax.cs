using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using MovieTrailersSearcher.Abstract;
using MovieTrailersSearcher.Implementation;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using TmdbGateway;
using YoutubeGateway;

namespace MovieTrailersSearcher
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            container.Register<IMovieInfoProvider>(
                () => new MovieInfoProvider(appSettings["TmdbApiKey"], int.Parse(appSettings["MaxQuerySize"])),
                Lifestyle.Singleton);
            container.Register<IYoutubeVideosSearcher>(
                () => new YoutubeVideosSearcher(appSettings["YoutubeApiKey"], int.Parse(appSettings["MaxQuerySize"])),
                Lifestyle.Singleton);
            container.Register<IMovieInformationCache>(
                () => new MovieInformationCache(int.Parse(appSettings["CacheExpirationTimeInSeconds"])),
                Lifestyle.Singleton);
            container.Register<IMovieInformationProvider, MovieInformationProvider>(Lifestyle.Singleton);
            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = 
                new SimpleInjectorWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
