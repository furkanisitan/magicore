using NUnit.Framework;

namespace MagiCore.Results.Tests;

[TestFixture]
public class ResultTests
{
    [TestCase(false, "Test message.", "error1", "error2")]
    public void Builder_WithoutData_AllPropertiesOfObjectsAreEqual(bool success, string message, params string[] errors)
    {
        var resultWithNew = new Result { Success = success, Message = message, Errors = errors };

        var resultWithBuilder = Result.Builder().Success(success).Message(message).Errors(errors).Build();

        Assert.AreEqual(resultWithNew.Message, resultWithBuilder.Message);
        Assert.AreEqual(resultWithNew.Success, resultWithBuilder.Success);
        Assert.That(resultWithNew.Errors, Is.EquivalentTo(resultWithBuilder.Errors));
    }

    [TestCase(false, "Test message.", 22)]
    [TestCase(true, "Test message 2.", "data")]
    public void Builder_WithData_AllPropertiesOfObjectsAreEqual<T>(bool success, string message, T payload)
    {
        var resultWithNew = new Result<T> { Success = success, Message = message, Payload = payload };

        var resultWithBuilder = Result.Builder(payload).Success(success).Message(message).Build();

        Assert.AreEqual(resultWithNew.Message, resultWithBuilder.Message);
        Assert.AreEqual(resultWithNew.Success, resultWithBuilder.Success);
        Assert.AreEqual(resultWithNew.Payload, resultWithBuilder.Payload);
    }
}