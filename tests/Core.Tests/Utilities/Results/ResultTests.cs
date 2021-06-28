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
            var resultWithNew = new Result { Success = success, Message = message };

            var resultWithBuilder = Result.Builder().Success(success).Message(message).Build();

            Assert.AreEqual(resultWithNew.Message, resultWithBuilder.Message);
            Assert.AreEqual(resultWithNew.Success, resultWithBuilder.Success);
        }

        [TestCase(false, "Test message.", 22)]
        [TestCase(true, "Test message 2.", "data")]
        public void Builder_WithData_AllPropertiesOfObjectsAreEqual<T>(bool success, string message, T data)
        {
            var resultWithNew = new Result<T> { Success = success, Message = message, Data = data };

            var resultWithBuilder = Result.Builder(data).Success(success).Message(message).Build();

            Assert.AreEqual(resultWithNew.Message, resultWithBuilder.Message);
            Assert.AreEqual(resultWithNew.Success, resultWithBuilder.Success);
            Assert.AreEqual(resultWithNew.Data, resultWithBuilder.Data);
        }

    }
}
