namespace EmployeeDetails.Data
{
    public class ApiToken
    {
        public static Dictionary<string, string> Employee { get; set; } = new Dictionary<string, string>
        {
            {"SampleEmployee", "Password" },
            {"DemoEmployee", "Password" }
     };
    }
}
