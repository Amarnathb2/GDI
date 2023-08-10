using Autofac;
using GDI.Business.Models;
using GDI.Business.Repositories;
using Microsoft.Extensions.Localization;
using System.Reflection;
using XperienceAdapter.Core;
using XperienceAdapter.Localization;
using XperienceAdapter.Repositories;
using XperienceAdapter.Services;

namespace GDI.Configuration
{
    public class AutoFacConfig
    {
        /// <summary>
        /// ConfigureContainer method  creats object for Repositories.
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(type => type.IsClass && !type.IsAbstract && typeof(IService).IsAssignableFrom(type))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(type => type.GetTypeInfo()
                .ImplementedInterfaces.Any(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(type => type.GetTypeInfo()
                .ImplementedInterfaces.Any(@interface => @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IPageRepository<,>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<RepositoryServices>().As<IRepositoryServices>().InstancePerLifetimeScope();
            builder.RegisterType<XperienceStringLocalizerFactory>().As<IStringLocalizerFactory>().InstancePerLifetimeScope();
            builder.RegisterType<RepositoryServices>().As<IRepositoryServices>()
              .InstancePerLifetimeScope();
            builder.RegisterType<XperienceStringLocalizerFactory>().As<IStringLocalizerFactory>()
                .InstancePerLifetimeScope();
            builder.RegisterType<DocumentTypeHelperRepository>()
                .As<IDocumentTypeHelperRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<RepeaterRepository>()
                        .As<IRepeaterRepository>()
                        .InstancePerLifetimeScope();
            builder.RegisterType<NavigationRepository<PageMenuModel>>().As<INavigationRepository<PageMenuModel>>()
          .InstancePerLifetimeScope();
            builder.RegisterType<SiteMapRepository>()
           .As<ISiteMapRepository>()
           .InstancePerLifetimeScope();
            builder.RegisterType<ContactFormRepository>()
          .As<IContactFormRepository>()
          .InstancePerLifetimeScope();
        }
    }
}
