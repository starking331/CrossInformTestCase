using System;
using System.Diagnostics;
using CrossInformAnalyzer;

namespace CrossInformTextAnalyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу с текстом");
            var filePath = Console.ReadLine();

            var analyzer = new TextAnalyzer();

            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var mostCommonSubstrings = analyzer.GetPopularSubstrings(3, 10, filePath);

                stopwatch.Stop();

                Console.WriteLine("Самые частые подстроки слов в формате: подстрока - количество вхождений");
                foreach (var (substring, substringCount) in mostCommonSubstrings)
                {
                    Console.WriteLine($"{substring} - {substringCount}");
                }

                Console.WriteLine($"Время работы программы: {stopwatch.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}