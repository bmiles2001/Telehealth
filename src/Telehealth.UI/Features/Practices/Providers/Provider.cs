using Telehealth.UI.Features.Profiles;

namespace Telehealth.UI.Features.Practices.Providers;

public static partial class Provider
{
	public const string TableName = nameof(Provider);

	public record Id(int Value);
	public sealed class Details
	{
		public Id? ProviderId { get; init; }
		public required Profile.Id ProfileId { get; init; }
		public required decimal Rate { get; init; }

		public Profile.Details? Profile { get; init; }
		private Details() { }
		public static Details Create(Profile.Id profileId, decimal rate)
		{
			return new()
			{
				ProfileId = profileId,
				Rate = rate,
			};
		}
	}
}
