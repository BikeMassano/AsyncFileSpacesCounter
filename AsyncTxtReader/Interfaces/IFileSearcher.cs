namespace AsyncTxtReader.Interfaces
{
    interface IFileSearcher
    {
        IEnumerable<string> FindFiles(string directory,string prompt);
    }
}
