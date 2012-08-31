using FileHelperHeaders.Zoom;
using FubuTestingSupport;
using NUnit.Framework;

namespace FileHelperHeaders.Tests
{
    public class VendorSpecificRowTests
    {
        [Test]
        public void Can_pull_rank_and_url_by_vendor()
        {
            var row = new ZoomRankCsvRow
            {
                BingOrganicRank = 1,
                BingOrganicMatchedURL = "test"
            };

            var vendor = new Vendor("MSN");
            vendor.GetRank(row).ShouldEqual(1);
            vendor.GetURL(row).ShouldEqual("test");
        }
    }
}