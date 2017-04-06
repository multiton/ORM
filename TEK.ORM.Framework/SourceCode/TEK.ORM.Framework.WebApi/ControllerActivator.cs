using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

using Castle.Windsor;

namespace TEK.ORM.Framework.WebApi
{
	public class ControllerActivator : IHttpControllerActivator
	{
		private readonly IWindsorContainer container;

		public ControllerActivator(IWindsorContainer container)
		{
			this.container = container;
		}

		public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor descriptor, Type controllerType)
		{
			var controller = (IHttpController)this.container.Resolve(controllerType);

			request.RegisterForDispose(new Release(() => this.container.Release(controller)));

			return controller;
		}

		private class Release : IDisposable
		{
			private readonly Action release;

			public Release(Action release)
			{
				this.release = release;
			}

			public void Dispose()
			{
				this.release();
			}
		}
	}
}