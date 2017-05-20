using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Diagnostics;

namespace EasyLogListener
{
    class Program
    {
        const int PRE_DEV_SIGNAL = 1;
        const int DEV_SIGNAL = 2;
        const int QA_SIGNAL = 3;
        const int QA2_SIGNAL = 4;
        const int PROD_SIGNAL = 5;
        const int STOP_SIGNAL = 6;
        
        const int BYTES_TO_READ = 2;

        const String PATH_TO_GIT_SHELL = @"C:\DevTools\Git\bin\sh.exe";

        static void Main(string[] args)
        {
            using (SerialPort objSerialPort = new SerialPort("COM6", 9600))
            {
                objSerialPort.Open();

                using (Stream objStream = objSerialPort.BaseStream)
                {
                    while (true)
                    {
                        try
                        {
                            int intEnvironmentCode = WaitForEnvironmentCode(objStream);
                            PullLogs(intEnvironmentCode);
                        }
                        catch (BadSerialDataException ex)
                        {
                            //just don't do anything with the bad data                            
                        }
                    }
                }
            }
        }

        static int WaitForEnvironmentCode(Stream objStream)
        {
            byte[] bytBuffer = new byte[BYTES_TO_READ];
            int intBytesRead = 0;
            int intTotalBytesRead = 0;

            while (intTotalBytesRead < BYTES_TO_READ)
            {
                intBytesRead = objStream.Read(bytBuffer, intTotalBytesRead, BYTES_TO_READ - 1);
                if (intBytesRead > 0) System.Diagnostics.Debug.WriteLine(System.Text.Encoding.UTF8.GetString(bytBuffer));
                else System.Diagnostics.Debug.WriteLine("no bytes");

                intTotalBytesRead += intBytesRead;

                System.Threading.Thread.Sleep(500);
            }

            String strByteString = System.Text.Encoding.UTF8.GetString(bytBuffer);
            int intByte1 = Int32.Parse(strByteString.Substring(0,1));
            int intByte2 = Int32.Parse(strByteString.Substring(1,1));

            if (intByte2 == STOP_SIGNAL && intByte1 >= PRE_DEV_SIGNAL && intByte1 <= PROD_SIGNAL)
            {
                return bytBuffer[0];
            }
            else
            {
                throw new BadSerialDataException();
            }
        }

        static void PullLogs(int intEnvironmentCode)
        {
            System.Diagnostics.Debug.WriteLine(AppConfigWrapper.ShellLogin);
            DoShellCommand(AppConfigWrapper.ShellLogin);
            DoShellCommand(AppConfigWrapper.ShellBuild);
            DoShellCommand(AppConfigWrapper.ShellDownload);
        }

        static void DoShellCommand(String strCommand)
        {
            Process objProcess;
            StreamReader objStdOutReader;

            objProcess = new Process();
            objProcess.StartInfo.FileName = PATH_TO_GIT_SHELL;
            objProcess.StartInfo.Arguments = strCommand;
            objProcess.StartInfo.RedirectStandardOutput = true;
            objProcess.StartInfo.UseShellExecute = false;
            objProcess.StartInfo.WorkingDirectory = AppConfigWrapper.WorkingDirectory;
            objProcess.Start();

            objStdOutReader = objProcess.StandardOutput;
            objProcess.WaitForExit();

            System.Diagnostics.Debug.Write(objStdOutReader.ReadToEnd());
        }
    }
}
