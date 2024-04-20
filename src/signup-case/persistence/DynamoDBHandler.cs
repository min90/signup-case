using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using AWSLambdaResolver.Models;

namespace signup_case;

public class DynamoDBHandler : IDynamoDBHandler
{
    
    private DynamoDBContext dbContext = new DynamoDBContext(new AmazonDynamoDBClient());
    
    public DynamoDBHandler() {}
    
    public async Task<Participant> GetParticipantAsync(string id)
    {
        Console.WriteLine($"Looking for participant with id {id}");

        Participant participant = await dbContext.LoadAsync<Participant>(id, id);
        Console.WriteLine($"Got result {participant}");

        return participant;
    }
    
    public async Task<List<Participant>> GetAllParticipantsAsync()
    {
        var keyCondition = new Expression
        {
            ExpressionStatement = "#classification = :classificationVal",
            ExpressionAttributeNames = { { "#classification", "Classification" } },
            ExpressionAttributeValues = { { ":classificationVal", new Primitive("Participant") } }
        };
        
        string indexName = "Classification-PK-index";
        
        var config = new QueryOperationConfig
        {
            IndexName = indexName,
            KeyExpression = keyCondition
        };

        var search = dbContext.FromQueryAsync<Participant>(config);
        List<Participant> results = await search.GetNextSetAsync();
        return results;
    }
    
    public async Task<Event> GetEventAsync(string id)
    {
        Console.WriteLine($"Looking for event with id {id}");

        Event @event = await dbContext.LoadAsync<Event>(id, id);
        Console.WriteLine($"Got result {@event}");

        return @event;
    }
    
    public async Task<Event> SaveEventAsync(Event @event)
    {
        Console.WriteLine($"Saving event {@event}");

        await dbContext.SaveAsync(@event);
        return @event;
    }
    
    public async Task<Participant> SaveParticipantAsync(Participant participant)
    {
        Console.WriteLine($"Saving participant {participant}");

        await dbContext.SaveAsync(participant);
        return participant;
    }
    
    public async Task<bool> SignUpAsync(Participant participant, Event @event)
    {
        Console.WriteLine($"Signing up participant {participant} for event {@event}");

        await dbContext.SaveAsync(new ParticipantEdge(@event.id, $"participant#{participant.id}"));
        return true;
    }
}