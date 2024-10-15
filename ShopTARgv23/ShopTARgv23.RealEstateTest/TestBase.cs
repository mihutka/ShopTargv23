using Microsoft.Extensions.DependencyInjection;
using ShopTARgv23.ApplicationServices.Services;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ShopTARgv23.RealEstateTest.Macros;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Hosting;
using ShopTARgv23.RealEstateTest.Mock;

namespace ShopTARgv23.RealEstateTest
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider {  get; set; }


        protected TestBase()
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {

        }

        protected T Svc<T>()
        {
            return serviceProvider.GetService<T>();
        }

        protected T Macro<T>() where T : IMacros
        {
            return serviceProvider.GetService<T>();
        }

        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<IRealEstateServices, RealEstatesServices>();
            services.AddScoped<IFileServices, FileServices>();
            services.AddScoped<IHostEnvironment, IMockHostEnviromnet>();

            services.AddDbContext<ShopTARgv23Context>(x =>
            {
                x.UseInMemoryDatabase("TEST");
                x.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            });

            RegisterMacros(services);
        }

        private void RegisterMacros(IServiceCollection services)
        {
            var macrosBaseType = typeof(IMacros);

            var macros = macrosBaseType.Assembly.GetTypes()
                .Where(x => macrosBaseType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            foreach (var macro in macros)
            {
                services.AddSingleton(macro);
            }
        }
    }
}
