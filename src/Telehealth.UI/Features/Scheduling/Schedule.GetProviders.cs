using MediatR;
using Microsoft.EntityFrameworkCore;
using Telehealth.UI.Features.Practices.Locations;
using Telehealth.UI.Features.Practices.Providers;
using Telehealth.UI.Infrastructure;

namespace Telehealth.UI.Features.Scheduling;

public static partial class Schedule
{
	public sealed class ProviderDetails
	{
		public required Provider.Id ProviderId { get; init; }
		public required string ProviderName { get; init; }
		public required string BackgroundCssClass { get; init; }
		public required string TextCssClass { get; init; }

		private ProviderDetails() { }
		public static ProviderDetails Create(Provider.Id providerId, string providerName, string backgroundCssClass, string textCssClass)
		{
			return new ProviderDetails
			{
				ProviderId = providerId,
				ProviderName = providerName,
				BackgroundCssClass = backgroundCssClass,
				TextCssClass = textCssClass
			};
		}
	}

	public class GetProviderDetails : IRequest<IEnumerable<ProviderDetails>>
	{
		public Provider.Id? ProviderId { get; init; }
		public Location.Id? LocationId { get; init; }
	}

	public class GetProviderDetailsHandler(TelehealthDbContext context) : IRequestHandler<GetProviderDetails, IEnumerable<ProviderDetails>>
	{
		private readonly string[] BackgroundColorChoices = ["dxbl-green-color", "dxbl-orange-color", "dxbl-purple-color", "dxbl-indigo-color", "dxbl-red-color"];
		private const string TextCssClass = "text-white";
		public async Task<IEnumerable<ProviderDetails>> Handle(GetProviderDetails request, CancellationToken cancellationToken)
		{
			var providers = await context
				.Set<Provider.Details>()
				.Include(p => p.Profile)
				.ToListAsync(cancellationToken);

			return providers
				.ConvertAll(p =>
					ProviderDetails.Create(p.ProviderId!, p.Profile!.FullName, Random.Shared.GetItems(BackgroundColorChoices, BackgroundColorChoices.Length)[0], TextCssClass)
				);
		}
	}
}
