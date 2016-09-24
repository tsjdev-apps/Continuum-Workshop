namespace RandomUser.Portable.Model
{
    public class User
    {
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public string PictureUrl { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}