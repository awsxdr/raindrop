namespace Raindrop.Configuration
{
    using System.IO;

    public class FileStreamProvider : IFileStreamProvider
    {
        public Stream OpenFileForRead(string path) =>
            File.OpenRead(path);
    }
}
