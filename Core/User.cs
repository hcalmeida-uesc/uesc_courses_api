namespace UescCoursesAPI.Domain;

public class User
{
    public int UserId { get; private set; }
    public string? Login { get; private set; }
    public string? Password { get; private set; }
    public UserRules Rules { get; private set; }

    public User(){
        Rules = UserRules.Public;
    }

    public User(string login, string password, UserRules rules)
    {
        Create(login, password, rules);
    }
    public User(int id, string login, string password, UserRules rules): this(login, password, rules)
    {
        UserId = id;
    }

    public void Update(string login, string password, UserRules rules)
    {
        Login = login;
        Password = password;
        Rules = rules;
    }

    public User Create(string login, string password, UserRules rules){
        Login = login;
        Password = password;
        Rules = rules;

        return this;
    }
}
