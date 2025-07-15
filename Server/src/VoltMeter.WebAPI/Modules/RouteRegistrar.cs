namespace VoltMeter.WebAPI.Modules;

public static class RouteRegistrar
{
    public static void RegisterRoutes(this IEndpointRouteBuilder app)
    {
        app.RegisterInvoiceRoutes();
        app.RegisterMunicipalTaxRoutes();
    }
}