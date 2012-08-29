using System;
using System.IO;
using System.Linq;
using FileHelpers;
using FubuTestingSupport;
using NUnit.Framework;

namespace FileHelperHeaders.Tests
{
    public class CsvFileParserTests : InteractionContext<CsvFileParser>
    {
        [Test]
        public void Test()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testData.csv");
            //const string headerText = "Phone#,FirstName,LastName,#OfPeopleInParty";
            var results = ClassUnderTest.For<TestData>(filePath, true /*ignore headers*/).ToList();
            //8014037002,Bob,Pace,1
            //8015555555,Fake,Person,5
            results.Count.ShouldEqual(2);
            results[0].PhoneNumber.ShouldEqual("8014037002");
            results[0].FirstName.ShouldEqual("Bob");
            results[0].LastName.ShouldEqual("Pace");
            results[0].NumberOfPeopleInParty.ShouldEqual(1);
            results[1].NumberOfPeopleInParty.ShouldEqual(0);
        }

        [DelimitedRecord(",")]
        public class TestData
        {
            public string PhoneNumber;
            public string FirstName;
            public string LastName;
            [FieldNullValue(0)]
            public int NumberOfPeopleInParty;
        }
    }

}