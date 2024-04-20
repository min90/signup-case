namespace signup_case;
using AWSLambdaResolver.Models;

public interface IDynamoDBHandler
{
    Task<Participant> GetParticipantAsync(string id);
    Task<List<Participant>> GetAllParticipantsAsync();
    Task<Event> GetEventAsync(string id);
    Task<Event> SaveEventAsync(Event @event);
    Task<Participant> SaveParticipantAsync(Participant participant);
    Task<bool> SignUpAsync(Participant participant, Event @event);
}