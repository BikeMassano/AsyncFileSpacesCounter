using AsyncTxtReader.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncTxtReader.Services.TxtProcessers
{
    class StringParallelProcesser : TextProcesser
    {
        public StringParallelProcesser(IServiceProvider serviceProvider, IEnumerable<string> filePaths)
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
            // Коллекция для хранения тасок
            ICollection<Task<int>> tasks = [];

            #region всякая чушь о состояниях
            // Заполняем коллекцию тасками
            // Когда задача добавляется в коллекцию tasks, она запускается асинхронно.
            // Когда таска добавляется её присваивается состояние Created, то есть она была создана, но ещё не запланирована планировщиком
            // Затем таска переходит в состояние WaitingForActivation, таска ждём планировщика и активации
            // Затем таска переходит в состояние WaitingToRun, то есть её выполнение было запланировано планировщиком, но оно ещё не начато
            // Затем переходит в состояние Running, то есть таска выполняется в выделенном потоке из пула потоков(если её быстрое выполнение невозможно в вызывающем потоке)
            // По завершению работы асинхронного метода таска переходит в состояние RanToCompletion, если таска завершилась успешно
            #endregion

            foreach (string filePath in _filePaths)
            {
                tasks.Add(ProcessFileAsync(filePath));
            }

            // Ждём результат выполнения тасок из массива, заносим эти результаты в массив
            int[] spaceCounts = await Task.WhenAll(tasks);

            // Время завершения работы метода
            DateTime endTime = DateTime.Now;
            // Сумарное время выполнения метода
            TimeSpan elapsedTime = endTime - startTime;
            // Вывод результата
            _printer.PrintResults(_filePaths, spaceCounts, elapsedTime);
        }

        private async Task<int> ProcessFileAsync(string filePath)
        {
            try
            {
                int totalSpaces = 0;
                await foreach (var line in File.ReadLinesAsync(filePath)) // Предполагается, что у _fileOpener есть метод ReadLinesAsync.
                {
                    totalSpaces += line.Count(c => c == ' ');
                }
                return totalSpaces;
            }
            catch (FileNotFoundException)
            {
                // Логируем исключение для лучшей отладки. Не следует молча игнорировать все исключения.
                Console.Error.WriteLine($"Файл не найден: {filePath}");
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Ошибка обработки файла {filePath}: {ex.Message}");
                return 0;
            }
        }
    }
}

