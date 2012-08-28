using System.Collections.Generic;

namespace FileHelperHeaders
{
    public interface ICsvFileParser
    {
        IEnumerable<T> For<T>(string path, bool ignoreHeaders = false);
    }
}