namespace CalcFunctionApp.Test
{
    using CalcFunctionApp.Test.Helper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;
    using NUnit.Framework;
    using SumFunctionApp;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestFixture]
    public class SumTests : FunctionTest
    {
        [TestCase(3, 4, ExpectedResult = 7)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(2.3, 1.8, ExpectedResult = 4.1)]
        [TestCase(-4, 8.2, ExpectedResult = 4.2)]
        public async Task<double> SumRequest_Valid_Query(double a, double b)
        {
            var query = new Dictionary<string, StringValues>();
            query.TryAdd("a", a.ToString());
            query.TryAdd("b", b.ToString());

            var body = "";

            var result = await Calc.Run(req: HttpRequestSetup(query, body), Log);
            var resultObject = (OkObjectResult)result;

            if (!double.TryParse(resultObject.Value.ToString(), out double resultValue))
            {
                Assert.Fail("Result is not double.");
            }

            return resultValue;
        }

        [Test]
        public async Task SumRequest_Invalid_Query()
        {
            var query = new Dictionary<string, StringValues>();
            query.TryAdd("a", "1");            

            var body = "";

            var result = await Calc.Run(req: HttpRequestSetup(query, body), Log);

            Assert.AreEqual(result.GetType(), typeof(BadRequestObjectResult));
        }

        [Test]
        public async Task SumRequest_Valid_Query_WrongArgument()
        {
            var query = new Dictionary<string, StringValues>();
            query.TryAdd("a", "1");
            query.TryAdd("b", "number");

            var body = "";

            var result = await Calc.Run(req: HttpRequestSetup(query, body), Log);

            Assert.AreEqual(result.GetType(), typeof(BadRequestObjectResult));
        }
    }
}
