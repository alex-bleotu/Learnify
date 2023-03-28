using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NumberToWord : MonoBehaviour
{
    private static string[] units = { "zero", "unu", "doi", "trei", "patru", "cinci", "șase", "șapte", "opt", "nouă", "zece", "unsprezece", "doisprezece", "treisprezece", "paisprezece", "cincisprezece", "șaisprezece", "șaptesprezece", "optsprezece", "nouăsprezece" };

    private static string[] tens = { "", "", "douazeci", "treizeci", "patruzeci", "cincizeci", "șaizeci", "șaptezeci", "optzeci", "nouăzeci" };

    public static string ConvertAmount(double number)
    {
        try
        {
            int intValue = (int)number;
            int decimalValue = (int)Math.Round((number - (double)(intValue)) * 100);
            if (decimalValue == 0)
                return Convert(intValue);
            else
                return Convert(intValue) + " virgulă " + Convert(decimalValue);
        }
        catch { }
        return "";
    }

    public static string Convert(int number)
    {
        if (number < 0)
            return "minus " + Convert(-number);

        if (number < 20)
            return units[number];

        if (number < 100)
            return tens[number / 10] + ((number % 10 != 0) ? " " : "") + units[number % 10];

        if (number < 1000)
            return units[number / 100] + " sute" + ((number % 100 != 0) ? " " : "") + Convert(number % 100);

        if (number < 1000000)
            return Convert(number / 1000) + " mii" + ((number % 1000 != 0) ? " " : "") + Convert(number % 1000);

        if (number < 1000000000)
            return Convert(number / 1000000) + " milioane" + ((number % 1000000 != 0) ? " " : "") + Convert(number % 1000000);

        return Convert(number / 1000000000) + " miliarde" + ((number % 1000000000 != 0) ? " " : "") + Convert(number % 1000000000);
    }

    public static string ReplaceSign(string word)
    {
        if (word == "+")
            return "plus";
        if (word == "-")
            return "minus";
        if (word == "*")
            return "înmulțit cu";
        if (word == "/")
            return "împărțit la";
        return word;
    }

    public static string ReplaceNumbers(string sentence)
    {
        if (sentence[sentence.Length - 1] == '.' || sentence[sentence.Length - 1] == ',' || sentence[sentence.Length - 1] == '?' ||
            sentence[sentence.Length - 1] == ';' || sentence[sentence.Length - 1] == ':' || sentence[sentence.Length - 1] == '!')
            sentence = sentence.Substring(0, sentence.Length - 1);

        string[] words = sentence.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            int number;

            if (words[i].Contains("+-*/"))
                words[i] = ReplaceSign(words[i]);

            if (int.TryParse(words[i], out number))
                words[i] = Convert(number);
        }

        return string.Join(" ", words);
    }
}
