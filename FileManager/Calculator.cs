using System;
using System.IO;

namespace Calculator
{
    class Program
    {
        private static int concatenationCount = 0;
        private static readonly string resultTempFile = "result.txt";

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            Console.WriteLine("Текстовый калькулятор. Введите две строки для сцепления или 'exit' для выхода.");
            Log("Приложение запущено");

            try
            {
                if (File.Exists(resultTempFile))
                    File.Delete(resultTempFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации: {ex.Message}");
                Log($"Ошибка инициализации: {ex.Message}");
            }

            while (true)
            {
                string input1 = GetInput("Введите первую строку: ");
                if (input1 == null) break;

                string input2 = GetInput("Введите вторую строку: ");
                if (input2 == null) break;

                string result = input1 + input2;
                concatenationCount++;

                Console.WriteLine($"Результат сцепления: {result}");
                Log($"Сцепление #{concatenationCount}: {input1} + {input2} = {result}");
                SaveResult(result);
            }

            FinalizeResults();
            Log("Работа приложения завершена");
        }

        private static string GetInput(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (input?.ToLower() == "exit") return null;

            Log($"Ввод: {prompt.Trim()} '{input}'");
            return input;
        }

        private static void Log(string message)
        {
            string logDir = "Logs";
            string logFile = Path.Combine(logDir, $"{DateTime.Now:yyyy-MM-dd}.log");

            try
            {
                Directory.CreateDirectory(logDir);
                File.AppendAllText(logFile, $"[{DateTime.Now:HH:mm:ss}] {message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка записи лога: {ex.Message}");
            }
        }

        private static void SaveResult(string result)
        {
            try
            {
                File.AppendAllText(resultTempFile, $"{result}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения: {ex.Message}");
                Log($"Ошибка сохранения результата: {ex.Message}");
            }
        }

        private static void FinalizeResults()
        {
            try
            {
                if (!File.Exists(resultTempFile)) return;

                string resultsDir = "Results";
                Directory.CreateDirectory(resultsDir);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string destFile = Path.Combine(resultsDir, $"result_{timestamp}.txt");

                File.Move(resultTempFile, destFile);
                Log($"Результаты сохранены в: {destFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка финализации: {ex.Message}");
                Log($"Ошибка финализации: {ex.Message}");
            }
        }

        private static void OnProcessExit(object sender, EventArgs e)
        {
            FinalizeResults();
        }
    }
}
