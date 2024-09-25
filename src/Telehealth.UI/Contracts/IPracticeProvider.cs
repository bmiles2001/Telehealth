namespace Telehealth.UI.Contracts;

public interface IPracticeProvider
{
	IEnumerable<Practice.Provider> Providers { get; }
	Practice.Configuration Configuration { get; }
}

public class PracticeProvider : IPracticeProvider
{
	public IEnumerable<Practice.Provider> Providers =>
			[
				new Practice.Provider { Id = 1, Name = "Margaret Peacock", BackgroundCss="dxbl-green-color", TextCss="text-white" },
				new Practice.Provider { Id = 2, Name = "Nancy Davolio", BackgroundCss="dxbl-orange-color", TextCss="text-white" },
				new Practice.Provider { Id = 3, Name = "Andrew Fuller", BackgroundCss="dxbl-indigo-color", TextCss="text-white" }
			];

	public Practice.Configuration Configuration => new()
	{
		OpeningTime = TimeSpan.FromHours(8),
		ClosingTime = TimeSpan.FromHours(18)
	};
}


public static class Practice
{
	public class Configuration
	{
		public TimeSpan OpeningTime { get; set; }
		public TimeSpan ClosingTime { get; set; }
	}

	public class Provider
	{
		public required int Id { get; set; }
		public required string Name { get; set; }
		public string? TextCss { get; set; }
		public string? BackgroundCss { get; set; }

		public override bool Equals(object? obj)
		{
			return obj is Provider provider && provider.Id == Id;
		}

		public override int GetHashCode()
		{
			return Id;
		}
	}
}