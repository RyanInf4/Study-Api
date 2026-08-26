public class Utilities
{
    public static UserDTO ConvertUserDto (User user)
    {      
        return new UserDTO()
        {
            Username = user.Username,
            Email = user.Email,
            UserId = user.UserId,
        };
    }
}