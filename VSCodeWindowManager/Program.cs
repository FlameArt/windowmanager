using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace VSCodeWindowManager
{
    internal class Program
    {


        [DllImport("User32.dll")]
        public extern static bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">
        /// -name findproc by part of process name
        /// -title findproc by part of title
        /// -pid findproc by pid
        /// -x pos x
        /// -y pos y
        /// -h height
        /// -w width
        /// </param>
        static void Main(string[] args)
        {

            try
            {

                // Find processes
                List<Process> findedProc = new List<Process> { };

                if (findArg("name", args) != null || findArg("title", args) != null)
                    findedProc = findProcsByNamePart(findArg("name", args), findArg("title", args)).ToList();
                else if (findArg("pid", args) != null)
                    findedProc.Add(Process.GetProcessById(int.Parse(findArg("pid", args))));
                else
                    throw new Exception("Process not found");

                // Move window for each process
                foreach (var proc in findedProc)
                {
                    // Unmaximize, unminimize
                    ShowWindow(proc.MainWindowHandle, 1);
                    // Move and resize
                    MoveWindow(proc.MainWindowHandle, int.Parse(findArg("x", args) ?? "0"), int.Parse(findArg("y", args) ?? "0"), int.Parse(findArg("w", args) ?? "100"), int.Parse(findArg("h", args) ?? "100"), true);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        static string findArg(string name, string[] args)
        {
            for (var i = 0; i + 1 < args.Length; i += 2)
            {
                if (args[i].Replace("-", "") == name) return args[i + 1];
            }
            return null;
        }

        static List<Process> findProcsByNamePart(string name = null, string title = null)
        {
            var result = new List<Process> { };
            foreach (var proc in Process.GetProcesses())
                if (title != null && proc.MainWindowTitle.ToLower().Contains(title.ToLower()) || name != null && proc.ProcessName.ToLower().Contains(name.ToLower()))
                    result.Add(proc);

            if (result.Count == 0) throw new Exception("Process not found (by part of name/title)");

            return result;
        }
    }


}
