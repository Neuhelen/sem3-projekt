namespace Semester_3_Projekt.Models
{
    public enum UserRole
    {
        Admin
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; }
        public string PasswordHash { get; set; }
    }
}
