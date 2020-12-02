namespace msgraphapi.MsGraph
{
    public class Domain
    {
        public readonly string AuthenticationType;
        public readonly string Id;
        public Domain(string id, string authenticationType)
        {
            Id = id;
            AuthenticationType = authenticationType;
        }
    }
}
