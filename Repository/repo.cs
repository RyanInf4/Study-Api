public class Utilities
{
    public UserDTO ConvertUserDto (User user)
    {      
        return new UserDTO()
        {
            Username = user.Username,
            Email = user.Email,
            UserId = user.UserId,
        };
    }
}