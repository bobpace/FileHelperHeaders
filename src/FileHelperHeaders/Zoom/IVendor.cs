namespace FileHelperHeaders.Zoom
{
    public interface IVendor
    {
        int GetRank(ZoomRankCsvRow row);
        string GetURL(ZoomRankCsvRow row);
    }
}