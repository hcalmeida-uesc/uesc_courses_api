using UescCoursesAPI.Domain;

namespace UescCoursesAPI.Services.DTO;
public record UserPostDTO(string Login, string Password, UserRules Rules);

public record UserAuthDTO(string? Login, string Token);
