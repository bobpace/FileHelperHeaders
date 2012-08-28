using System.Collections.Generic;
using System.Linq;
using FileHelpers;

namespace FileHelperHeaders
{
    public class CsvFileParser : ICsvFileParser
    {
        public IEnumerable<T> For<T>(string path, bool ignoreHeaders = false)
        {
            var engine = new FileHelperAsyncEngine(typeof(T));
            if (ignoreHeaders)
            {
                engine.Options.IgnoreFirstLines = 1;
            }
            engine.BeginReadFile(path);
            return engine.Cast<T>();
        }
    }
}