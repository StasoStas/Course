using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Lesson_6_App_1
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);
        static void Main(string[] args)
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3);

            string catalog = String.Empty;
            string filename = "catalog.txt";
            string path = @"C:\Users\Intel\Desktop\LabMoscow\Lesson6\less_6_1\less_6";
            DirectoryInfo dir = new DirectoryInfo(path);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(path);
            Console.ForegroundColor = ConsoleColor.White;
            GetDirectoriesAndFilesList(dir);

            Console.ReadLine();
        }
        static void GetDirectoriesAndFilesList(DirectoryInfo path, string prefix = "")
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            files = path.GetFiles(" *.*"); // перебираем файлы
            if (files != null)
            {
                Console.ForegroundColor = ConsoleColor.White;
                foreach (FileInfo file in files.Take(files.Length - 1))
                {
                    Console.Write(prefix + "├── ");
                    Console.WriteLine(file.Name);
                }
                if (files.LastOrDefault() != null)
                {
                    Console.Write(prefix + "└── ");
                    Console.WriteLine(files.LastOrDefault().Name);
                }
            }

            subDirs = path.GetDirectories(); // перебираем папки
            if (subDirs != null)
            {
                foreach (DirectoryInfo directory in subDirs.Take(subDirs.Length - 1))
                {
                    Console.Write(prefix + "├── ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(directory.Name);
                    Console.ForegroundColor = ConsoleColor.White;
                    GetDirectoriesAndFilesList(directory, prefix + "│   ");
                }
                if (subDirs.LastOrDefault() != null)
                {
                    Console.Write(prefix + "└── ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(subDirs.LastOrDefault().Name);
                    Console.ForegroundColor = ConsoleColor.White;
                    GetDirectoriesAndFilesList(subDirs.LastOrDefault(), prefix + "    ");
                }
            }
        }
    }
}
