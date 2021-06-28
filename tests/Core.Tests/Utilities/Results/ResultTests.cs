using Core.Utilities.Results;
using NUnit.Framework;

namespace Core.Tests.Utilities.Results
{
    [TestFixture]
    public class ResultTests
    {
        [TestCase(false, "Test message.")]
        public void Builder_WithoutData_AllPropertiesOfObjectsAreEqual(bool success, string message)
        {
            var resultNew = new Result { Success = success, Message = message };

            var resultBuilder = Result.Builder().Success(success).Message(message).Build();

            Assert.AreEqual(resultNew, resultBuilder);
        }


    }
}
