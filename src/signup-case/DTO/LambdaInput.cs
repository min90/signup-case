using AWSLambdaResolver.Models;

namespace signup_case;

public class LambdaInput
{
    public string RequestType { get; set; }
    public Participant participant { get; set; }
    public Event eventInfo { get; set; }
    public SignUpInfo signUpInfo { get; set; }
}