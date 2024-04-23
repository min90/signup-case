# Signup case

The lambda function can be found in src/signup-case/Function.cs - the lambda is built to be invoked by AWS AppSync with request and response template matching the in/output from the lambda.

The input to the lambda function is as the following example

```
{
  "version" : "2017-02-28",
  "operation": "Invoke",
  "payload": {
    "RequestType": "<name of query/mutation e.g. createEvent>",
    "<object being worked on e.g event>": $util.toJson($context.args) <-- The GraphQL argument
  }
}
```

To execute code, test and deploy the lambda function - the dotnet cli with .Net Runtime and SDK needs to be installed, and the cli has to be on the path - See https://learn.microsoft.com/en-us/dotnet/core/install/macos

### Test

The tests can be executed by changing directory to test/signup-case.Tests

Run the following command

```
dotnet test
```

### Deploy through 
```
dotnet lambda deploy-function
```

AWS AppSync managed GraphQL schema with a Lambda function as a resolver.
