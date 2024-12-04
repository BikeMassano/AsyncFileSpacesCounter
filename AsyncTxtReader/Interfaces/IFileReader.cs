namespace AsyncTxtReader.Interfaces
{
    interface IFileReader
    {
        Task<string> ReadFileAsync(string filePath);
    }
}
