using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XDSDotNet;
using Microsoft.Extensions.CommandLineUtils;
using static System.Console;

namespace XDSClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.HelpOption("-?|-h|--help");
            app.Command("iti18", command =>
            {
                command.Description = "Execute ITI18 RegistryStoredQuery by providing the request SOAP body";
                command.HelpOption("-?|-h|--help");
                var argument = command.Argument("requestfilename", "Request file name");
                command.OnExecute(() => {
                    if (argument.Value == null)
                    {
                        return ErrorAndRetval($"Argument {argument.Name} not specified");
                    }
                    return ITI18RegistryStoredQueryCommand.ExecuteFromFile(argument.Value);
                });
            });

            app.Command("finddocuments", command => {
                command.Description = "Execute ITI18 RegistryStoredQuery by providing a patient ID";
                command.HelpOption("-?|-h|--help");
                var argument = command.Argument("patientid", "Patient ID");
                command.OnExecute(() => {
                    if (argument.Value == null)
                    {
                        return ErrorAndRetval($"Argument {argument.Name} not specified");
                    }
                    return ITI18RegistryStoredQueryCommand.ExecuteUsingPatientId(argument.Value);
                });
            });

            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 2;
            });

            app.Execute(args);

        }

        static public int ErrorAndRetval(string msg)
        {
            Error.WriteLine($"ERROR: {msg}");
            return 2;
        }
    }
}
