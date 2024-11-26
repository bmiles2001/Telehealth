namespace Telehealth.UI.Features.Practices.Providers;

public static partial class Provider
{
	//public class Create : IRequest<Details>
	//{
	//	public required Profile.Id ProfileId { get; init; }
	//	public required Practice.Id PracticeId { get; init; }
	//	public required decimal Rate { get; init; }
	//}

	//public class CreateHandler(TelehealthDbContext context) : IRequestHandler<Create, Details>
	//{
	//	public async Task<Details> Handle(Create request, CancellationToken cancellationToken)
	//	{
	//		var newProvider = Details.Create(request.ProfileId, request.PracticeId, request.Rate);

	//		await context
	//			.Set<Details>()
	//			.AddAsync(newProvider, cancellationToken);

	//		await context.SaveChangesAsync(cancellationToken);

	//		return newProvider;
	//	}
	//}
}
