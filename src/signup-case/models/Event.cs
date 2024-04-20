using Amazon.DynamoDBv2.DataModel;

namespace AWSLambdaResolver.Models
{

    [DynamoDBTable("liveevents")]
    public class Event
    {
        [DynamoDBHashKey("PK")]
        public string id { get; set; }
        [DynamoDBRangeKey("SK")]
        public string typeTarget { get; set; }
        [DynamoDBProperty("Name")]
        public string name { get; set; }
        [DynamoDBProperty("Classification")]
        public string classification { get; set; }
        [DynamoDBProperty("StartDate")]
        public DateTime startDate { get; set; }
        [DynamoDBProperty("EndDate")]
        public DateTime endDate { get; set; }
        
        public Event() {}
        public Event(string eventId, string typeTarget, string eventName, string classification, DateTime startDate, DateTime endDate)
        {
            this.id = eventId;
            this.typeTarget = typeTarget;
            this.name = eventName;
            this.classification = classification;
            this.startDate = startDate;
            this.endDate = endDate;
        }
        
        public override string ToString()
        {
            return $"Event: {name} with id: {id} and typeTarget: {typeTarget} is a {classification} and starts at: {startDate} and ends at: {endDate}";
        }
        
        public Event builder()
        {
            return this;
        }

        public Event setTypeTarget(string id)
        {
            this.typeTarget = id;
            return this;
        }

        public Event setClassification()
        {
            this.classification = "Event";
            return this;
        }
    }
}