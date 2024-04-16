namespace UescCoursesAPI.Domain;

public class User
{
    public int UserId { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required string Rules { get; set; }
}
