using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FileHelperHeaders.Zoom
{
    public class Vendor : IVendor
    {
        readonly Dictionary<string, Expression<Func<ZoomRankCsvRow, string>>> _urlLookup;
        readonly Dictionary<string, Expression<Func<ZoomRankCsvRow, int>>> _rankLookup;
        readonly string _key;

        public Vendor(string key)
        {
            _key = key;
            _urlLookup = new Dictionary<string, Expression<Func<ZoomRankCsvRow, string>>>
            {
                {"MSN", x => x.BingOrganicMatchedURL},
                {"Yahoo", x => x.YahooOrganicMatchedURL},
                {"Google", x => x.GoogleOrganicMatchedURL},
            };
            _rankLookup = new Dictionary<string, Expression<Func<ZoomRankCsvRow, int>>>
            {
                {"MSN", x => x.BingOrganicRank},
                {"Yahoo", x => x.YahooOrganicRank},
                {"Google", x => x.GoogleOrganicRank},
            };
        }

        public int GetRank(ZoomRankCsvRow row)
        {
            return _rankLookup.ContainsKey(_key) ? _rankLookup[_key].Compile()(row) : 0;
        }

        public string GetURL(ZoomRankCsvRow row)
        {
            return _urlLookup.ContainsKey(_key) ? _urlLookup[_key].Compile()(row) : "";
        }
    }
}