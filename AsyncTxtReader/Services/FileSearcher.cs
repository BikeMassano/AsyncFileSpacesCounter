using AsyncTxtReader.Interfaces;

namespace AsyncTxtReader.Services
{
    class FileSearcher : IFileSearcher
    {
        /// <summary>
        /// Метод поиска файлов по паттерну в заданной директории не реализован асинхронно, 
        /// т.к. метод Directory.GetFiles блокирует поток, а его асинхронная реализация не была найдена
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public IEnumerable<string> FindFiles(string directory, string searchPattern)
        {
            return Directory.GetFiles(directory, searchPattern);
        }
    }
}
