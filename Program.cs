using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecCommandWindows
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = ConfigurationManager.AppSettings;
            string app = args.Any() && !String.IsNullOrEmpty(args[0]) ? args[0] : settings["app"];
            string command = args.Any() && args.Length > 1 && !String.IsNullOrEmpty(args[1]) ? args[1] : settings["arg"];
            string timeout = args.Any() && args.Length > 2 && !String.IsNullOrEmpty(args[2]) ? args[2] : settings["timeout"];
            string continues = args.Any() && args.Length > 3 && !String.IsNullOrEmpty(args[3]) ? args[3] : settings["continues"];

            Console.WriteLine($" ||| Inicio do processo :-)");

            int iTimeout = 0;
            if (!String.IsNullOrEmpty(timeout) && !int.TryParse(timeout, out iTimeout)) 
            {
                iTimeout = 60;
            }
            bool bContinues = continues.ToLower() == "true";

            command = command.Replace(",", " ");
            if (String.IsNullOrEmpty(command)) 
            {
                Console.WriteLine(" | Não preenchido a linha de comando esperado :-(");
                return;
            }            

            ProcessStartInfo cmdsi = new ProcessStartInfo(app);
            cmdsi.Arguments = command;
            Process cmd = Process.Start(cmdsi);
            Console.WriteLine($" || Será Executado o commando abaixo com o timeout de {timeout} segundos S2");
            Console.WriteLine($" | ============================================================ ");
            Console.WriteLine($" | \"{app} {command}\" ");
            Console.WriteLine($" | ============================================================ ");

            if (iTimeout > 0)
                cmd.WaitForExit(iTimeout*1000); 
            else
                cmd.WaitForExit();

            Console.WriteLine($" ||| Termino do processo! \\o/");
            if (!bContinues)
            {
                Console.WriteLine(" ||| Digite uma tecla para finalizar!");
                Console.ReadKey();
            }
                

        }        
    }
}
