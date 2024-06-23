namespace Visitor_Security_Clearance_System.Common
{
    public class Credentials
    {
        public static readonly string databaseName = Environment.GetEnvironmentVariable("databaseName");
        public static readonly string containerName = Environment.GetEnvironmentVariable("containerName");
        public static readonly string CosmosEndPoint = Environment.GetEnvironmentVariable("cosmosUrl");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");
        public static readonly string VisitorDocumentType = "Visitor";
        public static readonly string SecurityDocumentType = "Security";
        public static readonly string ManagerDocumentType = "Manager";
        public static readonly string OfficeDocumentType = "Office";
        public static readonly string PassDocumentType = "Pass";
    }
}
