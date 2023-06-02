using EngTextToNum.Model;
using Xunit;

namespace EngTextToNum.Tests
{
    public class Tests
    {
        [Fact]
        public void Test_Mail_Sample()
        {
            var testWord = "He paid twenty millions for three such cars.";
            var expected = "He paid 20000000 for 3 such cars.";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Point_With_No_Number()
        {
            var testWord = "Point point PoInT pOiNt poINT";
            var expected = "Point point PoInT pOiNt poINT";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_One_Decimal()
        {
            var testWord = "one hundred twenty three thousand one hundred twenty three and one hundred twenty three thousand one hundred twenty three millionths";
            var expected = "123123.123123";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_One_Decimal_Case2()
        {
            var testWord = "one hundred twenty-three and twelve thousand three hundred twelve hundred-thousandths";
            var expected = "123.12312";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_One_Decimal_Case3()
        {
            var testWord = "one hundred twenty-three and one million two hundred thirty-one thousand two hundred thirty-one ten-millionths";
            var expected = "123.1231231";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_One_Decimal_With_Word_Point()
        {
            var testWord = "one hundred twenty-three point one million two hundred thirty-one thousand two hundred thirty-one";
            var expected = "123.1231231";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_One_Decimal_With_Word_Point_Case2()
        {
            var testWord = "one hundred twenty three thousand one hundred twenty three point one hundred twenty three thousand one hundred twenty three";
            var expected = "123123.123123";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_One_Decimal_With_Word_Point_Case3()
        {
            var testWord = "one hundred twenty-three point twelve thousand three hundred twelve   ";
            var expected = "123.12312   ";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Combined_Decimals()
        {
            var testWord = "one hundred twenty-three point twelve thousand three hundred twelve plus one hundred twenty-three and twelve thousand three hundred twelve hundred-thousandths is equal to two hundred forty-six and twenty-four thousand six hundred twenty-four hundred-thousandths";
            var expected = "123.12312 plus 123.12312 is equal to 246.24624";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Combined_Decimals_And_One_Cardinal()
        {
            var testWord = "one thousand and five tenths plus eight thousand nine hundred ninety-nine point five is equal to ten thousand";
            var expected = "1000.5 plus 8999.5 is equal to 10000";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Minus_Decimals_And_One_Cardinal()
        {
            var testWord = "minus one thousand and five tenths plus one thousand point five is equal to zero";
            var expected = "-1000.5 plus 1000.5 is equal to 0";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Minus_With_No_Number()
        {
            var testWord = "minus Minus minuS mInUs MiNuS MInus   ";
            var expected = "minus Minus minuS mInUs MiNuS MInus   ";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_All_Minus_Numbers()
        {
            var testWord = "minus one thousand and five tenths plus minus one thousand point five is equal to minus two thousand one";
            var expected = "-1000.5 plus -1000.5 is equal to -2001";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_All_Spaces()
        {
            var testWord = "          ";
            var expected = "          ";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Test_Zero()
        {
            var testWord = "ZeRo";
            var expected = "0";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Smaller_Than_One_Hundred()
        {
            var testWord = "Thirty four";
            var expected = "34";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_OnlyNumbers_NotWords()
        {
            var testWord = "one hundred twenty three million four hundred fifty six thousand seven hundred eighty nine";
            var expected = "123456789";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Too_Big_Number()
        {
            var testWord = "eleven trillion two billion two hundred million three hundred thirty thousand forty four";
            var expected = "11002200330044";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_OnlyWords_NotNumbers()
        {
            var testWord = "Hello world! This is Ugur from Turkey.";
            var expected = "Hello world! This is Ugur from Turkey.";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Mixed_Text()
        {
            var testWord = "I started to count to ten by zero one two three four five six seven eight nine ten and backwards ten nine eight seven six five four three two one zero because software developers start counting from [zero]";
            var expected = "I started to count to 10 by 0 1 2 3 4 5 6 7 8 9 10 and backwards 10 9 8 7 6 5 4 3 2 1 0 because software developers start counting from [0]";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_Mixed_Text_Ends_With_Number()
        {
            var testWord = "Uncle Mahmut does not read the number one trillion two hundred thirty four billion five hundred sixty seven million eight hundred ninety thousand one hundred twenty three so he spelled it like one two three four five six seven eight nine and so on one by one";
            var expected = "Uncle Mahmut does not read the number 1234567890123 so he spelled it like 1 2 3 4 5 6 7 8 9 and so on 1 by 1";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_All_Combined()
        {
            var testWord = "Uncle Mahmut does not read the number one trillion two hundred thirty four billion five hundred sixty seven million eight hundred ninety thousand one hundred twenty three so he spelled it like one two three four five six seven eight nine and so on one by one. After that uncle Mahmut tried to add minus one thousand and five tenths to one thousand point five and he found that the equation is equal to [zero]";
            var expected = "Uncle Mahmut does not read the number 1234567890123 so he spelled it like 1 2 3 4 5 6 7 8 9 and so on 1 by 1. After that uncle Mahmut tried to add -1000.5 to 1000.5 and he found that the equation is equal to [0]";

            var converter = new Converter(testWord);
            var result = converter.Convert();

            Assert.Equal(expected, result);
        }

    }
}