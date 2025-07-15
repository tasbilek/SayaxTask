using MediatR;
using VoltMeter.Application.MunicipalTax;
using VoltMeter.Shared;

namespace VoltMeter.WebAPI.Modules;

public static class MunicipalTaxModule
{
    public static void RegisterMunicipalTaxRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder routeGroup = app.MapGroup("/municipal-taxes").WithTags("MunicipalTaxes");

        routeGroup.MapGet(string.Empty, async (ISender sender, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new GetAllMunicipalTaxesQuery(), cancellationToken);

            return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
        }).Produces<BaseResult<IEnumerable<GetAllMunicipalTaxesResponse>>>();
    }
}