namespace DineSmartWebAPI.Models
{
    public class UserDatabaseSettings
    {
        public string ConnectionString { get; set; } = "mongodb+srv://davidcaluag:vgDHJCRMYYN2ed9s@cluster0.hjzavom.mongodb.net/?retryWrites=true&w=majority";

        public string DatabaseName { get; set; } = "UserDatabase";

        public string UsersCollectionName { get; set; } = "_user";
    }
}
