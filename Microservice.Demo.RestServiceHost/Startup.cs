using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using MediatR;
using Microservice.Demo.Infrastructure;
using Microservice.Demo.Infrastructure.Events;
using Microservice.Demo.Service;
using Microservice.Demo.Service.Applications.Services;
using Microservice.Demo.Service.Configurations;
using Microservice.Demo.Service.Domain.Events;
using Microservice.Demo.Service.Domain.Repositories;
using Microservice.Demo.Service.Domain.Services;
using Microservice.Demo.Service.Interceptors;
using Microservice.Demo.Service.Repositories;
using Microservice.Demo.ServiceHost.Configurations;
using Microservice.Demo.ServiceHost.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microservice.Demo.RestServiceHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<ApiExceptionFilter>();
                options.Filters.Add<LogFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);//.AddJsonOptions(options => { options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; });

            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddOptions();
            services.AddDbContext<ServiceDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("ServiceDb"));
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/User/Login");
                    o.AccessDeniedPath = new PathString("/Error/Forbidden");
                });
            services.AddMediatR(typeof(Startup).Assembly,typeof(IEventBus).Assembly,typeof(IDomainEvent).Assembly);
            services.AddControllers();

            services.AddSingleton<ITest, Test>();
            services.AddSingleton<IEventBus, EventBus>();
            services.AddSingleton<IServiceConfigurationAgent, ServiceConfigurationAgent>();
            services.AddScoped<IDbUnitOfWork, DbUnitOfWork>();
            services.AddScoped<IDbContextProvider, DbContextProvider>();
            services.AddTransient<IVerificationAppService, VerificationAppService>();
            services.AddTransient<IMessageAppService, MessageAppService>();
            services.AddTransient<IVerificationService, VerificationService>();
            services.AddTransient<IVerificationRepository, VerificationRepository>();
            services.AddTransient<IDomainEventHandler<VerificationCreatedEvent>, VerificationCreatedEventHandler>();

            services.ConfigureDynamicProxy(config =>
            {
                config.Interceptors.AddTyped<ExceptionInterceptor>(m => m.DeclaringType.Name.EndsWith("AppService"));
                config.Interceptors.AddTyped<TransactionInterceptor>(m => m.DeclaringType.Name.EndsWith("AppService"));
                config.Interceptors.AddTyped<ProxyInterceptor>(m => m.DeclaringType.Name.EndsWith("Proxy"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            InitializeDatabase(app);

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ServiceDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
