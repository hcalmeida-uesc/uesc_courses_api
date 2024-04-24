using UescCoursesAPI.Services.Utils;

namespace UescCoursesAPI.Domain;

public class User
{
    public int UserId { get; private set; }
    public string? Login { get; private set; }
    private string _password;
    public string? Password { 
        get => _password;
        private set 
        {
            _password = value ?? throw new ArgumentNullException(nameof(Password));
            _password = Utils.EncryptString(_password);
        }
    }
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
        Create(login, password, rules);
    }

    public User Create(string login, string password, UserRules rules){
        Login = login;
        Password = password;
        Rules = rules;

        return this;
    }
}
