using System;
using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Text
{
    public class TextUtility : ITextUtility
    {
        public string ToPlural(string word)
        {
            var result = Inflector.Inflector.Pluralize(word);
            if (result == null)
            {
                //no result, so let's assume it is
                return word;
            }
            return result;
        }

        public string ToSingular(string word)
        {
            var result = Inflector.Inflector.Singularize(word);
            if (result == null)
            {
                //no result, so let's assume it is
                return word;
            }
            return result;
        }

        public bool IsPlural(string word)
        {
            var result = Inflector.Inflector.Pluralize(word);
            if (result == null)
            {
                //no result, so let's assume it's not
                return false;
            }
            return word == result;
        }

        public bool IsSingular(string word)
        {
            var result = Inflector.Inflector.Singularize(word);
            if (result == null)
            {
                //no result, so let's assume it is
                return true;
            }
            return word == result;
        }

        public string ToCapitalCase(string text)
        {
            var words = PrepareWordsArray(text);
            return CombineWords(words);
        }

        public string ToPluralCapitalCase(string text)
        {
            var words = PrepareWordsArray(text);
            words [words.Length - 1] = ToPlural (words [words.Length - 1]);
            return CombineWords(words);
        }

        public string ToCamelCase(string text)
        {
            string[] words = PrepareWordsArray(text);
            var result = CombineWords(words);
            return ForceFirstLetterLower(result);
        }
        
        public string ToPluralCamelCase(string text)
        {
            string[] words = PrepareWordsArray(text);
            words[words.Length - 1] = ToPlural(words[words.Length - 1]);
            var result = CombineWords(words);
            return ForceFirstLetterLower(result);
        }

        private string[] PrepareWordsArray(string text)
        {
            string[] words;
            if (IsThisTableConvention(text))
            {
                //table conventions reveres the order
                words = SplitTheTableName(text);
                Array.Reverse(words);
                List<string> tempWords = new List<string>();
                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = RemoveConventionCharacters(words[i]);
                    tempWords.AddRange(words[i].Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries));
                }
                words = tempWords.ToArray();
            }
            else
            {
                text = RemoveConventionCharacters(text);
                words = text.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            }
            return words;
        }

        private string RemoveConventionCharacters(string word)
        {
            return word.Replace("_", " ").Replace(".", " ").Replace("-", " ").Replace("$", " ");
        }

        private string[] SplitTheTableName(string text)
        {
            return text.Split(new string[] { "$" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private bool IsThisTableConvention(string text)
        {
            return text.Contains("$");
        }

        public string Sanitise(string text)
        {
            return text.Replace(" ", "").Replace("_", "").Replace(".", "").Trim();
        }

        private string ForceFirstLetterLower(string result)
        {
            return (char.ToLower(result[0]) +
                    ((result.Length > 1) ? result.Substring(1) : string.Empty));
        }

        private string CombineWords(string[] words)
        {
            string result = "";
            foreach (var word in words)
            {
                result += (char.ToUpper(word[0]) +
                           ((word.Length > 1) ? word.Substring(1).ToLower() : string.Empty));
            }
            return result;
        }
    }
}
