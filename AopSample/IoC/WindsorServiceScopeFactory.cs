﻿using Castle.Windsor;
using Microsoft.Extensions.DependencyInjection;

namespace AopSample.IoC
{
    public class WindsorServiceScopeFactory : IServiceScopeFactory
    {
        private readonly IWindsorContainer _container;

        public WindsorServiceScopeFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public IServiceScope CreateScope()
        {
            return new WindsorServiceScope(_container);
        }
    }
}
