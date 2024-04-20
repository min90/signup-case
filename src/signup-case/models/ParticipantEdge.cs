using Amazon.DynamoDBv2.DataModel;

namespace AWSLambdaResolver.Models;

[DynamoDBTable("liveevents")]
public class ParticipantEdge
{
    public string PK { get; set; }
    public string SK { get; set; }
    
    public ParticipantEdge() {}
    
    public ParticipantEdge(string PK, string SK)
    {
        this.PK = PK;
        this.SK = SK;
    }
    
    public override string ToString()
    {
        return $"ParticipantEdge: PK: {PK} and SK: {SK}";
    }
}