using Amazon.Lambda.Core;
using System.Text.Json;
using AWSLambdaResolver.Models;
using System;
using System.Text;
using System.Collections;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace signup_case;

public class Function
{
    private readonly IDynamoDBHandler _handler;

    public Function() : this(new DynamoDBHandler()) { }

    public Function(IDynamoDBHandler handler)
    {
        _handler = handler;
    }
    public async Task<object> FunctionHandler(object input, ILambdaContext context)
    {
        //Serialize the input to the LambdaInput class
        LambdaInput lambdaInput = JsonSerializer.Deserialize<LambdaInput>(input.ToString());
        
        Console.WriteLine($"Input context: {context}");
        Console.WriteLine($"Input received {input}");
        Console.WriteLine($"RequestType: {lambdaInput.RequestType}");
        
        switch (lambdaInput.RequestType)
        {
            case "listParticipants":
                //Trading simplicity for performance - No unbounded resultsets in production
                List<Participant> participants = await _handler.GetAllParticipantsAsync();
                Console.WriteLine($"Got the following Participants: {participants}");
                return participants;
            case "participant":
                if (lambdaInput.participant == null)
                {
                    Console.WriteLine("No participant provided");
                    return null;
                }
                Participant participant = await _handler.GetParticipantAsync(lambdaInput.participant.id);
                return participant;
            case "event":
                if (lambdaInput.eventInfo == null)
                {
                    Console.WriteLine("No event provided");
                    return null;
                }
                Event eventRetrived = await _handler.GetEventAsync(lambdaInput.eventInfo.id);
                return eventRetrived;
            case "addEvent":
                if (lambdaInput.eventInfo == null)
                {
                    Console.WriteLine("No event info provided cannot save event");
                    return null;
                }
                lambdaInput.eventInfo.builder().setClassification().setTypeTarget(lambdaInput.eventInfo.id);
                Console.WriteLine($"Saving the event {lambdaInput.eventInfo}");
                Event eventSaved = await _handler.SaveEventAsync(lambdaInput.eventInfo);
                return eventSaved;
            case "signup":
                SignUpInfo signUpInfo = lambdaInput.signUpInfo;
                Console.WriteLine($"Signupinfo received: {signUpInfo}");
                string token = signUpInfo.token;
                Console.WriteLine($"Got the following token: {token}");
                
                Participant decodedParticipant = DecodeToken(token);
                Console.WriteLine($"Decoded participant: {decodedParticipant}");
                
                Participant retrievedParticpant = await _handler.GetParticipantAsync(decodedParticipant.id);
                if (retrievedParticpant == null)
                {
                    Console.WriteLine("No participant found - creating one");
                    
                    //Setting type target to match the database schema
                    decodedParticipant.builder().setTypeTarget(decodedParticipant.id);
                    //Logging here is a violation of GDPR
                    Console.WriteLine($"Saving the participant {decodedParticipant}");
                    retrievedParticpant = await _handler.SaveParticipantAsync(decodedParticipant);
                }
                Event retrievedEvent = await _handler.GetEventAsync(signUpInfo.eventInfo.id);
                if (retrievedEvent == null)
                {
                    Console.WriteLine("No event found - cannot sign up");
                    return null;
                }
                //Signup
                bool signedUp = await _handler.SignUpAsync(retrievedParticpant, retrievedEvent);
                return new Confirmation(signedUp, retrievedEvent.name);
            default:
                return null;
        }
    }
    
    private Participant DecodeToken(string token)
    {
        byte[] data = Convert.FromBase64String(token);
        string decodedString = Encoding.UTF8.GetString(data);
        Console.WriteLine($"Decoded token: {decodedString}");
        return JsonSerializer.Deserialize<Participant>(decodedString);
    }
}
