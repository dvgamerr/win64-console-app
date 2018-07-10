﻿using System;
using System.IO;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static bool IsDebug = false;
        static bool IsWaitForClose = false;
        static bool IsWaitCompleted = false;

        static void WatchFile()
        {

        }
        static void Main(string[] args)
        {
            string sConfigFile = null;
            FileINI cConfig = null;

            try
            {
                for (var i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "--debug": IsDebug = true; break;
                        case "--config":
                            if (i + 1 > args.Length) throw new Exception("configuration not found path.");
                            sConfigFile = args[i + 1].Replace("\"", "");
                            break;
                    }
                }
                if (!File.Exists(sConfigFile)) throw new Exception("configuration not extsts path.");

                cConfig = new FileINI(sConfigFile);

                Log("sConfig: {0}", sConfigFile);
                Log("IsDebug: {0}", IsDebug);
                if (IsDebug)
                {
                    Log("--> Please any key to Start Program.");
                    Console.ReadKey();
                }
                Log("");
                Log("Program Processing...");
                Log("");
                do
                {
                    WatchFile();

                    while(IsWaitForClose)
                    {
                        Thread.Sleep(1);
                        if (IsWaitCompleted) break;
                    }
                    if (IsWaitCompleted) break;
                    Thread.Sleep(1);
                } while (true);
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }
        static void Log(string msg, object arg)
        {
            Log(msg, new object[] { arg });
        }
        static void Log(string msg = null, object[] arg = null)
        {
            if (IsDebug)
            {
                if (msg != null)
                {
                    if (arg == null) Console.WriteLine(" " + msg); else Console.WriteLine(" " + msg, arg);
                }
                else
                {
                    Console.WriteLine("");
                }
            }
        }
        static void Error(Exception ex)
        {
            if (IsDebug)
            {
                Log("---------------------------------------------------------------------");
                if (ex.Source != null) Log(ex.Source);
                Log(ex.Message);
                Log("---------------------------------------------------------------------");
                Log("--> Next, please any key.");
                Console.ReadKey();
            }
        }
    }
}
