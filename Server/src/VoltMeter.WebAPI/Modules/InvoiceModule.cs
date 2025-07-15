using MediatR;
using VoltMeter.Application.Invoice;
using VoltMeter.Shared;

namespace VoltMeter.WebAPI.Modules;

public static class InvoiceModule
{
    public static void RegisterInvoiceRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder routeGroup = app.MapGroup("/invoices").WithTags("Invoices");

        routeGroup.MapGet(string.Empty, async (ISender sender, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new GetAllInvoicesQuery(), cancellationToken);

            return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
        }).Produces<BaseResult<IEnumerable<GetAllInvoicesResponse>>>();

        routeGroup.MapGet("/{id}", async (string id, ISender sender, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new GetInvoiceByIdQuery(id), cancellationToken);

            return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
        }).Produces<BaseResult<GetInvoiceByIdResponse>>();
    }
}