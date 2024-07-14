namespace employeeManagementSystem.Common
{
    public class Credentials
    {
        public static readonly string DatabaseName = Environment.GetEnvironmentVariable("databaseName");
        public static readonly string ContainerName = Environment.GetEnvironmentVariable("containerName");
        public static readonly string CosmosEndPoint = Environment.GetEnvironmentVariable("cosmosUrl");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");

        internal static readonly string EmployeeUrl = Environment.GetEnvironmentVariable("employeeUrl");
        internal static readonly string AddEmployeeEndpoint = "/api/EmployeeBasicDetail/AddEmployeeBasicDetail";
        internal static readonly string GetEmployeeEndpoint = "/api/EmployeeBasicDetail/GetAllEmployee";
    }
}
