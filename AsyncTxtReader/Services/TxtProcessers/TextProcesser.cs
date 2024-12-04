using AsyncTxtReader.Interfaces;

namespace AsyncTxtReader.Services.TxtProcessers
{
    abstract class TextProcesser
    {
        protected IEnumerable<string> _filePaths { get; set; } = null!;
        protected IFileReader _fileOpener { get; set; } = null!;
        protected ISpaceCounter _spaceCounter { get; set; } = null!;
        protected IResultPrinter _printer { get; set; } = null!;
        public abstract Task ProcessText();
    }
}
