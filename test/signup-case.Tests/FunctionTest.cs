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
    public void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        
        var upperCase = function.FunctionHandler("hello world", context);
        ArrayList participants = new ArrayList();
        participants.Add(new Participant("1", "Jesper"));

        upperCase.Equals(participants);
    }
}
