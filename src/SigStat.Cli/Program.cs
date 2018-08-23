using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

//Ez is lehetne helyette: https://github.com/natemcmaster/CommandLineUtils
using Microsoft.Extensions.CommandLineUtils;

using SigStat.Common;
using SigStat.Common.Loaders;
using SigStat.Common.Helpers;
using System.IO;

namespace SigStat.Cli
{
    //CLI guidelines:
    //Do use Commands when semantically identifying an action
    //Do use Options to enable configuration to a specific Command
    //Favor verb for command name
    //Favor adjective or noun for option name

    class Program
    {
        static Logger cliLogger;
     
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = "sigstat";//Root Command
            app.HelpOption("-?|-h|--help");

            app.OnExecute(() => {
                //ez akkor fut le, amikor a root commandot argument nelkul hivjuk
                Console.WriteLine("sigstat cli");
                return 0;
            });

            app.Command("log", (command) =>
            {
                command.Description = "Initialize a logger.";
                command.HelpOption("-?|-h|--help");

                var levelArg = command.Argument("[level]", "Level of logging.");//TODO: felsorolni a szinteket a help ben.
                var pathArg = command.Argument("[path]", "File path to log messages.");

                var helloOpt = command.Option("-he|--hello", "Option to say Hello World.", CommandOptionType.NoValue);
                var displayOpt = command.Option("-d|--display", "Option to display log messages on console.", CommandOptionType.NoValue);

                command.OnExecute(() =>
                {
                    //init logger
                    string path = pathArg.Value ?? $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log";
                    FileStream fs = new FileStream(path, FileMode.Create);
                    if (displayOpt.HasValue())
                        cliLogger = new Logger(LogLevel.Debug, fs, LogConsole);//TODO: levelArg alapjan beallitani
                    else
                        cliLogger = new Logger(LogLevel.Debug, fs, null);

                    if (helloOpt.HasValue())
                        cliLogger.Info(cliLogger, "Hello World!");
                    return 0;
                });
            });

            //TODO: add more commands
            //valahogy lehetne kulon .cs be irni a commandokat, mint dotnet-cli ben

            app.Execute(args);
        }

        public static void LogConsole(LogLevel l, string message)
        {
            switch (l)
            {
                case LogLevel.Fatal:
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                default:
                    break;
            }
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }


    }
}

