using Bogus;
using System.Linq.Expressions;

namespace Telehealth.UI.Extensions;

public static class ExtensionMethods
{
	public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
	where T : class
	{
		if (condition)
		{
			return query.Where(predicate);
		}

		return query;
	}

	public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query, bool condition, Func<T, bool> predicate)
		where T : class
	{
		if (condition)
		{
			return query.Where(predicate);
		}

		return query;
	}

	// Using this as a mechanism to keep rules clean.
	// https://github.com/bchavez/Bogus/issues/213
	public static Faker<T> UsePrivateConstructor<T>(this Faker<T> faker) where T : class
	{
		return faker.CustomInstantiator(_ => (T)Activator.CreateInstance(typeof(T), nonPublic: true)!);
	}

	public static string FromArray(this IEnumerable<string> values, string delimiter = " ")
	{
		return string.Join(delimiter, values);
	}
}
