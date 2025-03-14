using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
    internal class Calculator
    {

        public static string RESULTFILE = @"result.txt";
        static void Main(string[] args)
        {
            try
            {
                string[] texts = new string[2];
                Console.WriteLine("Введите текст");
                texts[0] = Console.ReadLine();
                log($"Введён текст {texts[0]}");
                Console.WriteLine("Введите текст");
                texts[1] = Console.ReadLine();
                log($"Введён текст {texts[1]}");

                if (texts[0].Length == 0 || texts[1].Length == 0)
                {
                    log($"Один из текстов пустой!");
                    Console.WriteLine("Один из текстов пустой!");
                    return;
                }

                Console.WriteLine($"Нажми на кнопку для вывода результата.");
                Console.ReadKey();

                string result = $"{texts[0]}{texts[1]}";
                log($"Выведен результат в файл result.txt");
                string fresult = $"{result}";
                File.AppendAllText(RESULTFILE, "\nРезультат: " + fresult);
                Console.WriteLine($"изи. {result}");
                log($"Завершение работы программы.");
            } catch (Exception e)
            {
                log($"{e.Message}");
            }
        }

        private static void log(string log)
        {
            string logFormat = $"[{DateTime.Now.ToString()}] {log}\n";
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            File.AppendAllText($"{@"log-(" + day + "." + month + "." + year + ").txt"}", logFormat); ;
        }
    }
}
