/****************************************************************************
 * Copyleft (L) 2022 CENG - All Rights Not Reserved
 * You may use, distribute and modify this code.
 ****************************************************************************/

/**
 * @file Program.cs
 * @author Utku Oruc
 * @date 25 January 2022
 *
 * @brief <b> Console program, takes arguments </b>
 *
 * Program.cs
 *
 * @see http://bilgisayar.mmf.erdogan.edu.tr/en/
 *
 */
using System;
using System.IO;
using System.Linq;
using LargeTextFilesSorting;
using LargeTextFilesSorting.Utils;
using Mono.Options;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;

namespace LargeTextFilesSorting.Console
{
    class Program
    {
        static bool _generateFile;
        static TestFileSize _testFileSize = TestFileSize.Gb1;

        static bool _runPerformanceTest;

        static bool _runSorting;
        static string _sortingFilePath;

        static bool _showHelp;

        static bool _runCheck;
        static string _checkingFilePath;

        private static readonly OptionSet Options = new OptionSet
        {
            {
                "t|perftest", "Run basic performance tests.", pt =>
                {
                    _runPerformanceTest = true;

                    System.Console.WriteLine($"Selected performance test.");
                }
            },
            {
                "g|generate:", $"Generate test file with specified file size (possible sizes: {Enum.GetNames(typeof(TestFileSize)).Aggregate((f,s) => $"{f}, {s}")}). By default 100Mb will be used.", g =>
                {
                    _generateFile = true;
                    _testFileSize = ParseTestFileSize(g);

                    System.Console.WriteLine($"Selected generation of test file. Selected size of test file: {_testFileSize}");
                }
            },
            {
                "r|run:", "Run sorting of specified file. Without path of file 'test.txt' will be used", rs =>
                {
                    _runSorting = true;
                    _sortingFilePath = string.IsNullOrWhiteSpace(rs) ? DefaultValues.InputFileName : rs;

                    System.Console.WriteLine($"Selected sorting. Selected name of test file: {_sortingFilePath}");
                }
            },
            {
                "c|check:", "Run checking of output file for correct sorting", rs =>
                {
                    _runCheck = true;
                    _checkingFilePath = string.IsNullOrWhiteSpace(rs) ? GetOutputFileName(DefaultValues.InputFileName) : rs;

                    System.Console.WriteLine($"Selected checking. Selected name of test file: {_checkingFilePath}");
                }
            },
            {
                "h|help", "Show help and exit", h => _showHelp = h != null
            }
        };

        static void Main(string[] args)
        {

            if (args == null || !args.Any())
            {
                ShowHelp();
                return;
            }
            try
            {
                Options.Parse(args);
            }
            catch (OptionException e)
            {
                System.Console.WriteLine($"Error during parsing input arguments: {e.ToString()}");
                ShowHelp();
                return;
            }
            try
            {
                if (_runPerformanceTest)
                {
                    var pt = new PerfTest();
                    pt.RunMemoryTest();
                    System.Console.WriteLine();
                    pt.RunFileSystemTest();
                }

                if (_generateFile)
                {
                    System.Console.WriteLine();
                    new TestFileGenerator().GenerateInputFile(_testFileSize);
                }

                if (_runSorting)
                {
                    System.Console.WriteLine();
                    new SortingManager(_sortingFilePath, GetOutputFileName(_sortingFilePath)).ProcessFile();
                }

                if (_runCheck)
                {
                    System.Console.WriteLine();
                    new SortOrderChecher(_checkingFilePath).CheckSortingOrder();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"Error during execution: {e.ToString()}");
                return;
            }

            if (_showHelp)
            {
                ShowHelp();
            }
        }

        static void ShowHelp()
        {
            System.Console.WriteLine("Usage: LargeTestFilesSorting.Console.exe [OPTIONS]");
            System.Console.WriteLine();

            System.Console.WriteLine("Options:");
            Options.WriteOptionDescriptions(System.Console.Out);
        }

        static TestFileSize ParseTestFileSize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return TestFileSize.Gb1;
            }

            TestFileSize testFileSize;
            if (Enum.TryParse(value, true, out testFileSize) && Enum.IsDefined(typeof(TestFileSize), testFileSize))
            {
                return testFileSize;
            }

            return TestFileSize.Gb1;
        }

        static string GetOutputFileName(string inputFilePath)
        {
            if (string.IsNullOrWhiteSpace(inputFilePath))
            {
                throw new ArgumentNullException(nameof(inputFilePath));
            }

            return Path.Combine(Path.GetDirectoryName(inputFilePath), $"{Path.GetFileNameWithoutExtension(inputFilePath)}.output.csv");
        }
    }
}
