using AsyncTxtReader.Interfaces;

namespace AsyncTxtReader.Services
{
    class TxtFileReader : IFileReader
    {
        /// <summary>
        /// Использование асинхронности обосновано т.к. внутри метода происходят IO операции
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<string> ReadFileAsync(string filePath)
        {
            using StreamReader reader = new StreamReader(filePath);
            return await reader.ReadToEndAsync();
        }
    }
}
