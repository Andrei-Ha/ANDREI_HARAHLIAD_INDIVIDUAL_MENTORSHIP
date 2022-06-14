using Exadel.Forecast.BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string strFromUser = string.Empty;
            while (strFromUser != "q")
            {
                //Console.WriteLine(ConfigurationManager.AppSettings["OPENWEATHER_API_KEY"]);
                Console.WriteLine($"Type the name of city to get the forecast, please{Environment.NewLine} To quit type \"q\"");
                strFromUser = Console.ReadLine();
                if (strFromUser == "q")
                    break;
                Console.WriteLine(Handler.GetWeatherByName(strFromUser));
            }
        }
    }
}
