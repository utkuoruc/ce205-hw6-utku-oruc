using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using LargeTextFilesSorting;
using LargeTextFilesSorting.Utils;
using Mono.Options;
using System.Threading;
using System.Windows.Input;

namespace LargeTextFilesSorting.App
{
    public partial class Form1 : Form
    {
        static bool _generateFile;
        static TestFileSize _testFileSize = TestFileSize.Gb1;

        static bool _runPerformanceTest;

        static bool _runSorting;
        static string _sortingFilePath;

        static bool _showHelp;

        static bool _runCheck;
        static string _checkingFilePath;

        public Form1()
        {
            InitializeComponent();
        }
        private void generate_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine();
            new TestFileGenerator().GenerateInputFile(_testFileSize);
        }

        private void run_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine();
            new SortingManager(DefaultValues.InputFileName, DefaultValues.OutputFileName).ProcessFile();
        }

        private void folder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.FileName = "explorer.exe";
            startInfo.Arguments = ".";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = false;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            process.Close();

            //Process.Start("explorer.exe", @".");
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
