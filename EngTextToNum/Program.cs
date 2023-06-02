using EngTextToNum.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EngTextToNum
{
    public class Program
    {
         

        /// <summary>
        /// This program takes the text as command line argument. 
        /// Every word and special characters should be seperated for getting the correct result.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Text: ");
            string? input = Console.ReadLine();
            
            var converter = new Converter(input ?? "NULL");

            //string output = string.IsNullOrEmpty(input)
            //                ? "NULL"
            //                : Convert(input);

            Console.WriteLine();
            Console.WriteLine("Converted Text: ");
            //Console.WriteLine(output);
            Console.WriteLine(converter.Convert());
            Console.WriteLine();
            Console.WriteLine("Press Any Key To Exit");
            Console.ReadKey();
        }

        ///// <summary>
        ///// This method recalculates the output number to be printed and recurring number
        ///// </summary>
        ///// <param name="level">digit level</param>
        ///// <param name="value">numeric value of the current word</param>
        ///// <param name="numberKeeper">the recurring number</param>
        ///// <param name="outputNumber">the number to be printed</param>
        ///// <param name="outputWords">output to be printed end of the program</param>
        ///// <returns></returns>
        //static (long numberKeeper,long outputNumber) RecalculateNumbers(int level, long value, long numberKeeper, long outputNumber,List<string> outputWords)
        //{
        //    if ((outputNumber + numberKeeper) > 0 && (outputNumber + numberKeeper) < 20 && value < 20)
        //    {                
        //        outputNumber += numberKeeper;
        //        outputWords.Add(outputNumber.ToString());
        //        return (value, 0);
        //    }

        //    if (level > 2)
        //    {
        //        numberKeeper *= value;

        //        if (value >= 1000)
        //        {
        //            outputNumber += numberKeeper;
        //            numberKeeper = 0;
        //        }
        //    }
        //    else
        //    {
        //        numberKeeper += value;
        //    }

        //    return (numberKeeper, outputNumber);
        //}

        ///// <summary>
        ///// This method ends number calculation process and nullify calculation values
        ///// </summary>
        ///// <param name="numberKeeper">the recurring number</param>
        ///// <param name="outputNumber">the number to be printed</param>
        ///// <param name="word">the word to be added to outputWords</param>
        ///// <param name="outputWords">output to be printed end of the program</param>
        ///// <returns></returns>
        //static (long numberKeeper, long outputNumber) FinalizeNumberCalculation(long numberKeeper, long outputNumber,string word, List<string> outputWords)
        //{
        //    if (outputNumber > 0 || numberKeeper > 0)
        //    {
        //        outputNumber += numberKeeper;
        //        outputWords.Add(outputNumber.ToString());
        //    }
        //    outputWords.Add(word);
        //    return (0, 0);
        //}

        ///// <summary>
        ///// This method converts the given text to its equivalent consisting of numeric values
        ///// </summary>
        ///// <param name="text">text to be converted</param>
        ///// <returns></returns>
        //public static string Convert(string text)
        //{
        //    List<string> words = text.Split(' ').ToList();
        //    List<string> outputWords = new();

        //    var numberKeeper = 0L;
        //    var outputNumber = 0L;

        //    for (int i = 0; i < words.Count; i++)
        //    {
        //        string word = words[i].ToLower();
        //        //zero value is special, if we encounter zero, it is added immediately to the output list
        //        if(word == "zero" || word == "zeros")
        //        {
        //            (numberKeeper, outputNumber) = FinalizeNumberCalculation(numberKeeper, outputNumber, "0", outputWords);
        //        }
        //        else if (numberWords.ContainsKey(word))
        //        {
        //            var (number, level) = numberWords[word];
        //            (numberKeeper,outputNumber) = RecalculateNumbers(level, number,numberKeeper,outputNumber,outputWords);
        //        }
        //        //sometimes numbers ends with "s" character to provide plurality, this must be checked
        //        else if (word.EndsWith("s") && numberWords.ContainsKey(word[0..^1]))
        //        {
        //            var (number, level) = numberWords[word[0..^1]];
        //            (numberKeeper, outputNumber) = RecalculateNumbers(level, number,numberKeeper,outputNumber,outputWords);
        //        }
        //        else
        //        {
        //            (numberKeeper, outputNumber) = FinalizeNumberCalculation(numberKeeper, outputNumber, words[i], outputWords);
        //        }
        //    }

            
        //    //if the given text ends with numbers, it has to be added to the output
        //    if(outputNumber > 0 || numberKeeper > 0)
        //    {
        //        outputNumber += numberKeeper;
        //        outputWords.Add(outputNumber.ToString());
        //    }

        //    return string.Join(' ', outputWords);
        //}
    }
}