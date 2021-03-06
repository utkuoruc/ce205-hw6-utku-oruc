/****************************************************************************
 * Copyleft (L) 2022 CENG - All Rights Not Reserved
 * You may use, distribute and modify this code.
 ****************************************************************************/

/**
 * @file PerfTest.cs
 * @author Utku Oruc
 * @date 25 January 2022
 *
 * @brief <b> perform a test </b>
 *
 * PerfTest.cs
 *
 * @see http://bilgisayar.mmf.erdogan.edu.tr/en/
 *
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LargeTextFilesSorting.Utils
{
    public class PerfTest
    {
        const int InitialCount = 1024;
        
        static readonly int[] Multiplier = { 512, 1024, 2048, 4096, 16384, 512, 1024, 2048, 4096, 16384 };
        
        public void RunMemoryTest()
        {
            var comparator = new StringNumberPartComparator();
            
            var sw = new Stopwatch();

            Console.WriteLine($"{DateTime.Now}. Performance of sorting in single thread mode started");

            var avgs = new List<double>();

            foreach (var m in Multiplier)
            {
                sw.Reset();

                var count = InitialCount * m;

                var ar = GenerateInput(count);

                Console.WriteLine($"{DateTime.Now}. {count} lines generated in memory");

                sw.Start();

                Array.Sort(ar, comparator);

                sw.Stop();

                var iPerSec = count * 1000L / (sw.Elapsed.TotalMilliseconds.Equals(0.0) ? 1.0 : sw.Elapsed.TotalMilliseconds);
                avgs.Add(iPerSec);
                Console.WriteLine($"{DateTime.Now}. {count} lines sorting took: {sw.Elapsed.TotalMilliseconds:0.##} mc. Items per sec: {iPerSec:0.}");
            }

            Console.WriteLine($"{DateTime.Now}. Total avg items per sec: {avgs.Average()}");

            var possibleLinesPerMinute = avgs.Average() * 60;

            Console.WriteLine($"{DateTime.Now}. 10gb ASCII possible sorting time of line with {DefaultValues.AvgLineLengthForTest} chars count: {(DefaultValues.Gb10PossibleLinesCount / possibleLinesPerMinute):0.##} mins");
            Console.WriteLine($"{DateTime.Now}. 100gb ASCII possible sorting time of line with {DefaultValues.AvgLineLengthForTest} chars count: {(DefaultValues.Gb100PossibleLinesCount / possibleLinesPerMinute):0.##} mins");

            Console.WriteLine($"{DateTime.Now}. Performance of sorting in single thread mode completed");
        }

        public void RunFileSystemTest()
        {
            var sw = new Stopwatch();

            Console.WriteLine($"{DateTime.Now}. Performance of writing/reading to disk in single thread mode started");

            var avgsWrite = new List<double>();
            var avgsRead = new List<double>();
            
            foreach (var m in Multiplier)
            {
                sw.Reset();

                var count = InitialCount * m;

                var ar = GenerateInput(count);

                var fileName = $"{Guid.NewGuid().ToString()}.txt";

                Console.WriteLine($"{DateTime.Now}. {count} lines generated for writing");

                // writing
                sw.Start();

                using (var wr = new StreamWriter(fileName))
                {
                    for (int i = 0; i < ar.Length; i++)
                    {
                        wr.WriteLine(ar[i]);
                    }
                }

                sw.Stop();

                var iwPerSec = count * 1000L / (sw.Elapsed.TotalMilliseconds.Equals(0.0) ? 1.0 : sw.Elapsed.TotalMilliseconds);
                avgsWrite.Add(iwPerSec);
                Console.WriteLine($"{DateTime.Now}. Writing of {count} lines took: {sw.Elapsed.TotalMilliseconds:0.##} mc. Items per sec: {iwPerSec:0.}");

                // reading
                sw.Reset();
                sw.Start();

                using (var wr = new StreamReader(fileName))
                {
                    for (int i = 0; i < ar.Length; i++)
                    {
                        wr.ReadLine();
                    }
                }

                sw.Stop();

                var irPerSec = count * 1000L / (sw.Elapsed.TotalMilliseconds.Equals(0.0) ? 1.0 : sw.Elapsed.TotalMilliseconds);
                avgsRead.Add(irPerSec);
                Console.WriteLine($"{DateTime.Now}. Reading of {count} lines took: {sw.Elapsed.TotalMilliseconds:0.##} mc. Items per sec: {irPerSec:0.}");

                // delete file 
                File.Delete(fileName);
            }

            Console.WriteLine($"{DateTime.Now}. Total writing avg items per sec: {avgsWrite.Average()}");
            Console.WriteLine($"{DateTime.Now}. Total reading avg items per sec: {avgsRead.Average()}");

            var wPossibleLinesPerMinute = avgsWrite.Average() * 60;

            Console.WriteLine($"{DateTime.Now}. Write 10gb ASCII with avg line chars count {DefaultValues.AvgLineLengthForTest} - possible time: {(DefaultValues.Gb10PossibleLinesCount / wPossibleLinesPerMinute):0.##} min");
            Console.WriteLine($"{DateTime.Now}. Write 100gb ASCII with avg line chars count {DefaultValues.AvgLineLengthForTest} - possible time: {(DefaultValues.Gb100PossibleLinesCount / wPossibleLinesPerMinute):0.##} min");

            var rPossibleLinesPerMinute = avgsRead.Average() * 60;

            Console.WriteLine($"{DateTime.Now}. Read 10gb ASCII with avg line chars count {DefaultValues.AvgLineLengthForTest} - possible time: {(DefaultValues.Gb10PossibleLinesCount / rPossibleLinesPerMinute):0.##} min");
            Console.WriteLine($"{DateTime.Now}. Read 100gb ASCII with avg line chars count {DefaultValues.AvgLineLengthForTest} - possible time: {(DefaultValues.Gb100PossibleLinesCount / rPossibleLinesPerMinute):0.##} min");

            Console.WriteLine($"{DateTime.Now}. Performance of writing/reading to disk in single thread mode completed");
        }

        private static StringNumberPart[] GenerateInput(int countOfLines)
        {
            var ar = new StringNumberPart[countOfLines];

            var r = new Random();
            for (var i = 0; i < countOfLines; i++)
            {
                var line = new StringNumberPart(GetText(r), r.Next(1, DefaultValues.MaxNumberValue).ToString());
                ar[i] = line;
            }

            return ar;
        }

        private static string GetText(Random ran)
        {
            var run = ran.Next(DefaultValues.MinCharsInStringPart, DefaultValues.MaxCharsInStringPart);

            var st = "" + DefaultValues.UpperChars[ran.Next(0, DefaultValues.UpperChars.Length)];

            for (var i = 1; i < run; i++)
            {
                st = st + DefaultValues.LowerChars[ran.Next(0, DefaultValues.LowerChars.Length)];
            }

            return st;
        }
    }
}
