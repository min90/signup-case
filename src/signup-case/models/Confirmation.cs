namespace AWSLambdaResolver.Models;

public class Confirmation
{
    public bool accepted { get; set; }
    public string eventName { get; set; }
    
    public Confirmation() {}
    
    public Confirmation(bool accepted, string eventName)
    {
        this.accepted = accepted;
        this.eventName = eventName;
    }
    
    public override string ToString()
    {
        return $"Confirmation: {accepted} for event: {eventName}";
    }
}