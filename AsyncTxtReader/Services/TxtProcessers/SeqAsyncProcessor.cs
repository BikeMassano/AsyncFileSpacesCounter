using AsyncTxtReader.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncTxtReader.Services.TxtProcessers
{
    class SeqAsyncProcesser : TextProcesser
    {
        public SeqAsyncProcesser(IServiceProvider serviceProvider, IEnumerable<string> filePaths)
        {
            _filePaths = filePaths;
            _fileOpener = serviceProvider.GetRequiredService<IFileReader>(); ;
            _spaceCounter = serviceProvider.GetRequiredService<ISpaceCounter>(); ;
            _printer = serviceProvider.GetRequiredService<IResultPrinter>(); ;
        }
        public override async Task ProcessText()
        {
            // Задаём начало отсчёта начала работы метода
            DateTime startTime = DateTime.Now;

            // Коллекция для хранения данных о кол-ве пробелов в файлах
            ICollection<int>? spaceCounts = [];

            foreach (string filePath in _filePaths)
            {
                int count = await Task.Run(() => ProcessFileAsync(filePath));
                spaceCounts.Add(count);
            }

            // Время завершения работы метода
            DateTime endTime = DateTime.Now;
            // Сумарное время выполнения метода
            TimeSpan elapsedTime = endTime - startTime;
            // Вывод результата
            _printer.PrintResults(_filePaths, spaceCounts.ToArray(), elapsedTime);
        }

        private async Task<int> ProcessFileAsync(string filePath)
        {
            try
            {
                string fileContent = await _fileOpener.ReadFileAsync(filePath);
                return _spaceCounter.CountSpaces(fileContent);
            }
            catch
            {
                return 0;
            }
        }
    }
}
