using System.Collections.Generic;
using Newtonsoft.Json;

namespace msgraphapi
{
    public class Users
    {
        public IEnumerable<User> Value;
    }


    public class User
    {
        [JsonProperty("@odata.type")]
        public string type;
        public string id;
        public string displayName;
        public string mail;
        public string userPrincipalName;
    }
}
