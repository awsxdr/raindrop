using System.IO;

namespace Raindrop.Configuration
{
    public interface IFileStreamProvider
    {
        Stream OpenFileForRead(string path);
    }
}
