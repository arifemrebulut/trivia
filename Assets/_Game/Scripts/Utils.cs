using System.Collections;
using System.Collections.Generic;
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
    }
}