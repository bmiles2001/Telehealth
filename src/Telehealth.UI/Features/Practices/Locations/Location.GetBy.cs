using MediatR;
using Microsoft.EntityFrameworkCore;
using Telehealth.UI.Extensions;
using Telehealth.UI.Infrastructure;

namespace Telehealth.UI.Features.Practices.Locations;

public static partial class Location
{
	public class GetBy : IRequest<IEnumerable<Details>>
	{
		public Id? LocationId { get; init; }
		public required Practice.Id PracticeId { get; init; }
	}

	public class GetLocationByHandler(TelehealthDbContext context) : IRequestHandler<GetBy, IEnumerable<Details>>
	{
		public async Task<IEnumerable<Details>> Handle(GetBy request, CancellationToken cancellationToken)
		{
			return await context
				.Set<Details>()
				.Where(l => l.PracticeId == request.PracticeId)
				.WhereIf(request.LocationId is not null, l => l.LocationId == request.LocationId)
				.ToListAsync(cancellationToken);
		}
	}
}
