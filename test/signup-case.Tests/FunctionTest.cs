using Moq;
using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using System;
using System.Collections;
using AWSLambdaResolver.Models;

namespace signup_case.Tests;

public class FunctionTest
{
    [Fact]
    public async Task TestToUpperFunction()
    {

		var mockHandler = new Mock<IDynamoDBHandler>();
		mockHandler.Setup(m => m.GetAllParticipantsAsync())
               .ReturnsAsync(new List<Participant> { new Participant { id = "testId", name = "testName" } });      

        var function = new Function(mockHandler.Object);
        var context = new TestLambdaContext();
        
        var result = await function.FunctionHandler("{\"RequestType\":\"listParticipants\"}", context);
		Console.WriteLine(result);
		var participants = result as List<Participant>;
		
		Assert.NotNull(participants);
		Assert.Single(participants);
		Assert.Equal("testId", participants[0].id);
		Assert.Equal("testName", participants[0].name);
    }
}
