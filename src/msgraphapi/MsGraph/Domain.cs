namespace msgraphapi.MsGraph
{
    public class Domain
    {
        public readonly string authenticationType;
        public readonly string id;
        public readonly bool? isAdminManaged;
        public readonly bool? isDefault;
        public readonly bool? isRoot;
        public readonly bool? isInitial;
        public readonly bool? isVerified;

        public Domain(string id, string authenticationType, bool? isAdminManaged, bool? isDefault,
            bool? isInitial, bool? isRoot,
            bool? isVerified)
        {
            this.id = id;
            this.authenticationType = authenticationType;
            this.isAdminManaged = isAdminManaged;
            this.isDefault = isDefault;
            this.isRoot = isRoot;
            this.isInitial = isInitial;
            this.isVerified = isVerified;
        }

        public bool IsDefaultDomain()
        {
            return isDefault ?? isInitial ?? isRoot ?? authenticationType.Equals("Managed");
        }
    }
}
