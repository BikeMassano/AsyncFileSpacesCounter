using AsyncTxtReader.Interfaces;

namespace AsyncTxtReader.Services
{
    class ConsoleResultPrinter : IResultPrinter
    {
        /// <summary>
        /// Метод не нуждается в асинхронной реализации
        /// </summary>
        /// <param name="filePaths"></param>
        /// <param name="spaceCounts"></param>
        /// <param name="elapsedTime"></param>
        public void PrintResults(IEnumerable<string> filePaths, int[] spaceCounts, TimeSpan elapsedTime)
        {
            Console.WriteLine("Количество пробелов в каждом файле:");
            foreach (var i in Enumerable.Range(0, filePaths.Count()))
            {
                Console.WriteLine($"{filePaths.ElementAt(i)}: {spaceCounts[i]}");
            }
            Console.WriteLine($"\nОбщее время обработки: {elapsedTime}");
        }
    }
}
