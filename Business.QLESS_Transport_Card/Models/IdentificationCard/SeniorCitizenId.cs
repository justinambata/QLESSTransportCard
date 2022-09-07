using System.Text.RegularExpressions;

namespace Business.QLESS_Transport_Card.Models.IdentificationCard
{
    public class SeniorCitizenId : IIdentificationCard
    {
        public SeniorCitizenId(string id)
        {
            Id = id;
        }
        
        public string Id { get; set; }
        public bool IsIdValid => Regex.Match(Id, RegexPattern).Success;

        private const string RegexPattern = "[a-zA-Z0-9]{2}-[a-zA-Z0-9]{4}-[a-zA-Z0-9]{4}";
    }
}
