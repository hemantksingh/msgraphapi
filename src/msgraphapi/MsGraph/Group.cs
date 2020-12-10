namespace msgraphapi.MsGraph
{
    public class Group
    {
        public string Id { get; }
        public string SecurityIdentifier { get; }
        public string DisplayName { get; }

        public Group(string id, string securityIdentifier, string displayName)
        {
            Id = id;
            SecurityIdentifier = securityIdentifier;
            DisplayName = displayName;
        }
    }
}
