using AsyncTxtReader.Interfaces;
using AsyncTxtReader.Services;
using AsyncTxtReader.Services.TxtProcessers;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IFileReader, TxtFileReader>()
            .AddSingleton<IFileSearcher, FileSearcher>()
            .AddSingleton<ISpaceCounter, SpaceTxtCounter>()
            .AddSingleton<IResultPrinter, ConsoleResultPrinter>()
            .BuildServiceProvider();

        // Путь к директории с txt файлами
        string directoryPath = "C:\\Users\\yurij\\source\\repos\\AsyncTxtReader\\AsyncTxtReader\\Files";

        IFileSearcher fileSearcher = serviceProvider.GetRequiredService<IFileSearcher>();

        IEnumerable<string> filePaths;

        filePaths = fileSearcher.FindFiles(directoryPath, "*.txt");

        // Вариант А: Последовательная асинхронная обработка
        TextProcesser seqAsyncProcesser = new SeqAsyncProcesser(serviceProvider, filePaths);
        await seqAsyncProcesser.ProcessText();

        // Вариант Б: Одновременная асинхронная обработка
        TextProcesser parallelAsyncProcesser = new ParallelAsyncProcesser(serviceProvider, filePaths);
        await parallelAsyncProcesser.ProcessText();

        // Вариант В: Построчная асинхронная обработка
        TextProcesser stringParallelProcesser = new StringParallelProcesser(serviceProvider, filePaths);
        await stringParallelProcesser.ProcessText();
    }
}