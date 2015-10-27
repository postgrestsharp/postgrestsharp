using System;

namespace PostgRESTSharp.Text
{
	public interface ITextUtility
	{
		bool IsPlural(string word);

		bool IsSingular(string word);

		string ToPlural(string word);

		string ToSingular(string word);

		string ToCapitalCase(string text);

		string ToPluralCapitalCase(string text);

		string ToCamelCase(string text);

		string ToPluralCamelCase(string IsThisTableConvention);

		string Sanitise(string text);
	}
}

