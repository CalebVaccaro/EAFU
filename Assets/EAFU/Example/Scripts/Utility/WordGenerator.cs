using System.Text;
using UnityEngine;

public static class WordGenerator
{
    static string[] syllables = { "ba", "na", "ka", "la", "mo", "fu", "ji", "po", "so", "ri" };
    private static int wordLength = 4;

    public static string GenerateRandomWord()
    {
        StringBuilder word = new StringBuilder();

        for (int i = 0; i < wordLength; i++)
        {
            word.Append(syllables[Random.Range(1, wordLength)]);
        }

        int numberSuffix = Random.Range(1, 999); // Generates a random three-digit number
        word.Append(numberSuffix.ToString());

        return word.ToString();
    }
}
