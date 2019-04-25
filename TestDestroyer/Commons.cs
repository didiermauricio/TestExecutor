using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace TestDestroyer
{
    public class Commons
    {
        public string ChooseFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName.ToString();
            }
            else
            {
                return null;
            }
        }

        public void ExecuteCommandNunit(string dllPath)
        {
            string outputResults = String.Empty;
            
            string testAdaptor = Path.Combine("C:\\", "Program Files (x86)", "Microsoft Visual Studio", "2017",
                "TestAgent", "Common7", "IDE", "CommonExtensions", "Microsoft", "TestWindow", "net35");
            string command = "vstest.console.exe " + dllPath + " /TestAdapterPath:\"" + testAdaptor + "\"";
            System.Diagnostics.ProcessStartInfo procStartInfo =
                new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

            // The following commands are needed to redirect the standard output.
            // This means that it will be redirected to the Process.StandardOutput StreamReader.
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.WorkingDirectory = Path.Combine("C:\\","Program Files (x86)","Microsoft Visual Studio","2017","TestAgent","Common7","IDE","CommonExtensions","Microsoft","TestWindow");
            procStartInfo.UseShellExecute = false;
            // Do not create the black window.
            procStartInfo.CreateNoWindow = false;
            // Now we create a process, assign its ProcessStartInfo and start it
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            string result = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
            // Get the output into a string
            
            // Display the command output.
            if (result.Length > 0)
            {
                outputResults += result;
            }
           
            Console.WriteLine(outputResults);
            
            SaveFile(outputResults, "ExecutedTests");

        }
        public void ExecuteCommandMsTest(string dllPath)
        {
            string wd = Path.Combine("C:\\", "Program Files (x86)", "Microsoft Visual Studio", "2017", "TestAgent", "Common7", "IDE", "CommonExtensions", "Microsoft", "TestWindow");
            //string wd = Path.Combine("C:\\",  "TestWindow");
            string tSettings = " /Settings:Local.testsettings"; 
            string outputResults = String.Empty;
            
            string command = "vstest.console.exe " + dllPath + tSettings;
            System.Diagnostics.ProcessStartInfo procStartInfo =
                new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

            // The following commands are needed to redirect the standard output.
            // This means that it will be redirected to the Process.StandardOutput StreamReader.
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.WorkingDirectory = wd;
            procStartInfo.UseShellExecute = false;
            // Do not create the black window.
            procStartInfo.CreateNoWindow = false;
            // Now we create a process, assign its ProcessStartInfo and start it
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();

            // Get the output into a string
            string result = proc.StandardOutput.ReadToEnd();
            
            proc.WaitForExit();
            
            // Display the command output.
            if (result.Length > 0)
            {
                outputResults += result;
            }
            
            SaveFile(outputResults, "ExecutedTests");
            
        }

        public void SaveFile(string content, String name)
        {
            try
            {
                string dateTime = DateTime.Now.ToString("yyyy_MM_dd");
                string fileName = "C:\\" + name + "TestResults" + dateTime + ".txt";
                File.WriteAllText(fileName, content);
                string notepadExe = Path.Combine("C:\\", "Program Files", "Notepad++", "Notepad++.exe");
                Process.Start(notepadExe, fileName);
            }
            catch (Exception e)
            {
                MessageBox.Show("Execution finished but there are problems opening Results: "+e.ToString());
            }
            
        }
    }
}
