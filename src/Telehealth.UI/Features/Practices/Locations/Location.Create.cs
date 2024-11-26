using MediatR;
using Telehealth.UI.Infrastructure;

namespace Telehealth.UI.Features.Practices.Locations;

public static partial class Location
{
	public class Create : IRequest<Details>
	{
		public required string Name { get; init; }
		public required Practice.Id PracticeId { get; init; }
		public required OperatingHours OperatingHours { get; init; }
		public required string StreetAddress { get; init; }
		public required string City { get; init; }
		public required string State { get; init; }
		public required string Zip { get; init; }
	}

	public class CreateLocationHandler(TelehealthDbContext context) : IRequestHandler<Create, Details>
	{
		public async Task<Details> Handle(Create request, CancellationToken cancellationToken)
		{
			var location = Details.Create(request.Name, request.PracticeId, request.OperatingHours, request.StreetAddress, request.City, request.State, request.Zip);

			await context
				.Set<Details>()
				.AddAsync(location, cancellationToken);

			return location;
		}
	}
}
