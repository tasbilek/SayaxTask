using Microsoft.Extensions.DependencyInjection;
using VoltMeter.Application.Services;

namespace VoltMeter.Application;

public static class ApplicationRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(typeof(ApplicationRegistrar).Assembly);
        });

        services.AddScoped<InvoiceDataCalculationService>();

        return services;
    }
}