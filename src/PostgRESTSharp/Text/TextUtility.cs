using System;
using Inflector;

namespace PostgRESTSharp.Text
{
	public class TextUtility : ITextUtility
	{

		public TextUtility()
		{
		}

		public string ToPlural(string word)
		{
			return Inflector.Inflector.Pluralize (word);
		}

		public string ToSingular(string word)
		{
			return Inflector.Inflector.Singularize (word);
		}

		public bool IsPlural(string word)
		{
			return word == Inflector.Inflector.Pluralize (word);
		}

		public bool IsSingular(string word)
		{
			return word == Inflector.Inflector.Singularize (word);
		}

		public string ToCapitalCase(string text)
		{
			text = text.Replace("_", " ").Replace(".", " ").Replace("-", " ").Replace("$", " ");
			var words = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			var result = "";
			foreach (var word in words)
			{
				result += ( char.ToUpper(word[0]) +
					((word.Length > 1) ? word.Substring(1).ToLower() : string.Empty));
			}
			return result;
		}

		public string ToPluralCapitalCase(string text)
		{
			text = text.Replace("_", " ").Replace(".", " ").Replace("-", " ");
			var words = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			words [words.Length - 1] = ToPlural (words [words.Length - 1]);
			var result = "";
			foreach (var word in words)
			{
				result += ( char.ToUpper(word[0]) +
					((word.Length > 1) ? word.Substring(1).ToLower() : string.Empty));
			}
			return result;
		}

		public string ToCamelCase(string text)
		{
			text = text.Replace("_", " ").Replace(".", " ").Replace("-", " ");
			var words = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			var result = "";
			foreach (var word in words)
			{
				result += (char.ToUpper(word[0]) +
					((word.Length > 1) ? word.Substring(1).ToLower() : string.Empty));
			}

			result = (char.ToLower(result[0]) +
				((result.Length > 1) ? result.Substring(1) : string.Empty));
			return result;
		}

		public string ToPluralCamelCase(string text)
		{
			text = text.Replace("_", " ").Replace(".", " ").Replace("-", " ");
			var words = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			words [words.Length - 1] = ToPlural (words [words.Length - 1]);
			var result = "";
			foreach (var word in words)
			{
				result += (char.ToUpper(word[0]) +
					((word.Length > 1) ? word.Substring(1).ToLower() : string.Empty));
			}

			result = (char.ToLower(result[0]) +
				((result.Length > 1) ? result.Substring(1) : string.Empty));
			return result;
		}

		public string Sanitise(string text)
		{
			return text.Replace(" ", "").Replace("_", "").Replace(".", "").Trim();
		}
	}
}

