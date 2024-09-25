namespace Telehealth.UI.Features.Patient;

public static class Patient
{
	public class Profile
	{
		public int Id { get; set; } = default!;
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
		public string PhoneNumber { get; set; } = default!;
	}
}