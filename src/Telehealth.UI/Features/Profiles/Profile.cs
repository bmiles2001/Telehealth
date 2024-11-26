using MediatR;
using Microsoft.EntityFrameworkCore;
using Telehealth.UI.Extensions;
using Telehealth.UI.Infrastructure;

namespace Telehealth.UI.Features.Profiles;

public static class Profile
{
	public const string TableName = nameof(Profile);
	public record Id(int Value);
	public enum ProfileType { Patient, Provider }

	public class Details
	{
		public Id? ProfileId { get; init; }
		public required DateOnly DateOfBirth { get; init; }
		public string? Email { get; init; }
		public required string FirstName { get; init; }
		public string FullName => $"{FirstName} {LastName}";
		public required string LastName { get; init; }
		public required string PhoneNumber { get; init; }
		public ProfileType ProfileType { get; init; }

		protected Details() { }
		public static Details Create(DateOnly dateOfBirth, string email, string firstName, string lastName, string phoneNumber, ProfileType profileType)
		{
			return new()
			{
				DateOfBirth = dateOfBirth,
				Email = email,
				FirstName = firstName,
				LastName = lastName,
				PhoneNumber = phoneNumber,
				ProfileType = profileType,
			};
		}
	}

	public class Create : IRequest<Details>
	{
		public required Details Profile { get; init; }
	}

	public class CreateHandler(TelehealthDbContext context) : IRequestHandler<Create, Details>
	{
		public async Task<Details> Handle(Create request, CancellationToken cancellationToken)
		{
			await context
				.Set<Details>()
				.AddAsync(request.Profile, cancellationToken);

			await context.SaveChangesAsync(cancellationToken);

			return request.Profile;
		}
	}

	public class GetBy : IRequest<IEnumerable<Details>>
	{
		public Id? ProfileId { get; init; }
		public ProfileType? ProfileType { get; init; }
		public string? Name { get; init; }
	}

	public class GetByHandler(TelehealthDbContext context) : IRequestHandler<GetBy, IEnumerable<Details>>
	{
		public async Task<IEnumerable<Details>> Handle(GetBy request, CancellationToken cancellationToken)
		{
			return await context
				.Set<Details>()
				.WhereIf(request.ProfileId is not null, p => p.ProfileId == request.ProfileId)
				.WhereIf(request.ProfileType is not null, p => p.ProfileType == request.ProfileType)
				.WhereIf(request.Name is not null, p => p.FullName.StartsWith(request.Name!))
				.ToListAsync(cancellationToken);
		}
	}

}