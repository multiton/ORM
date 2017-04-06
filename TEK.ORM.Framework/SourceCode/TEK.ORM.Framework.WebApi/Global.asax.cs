using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Controllers;

using Castle.Windsor;
using Castle.MicroKernel.Registration;

using TEK.ORM.Framework.IoC;

namespace TEK.ORM.Framework.WebApi
{
    public class WebApiApplication : HttpApplication
    {
		private readonly IWindsorContainer container;

		public WebApiApplication() : base()
		{
			this.container = BootStrapper.Configure();

			// this.container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.NLog).WithConfig("NLog.config"));
			this.container.Register(Classes.FromThisAssembly().BasedOn<IHttpController>().LifestylePerWebRequest());
		}

		protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

			GlobalConfiguration.Configuration.Services.Replace
				(typeof(IHttpControllerActivator), new ControllerActivator(this.container));
		}
    }
}