/****************************************************************************
 * Copyleft (L) 2022 CENG - All Rights Not Reserved
 * You may use, distribute and modify this code.
 ****************************************************************************/

/**
 * @file DefaultValues.cs
 * @author Utku Oruc
 * @date 25 January 2022
 *
 * @brief <b> define default values for future use </b>
 *
 * DefaultValues.cs
 *
 * @see http://bilgisayar.mmf.erdogan.edu.tr/en/
 *
 */
namespace LargeTextFilesSorting.Utils
{
    public static class DefaultValues
    {
        private static readonly char[] LowerCharsToGenerate = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static readonly char[] UpperCharsToGenerate = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ -"

        public const string InputFileName = "test.csv";
        public const string OutputFileName = "test.output.csv";

        public const int MinCharsInStringPart = 10;
        public const int MaxCharsInStringPart = 30;
        public const int MaxNumberValue = 50000;

        public const long Gb10 = 1024L * 1024 * 1024 * 10;
        public const long Gb100 = 1024L * 1024 * 1024 * 100;

        public static char[] LowerChars { get { return LowerCharsToGenerate; } }
        public static char[] UpperChars { get { return UpperCharsToGenerate; } }
        public static readonly int AvgLineLengthForGenerator = MaxNumberValue.ToString().Length + 2 + (MinCharsInStringPart + MaxCharsInStringPart) / 2 + 2; //number part, separators, string part, end of line
        
        public static readonly int AvgLineLengthForTest = MaxNumberValue.ToString().Length + (MinCharsInStringPart + MaxCharsInStringPart) / 2;
        public static readonly long Gb10PossibleLinesCount = Gb10 / AvgLineLengthForTest;
        public static readonly long Gb100PossibleLinesCount = Gb100 / AvgLineLengthForTest;
    }
}
