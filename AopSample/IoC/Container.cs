using Castle.Core;
using Castle.MicroKernel.ModelBuilder.Descriptors;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;

namespace AopSample.IoC
{
    public class Container
    {
        private static IWindsorContainer container;

        static Container() {
            container = new WindsorContainer();
        }

        public static IWindsorContainer WindsorContainer => container;

        public static void Register<T>(LifestyleType lifestyleType = LifestyleType.Singleton) {
            Register(typeof(T), lifestyleType); }

        public static void Register(Type type, LifestyleType lifestyleType = LifestyleType.Singleton) {
            RegisterComponent(Component.For(type), lifestyleType);
        }

        public static void Register<T1, T2>(LifestyleType lifestyleType = LifestyleType.Singleton) {
            Register(typeof(T1), typeof(T2), lifestyleType);
        }

        public static void Register(Type type1, Type type2, LifestyleType lifestyleType = LifestyleType.Singleton) {
            RegisterComponent(Component.For(type1).ImplementedBy(type2), lifestyleType);
        }

        public static void RegisterServices<T1, T2>() {
            container.Register(Classes.FromAssemblyContaining(typeof(T2)).BasedOn(typeof(T1)).WithService.FromInterface().Configure(c => c.LifestyleSingleton().Interceptors<ServiceInterceptor>()));
        }

        public static void RegisterWithBase<T1, T2>() => RegisterWithBase(typeof(T1), typeof(T2));

        public static void RegisterWithBase(Type type1, Type type2) {
            container.Register(Classes.FromAssemblyContaining(type2).BasedOn(type1).WithService.Base().Configure(c => c.LifestyleSingleton().NamedAutomatically(Guid.NewGuid().ToString())));
        }

        private static void RegisterComponent<T>(ComponentRegistration<T> componentRegistration, LifestyleType lifestyleType) where T : class {
            var lifestyleDescriptor = new LifestyleDescriptor<T>(lifestyleType);
            componentRegistration.AddDescriptor(lifestyleDescriptor);

            container.Register(componentRegistration);
        }


        public static object Resolve(Type targetType) {
            if (container.Kernel.HasComponent(targetType)) {
                return container.Resolve(targetType);
            }

            return null;
        }

        public static T Resolve<T>() => container.Resolve<T>();

        public static IEnumerable<T> ResolveAll<T>() => container.ResolveAll<T>();

        public static void Release(object instance) {
            container.Release(instance);
        }

        public static void Dispose() {
            container.Dispose();
        }
    }
}