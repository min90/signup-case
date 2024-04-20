using Amazon.DynamoDBv2.DataModel;

namespace AWSLambdaResolver.Models
{
	[DynamoDBTable("liveevents")]
    public class Participant
    {
		[DynamoDBHashKey("PK")]
        public string id { get; set; }
		[DynamoDBRangeKey("SK")]
        public string typeTarget { get; set; }
		[DynamoDBProperty("Name")]
		public string name { get; set; }
		[DynamoDBProperty("Classification")]
		public string classification { get; set; }
		[DynamoDBProperty("Consent")]
		public bool consent { get; set; }
		[DynamoDBProperty("Child")]
		public bool child { get; set; }
		[DynamoDBProperty("Email")]
		public string email { get; set;}

		public Participant() {}

        public Participant(string participantId, string typeTarget, string participantName, string classification,
		    bool consent, bool child, string email)
        {
            this.id = participantId;
            this.typeTarget = typeTarget;
            this.name = participantName;
            this.classification = classification;
            this.consent = consent;
            this.child = child;
            this.email = email;
        }

		public Participant(string participantId, string typetarget) 
	    {
            this.id = participantId;
            this.typeTarget = typetarget;
        }

		public override string ToString() 
		{
			return $"Participant: {name} with id: {id} and typeTarget: {typeTarget} is a {classification} with email {email} and has given consent: {consent} and is a child: {child}";
    	}

        public Participant builder()
        {
            return this;
        }

        public Participant setTypeTarget(string id)
        {
            this.typeTarget = id;
            return this;
        }
	}   
}