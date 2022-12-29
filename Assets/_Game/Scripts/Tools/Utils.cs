using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Trivia
{
    public static class Utils
    {
        public static string GenerateCategoryDisplayName(string categoryName)
        {
            // Split words.
            string[] words = categoryName.Split("-");

            // Make first letters uppercase
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];

                words[i] = char.ToUpper(word[0]) + word.Substring(1);
            }

            // Combine words;
            string displayName = string.Join(" ", words);

            return displayName;
        }

        public static string GetUriWithQueryString(string requestUri, Dictionary<string, string> queryStringParams)
        {
            bool startingQuestionMarkAdded = false;
            var sb = new StringBuilder();
            sb.Append(requestUri);
            foreach (var parameter in queryStringParams)
            {
                if (parameter.Value == null)
                {
                    continue;
                }

                sb.Append(startingQuestionMarkAdded ? '&' : '?');
                sb.Append(parameter.Key);
                sb.Append('=');
                sb.Append(parameter.Value);
                startingQuestionMarkAdded = true;
            }
            return sb.ToString();
        }
    }
}