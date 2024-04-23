# Signup case

The lambda function can be found in src/signup-case/Function.cs - the lambda is built to be invoked by AWS AppSync with request and response template matching the in/output from the lambda.

### Test

The tests can be executed by changing directory to 

### Deploy through 
```
dotnet lambda deploy-function
```

AWS AppSync managed GraphQL schema with a Lambda function as a resolver.
