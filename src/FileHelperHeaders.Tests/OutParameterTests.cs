using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FileHelperHeaders.Tests
{
    public class OutParameterTests : InteractionContext<ClassThatUsesDependencyWithOutputParameters>
    {
        [Test]
        public void Can_mock_out_parameters()
        {
            var outputParameter = MockFor<IOutParameter>();
            bool value;
            outputParameter.Expect(x => x.WhyUseThese(out value)).OutRef(true);
            var result = ClassUnderTest.GetOutputParameter();
            result.ShouldBeTrue();
        }
    }

    public class ClassThatUsesDependencyWithOutputParameters
    {
        readonly IOutParameter _outputParameter;

        public ClassThatUsesDependencyWithOutputParameters(IOutParameter outputParameter)
        {
            _outputParameter = outputParameter;
        }

        public bool GetOutputParameter()
        {
            bool test;
            _outputParameter.WhyUseThese(out test);
            return test;
        }
    }
}