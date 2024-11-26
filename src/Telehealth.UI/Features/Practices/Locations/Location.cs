namespace Telehealth.UI.Features.Practices.Locations;

public static partial class Location
{
	public const string TableName = nameof(Location);
	public record Id(int Value);
	public record OperatingHours(TimeSpan OpeningTime, TimeSpan ClosingTime);

	public sealed class Details
	{
		public Id? LocationId { get; init; }
		public required string Name { get; init; }
		public required Practice.Id PracticeId { get; init; }
		public required OperatingHours OperatingHours { get; init; }
		public required string StreetAddress { get; init; }
		public required string City { get; init; }
		public required string State { get; init; }
		public required string Zip { get; init; }

		public Practice.Details? Practice { get; init; }
		private Details() { }
		public static Details Create(string name, Practice.Id practiceId, OperatingHours operatingHours, string streetAddress, string city, string state, string zip)
		{
			return new()
			{
				Name = name,
				PracticeId = practiceId,
				OperatingHours = operatingHours,
				StreetAddress = streetAddress,
				City = city,
				State = state,
				Zip = zip,
			};
		}
	}
}
