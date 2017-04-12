namespace Server.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Passwordhash { get; set; }
        public string Passwordsalt { get; set; }
        public string Role { get; set; }
    }
}
