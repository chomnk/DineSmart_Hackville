namespace DineSmartWebAPI.Models
{
    public class RestaurantDatabaseSettings
    {
        public string ConnectionString { get; set; } = "mongodb+srv://davidcaluag:vgDHJCRMYYN2ed9s@cluster0.hjzavom.mongodb.net/?retryWrites=true&w=majority";

        public string DatabaseName { get; set; } = "RestaurantDatabase";

        public string RestaurantsCollectionName { get; set; } = "_restaurant";
        public string UsersCollectionName { get; set; } = "_user";
    }
}
