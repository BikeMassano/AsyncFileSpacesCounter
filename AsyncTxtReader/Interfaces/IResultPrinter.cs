namespace AsyncTxtReader.Interfaces
{
    interface IResultPrinter
    {
        void PrintResults(IEnumerable<string> filePaths, int[] spaceCounts, TimeSpan elapsedTime);
    }
}
