using MediatR;
using Telehealth.UI.Features.Profiles;

namespace Telehealth.UI.Features.Practices.Patients;

public static class Patient
{
	public class GetBy : IRequest<IEnumerable<Profile.Details>>
	{
		public required string Name { get; init; }
	}

	public sealed class Handler(ISender Request) : IRequestHandler<GetBy, IEnumerable<Profile.Details>>
	{
		public async Task<IEnumerable<Profile.Details>> Handle(GetBy request, CancellationToken cancellationToken)
		{
			return await Request.Send(new Profile.GetBy { Name = request.Name, ProfileType = Profile.ProfileType.Patient }, cancellationToken);
		}
	}
}