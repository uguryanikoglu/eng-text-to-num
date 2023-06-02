using EngTextToNum.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EngTextToNum.Model
{
    public class Converter
    {
        private long NumberKeeper { get; set; }

        private long OutputNumber { get; set; }

        /// <summary>
        /// It is used for number calculation. If it is true, calculated output number is negated
        /// </summary>
        private bool MinusEncountered { get; set; }

        /// <summary>
        /// It is used for decimal point detection.
        /// </summary>
        private bool PointEncountered { get; set; }

        private string Text { get; set; }

        public Converter(string text)
        {
            Text = text;
        }

        private void SetNumberKeepers(long numberKeeper,long outputNumber)
        {
            NumberKeeper = numberKeeper;
            OutputNumber = outputNumber;
        }

        private void Nullify()
        {
            NumberKeeper = 0;
            OutputNumber = 0;
            MinusEncountered = false;
            PointEncountered = false;
        }

        private bool WordIsZero(string word)
        {
            return word == "zero" || word == "zeros";
        }

        private bool WordIsMinus(string word)
        {
            return word == "minus";
        }

        private bool WordIsPoint(string word)
        {
            return Constants.PointRepresentatives.Contains(word);
        }

        private bool WordIsNumberSeperator(string word)
        {
            return Constants.NumberSeperators.Contains(word);
        }

        private bool WordIsNumber(string word)
        {
            return Constants.NumberWords.ContainsKey(word) 
                    || (word.EndsWith("s") && Constants.NumberWords.ContainsKey(word[0..^1]))
                    || (word.EndsWith("ths") && Constants.NumberWords.ContainsKey(word[0..^3]))
                    ;
        }

        private bool ValueIsPointFinisherPreNumber(long value)
        {
            return Constants.PointFinisherPreNumbers.Contains(value);
        }

        private bool WordIsDecimalFinisher(string word)
        {
            return word.EndsWith("ths") && Constants.NumberWords.ContainsKey(word[0..^3]);
        }

        private WordNumberRepresentation GetNumberFromWord(string word)
        {
            if (word.EndsWith("ths"))
            {
                return Constants.NumberWords[word[0..^3]];
            }
            else if (word.EndsWith("s"))
            {
                return Constants.NumberWords[word[0..^1]];
            }

            return Constants.NumberWords[word];
        }

        /// <summary>
        /// This method handles the situation when the word "minus" is encountered
        /// </summary>
        /// <param name="current">the word to be added to the list "outputWords" </param>
        /// <param name="next">the value to be checked whether it is a number</param>
        /// <param name="outputWords"></param>
        private void MinusEncounter(string current, string? next, List<string> outputWords)
        {
            if(next == null || !WordIsNumber(next))
            {
                outputWords.Add(current);
            }
            else
            {
                MinusEncountered = true;
            }
        }

        /// <summary>
        /// This method handles the situation when decimal point is encountered
        /// </summary>
        /// <param name="current">the word to be added to the list "outputWords" </param>
        /// <param name="next">the value to be checked whether it is a number</param>
        /// <param name="outputWords"></param>
        private void PointEncounter(string current, string? next, List<string> outputWords)
        {

            if (next == null || !WordIsNumber(next) || PointEncountered)
            {
                FinalizeNumberCalculation(current,outputWords);
            }
            else
            {
                //Number seperator can be added to the list before decimal point calculation. So the last element should be removed
                if(outputWords.Count > 0)
                {
                    var lastElement = outputWords.ElementAt(^1);
                    if (WordIsNumberSeperator(lastElement))
                    {
                        outputWords.RemoveAt(outputWords.Count - 1);
                    }
                }
                    

                FinalizeNumberCalculation(".",outputWords);
                PointEncountered = true;
            }
        }

        /// <summary>
        /// This method handles the situation when number seperator is encountered
        /// </summary>
        /// <param name="previous">the word before the seperator</param>
        /// <param name="current">the word after the seperator and to be added to the list "outputWords" </param>
        /// <param name="next">the value to be checked whether it is a number</param>
        /// <param name="outputWords"></param>
        private void NumberSeperatorEncounter(string? previous, string current, string? next, List<string> outputWords)
        {
            if (next == null)
            {
                outputWords.Add(current);
                return;
            }

            if (WordIsNumber(next))
            {
                //If decimal point and numeric value is added before seperator, it is ignored and not added to the output list
                if (outputWords.Count > 1)
                {
                    var pointElement = outputWords.ElementAt(^1);
                    var numberElement = outputWords.ElementAt(^2);
                    var isNumeric = int.TryParse(numberElement, out _);

                    if (pointElement == "." && isNumeric)
                    {
                        return;
                    }
                }

                if (previous!= null && WordIsMinus(previous))
                {
                    return;
                }

                var model = GetNumberFromWord(next);

                //If number calculation is not started yet, seperator is added to the list
                if(OutputNumber + NumberKeeper == 0)
                {
                    outputWords.Add(current);
                }
                //Special case : for the numbers smaller than 20, the numbers seperated from each other
                else if ((OutputNumber + NumberKeeper) > 0 && (OutputNumber + NumberKeeper) < 20 && model.Value < 20)
                {
                    FinalizeNumberCalculation(current, outputWords);
                }
            }
            else
            {
                FinalizeNumberCalculation(current, outputWords);
            }
        }

        /// <summary>
        /// This method recalculates the output number to be printed and recurring number
        /// </summary>
        /// <param name="level">digit level</param>
        /// <param name="value">numeric value of the current word</param>
        /// <param name="outputWords">output to be printed end of the program</param>
        /// <returns></returns>
        private void RecalculateNumbers(int level, long value, string? next, List<string> outputWords)
        {
            //This control is added for decimal point finishers like "ten-thousandths" or "hundred-millionths"
            //Ex : in finisher "ten-thousandths", ten should not be added to the output list, so it is ignored with this control
            if (next != null && ValueIsPointFinisherPreNumber(value) && WordIsDecimalFinisher(next))
                return;

            //Special case : for the numbers smaller than 20, the numbers seperated from each other
            if ((OutputNumber + NumberKeeper) > 0 && (OutputNumber + NumberKeeper) < 20 && value < 20)
            {
                OutputNumber += NumberKeeper;
                outputWords.Add(OutputNumber.ToString());
                SetNumberKeepers(value, 0);
                return;
            }

            if (level > 2)
            {
                //For the multiplication case, the multiplier should be greater than the keeper value
                //Ex : "twenty three hundred" --> "2300"
                //     "hundred one hundred" --> "101 100"
                if(value > NumberKeeper)
                {
                    NumberKeeper *= value;

                    if (value >= 1000)
                    {
                        SetNumberKeepers(0, OutputNumber + NumberKeeper);
                    }
                }
                else
                {
                    OutputNumber += NumberKeeper;
                    outputWords.Add(OutputNumber.ToString());
                    SetNumberKeepers(value, 0);
                }
            }
            else
            {
                NumberKeeper += value;
            }
        }

        private void CheckAndAddNumber(List<string> outputWords)
        {
            if (OutputNumber > 0 || NumberKeeper > 0)
            {
                OutputNumber += NumberKeeper;
                if (MinusEncountered)
                    OutputNumber *= -1;
                outputWords.Add(OutputNumber.ToString());
            }
            Nullify();
        }

        /// <summary>
        /// This method ends number calculation process and nullify calculation values
        /// </summary>
        /// <param name="word">the word to be added to outputWords</param>
        /// <param name="outputWords">output to be printed end of the program</param>
        /// <returns></returns>
        private void FinalizeNumberCalculation(string word, List<string> outputWords)
        {
            CheckAndAddNumber(outputWords);
            outputWords.Add(word);
        }

        /// <summary>
        /// This method converts the given text to its equivalent consisting of numeric values
        /// </summary>
        /// <param name="text">text to be converted</param>
        /// <returns></returns>
        public string Convert()
        {
            var regex = new Regex("([^a-zA-Z\\d])");
            List<string> words = regex.Split(Text).ToList();
            List<string> outputWords = new();

            for (int i = 0; i < words.Count; i++)
            {
                string word = words[i].ToLower();

                if(WordIsNumberSeperator(word))
                {
                    var nextOne = i+1 ==  words.Count ? null : words[i+1];
                    var prevOne = i == 0 ? null : words[i - 1];
                    NumberSeperatorEncounter(prevOne,words[i],nextOne,outputWords);
                }
                else if(WordIsMinus(word))
                {
                    var nextOne = i + 2 >= words.Count ? null : words[i + 2];
                    MinusEncounter(words[i],nextOne,outputWords);
                }
                else if (WordIsPoint(word))
                {
                    var nextOne = i + 2 >= words.Count ? null : words[i + 2];
                    PointEncounter(words[i], nextOne, outputWords);
                }
                //zero value is special, if we encounter zero, it is added immediately to the output list
                else if (WordIsZero(word))
                {
                    FinalizeNumberCalculation("0", outputWords);
                }
                else if (WordIsNumber(word))
                {
                    if(!(WordIsDecimalFinisher(word) && PointEncountered))
                    {
                        var model = GetNumberFromWord(word);
                        var nextOne = i + 2 >= words.Count ? null : words[i + 2];

                        RecalculateNumbers(model.Level, model.Value, nextOne, outputWords);
                    }
                }
                else
                {
                    FinalizeNumberCalculation(words[i], outputWords);
                }
            }


            //if the given text ends with numbers, it has to be added to the output
            CheckAndAddNumber(outputWords);


            return string.Join("", outputWords);
        }
    }
}
