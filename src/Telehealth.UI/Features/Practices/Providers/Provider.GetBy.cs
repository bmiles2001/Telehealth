using MediatR;
using Microsoft.EntityFrameworkCore;
using Telehealth.UI.Extensions;
using Telehealth.UI.Features.Practices.Locations;
using Telehealth.UI.Features.Profiles;
using Telehealth.UI.Infrastructure;

namespace Telehealth.UI.Features.Practices.Providers;

public static partial class Provider
{
	public class GetBy : IRequest<IEnumerable<Details>>
	{
		public Profile.Id? ProfileId { get; init; }
		public Location.Id? LocationId { get; init; }
	}

	public class GetByHandler(TelehealthDbContext context) : IRequestHandler<GetBy, IEnumerable<Details>>
	{
		public async Task<IEnumerable<Details>> Handle(GetBy request, CancellationToken cancellationToken)
		{
			return await context
				.Set<Details>()
				.Include(r => r.Profile)
				.WhereIf(request.ProfileId is not null, p => p.ProfileId == request.ProfileId)
				//.WhereIf(request.LocationId is not null, p => p.LocationId == request.LocationId)
				.ToListAsync(cancellationToken);
		}
	}
}
