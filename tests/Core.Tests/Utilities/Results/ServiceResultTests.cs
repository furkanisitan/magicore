using Core.Utilities.Results;
using NUnit.Framework;

namespace Core.Tests.Utilities.Results;

[TestFixture]
public class ServiceResultTests
{
    [TestCase(false, "Test message.", 200)]
    public void Builder_WithoutData_AllPropertiesOfObjectsAreEqual(bool success, string message, int statusCode)
    {
        var serviceResultWithNew = new ServiceResult { Success = success, Message = message, StatusCode = statusCode };

        var serviceResultWithBuilder = ServiceResult.Builder().Success(success).Message(message).StatusCode(statusCode).Build();

        Assert.AreEqual(serviceResultWithNew.Message, serviceResultWithBuilder.Message);
        Assert.AreEqual(serviceResultWithNew.Success, serviceResultWithBuilder.Success);
        Assert.AreEqual(serviceResultWithNew.StatusCode, serviceResultWithBuilder.StatusCode);
    }

    [TestCase(false, "Test message.", 200,22)]
    [TestCase(true, "Test message 2.",400, "data")]
    public void Builder_WithData_AllPropertiesOfObjectsAreEqual<T>(bool success, string message, int statusCode, T data)
    {
        var serviceResultWithNew = new ServiceResult<T> { Success = success, Message = message, StatusCode = statusCode, Data = data };

        var serviceResultWithBuilder = ServiceResult.Builder(data).Success(success).Message(message).StatusCode(statusCode).Build();

        Assert.AreEqual(serviceResultWithNew.Message, serviceResultWithBuilder.Message);
        Assert.AreEqual(serviceResultWithNew.Success, serviceResultWithBuilder.Success);
        Assert.AreEqual(serviceResultWithNew.StatusCode, serviceResultWithBuilder.StatusCode);
        Assert.AreEqual(serviceResultWithNew.Data, serviceResultWithBuilder.Data);
    }

}