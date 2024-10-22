using Ecommerce.Domain.Models;

namespace Ecommerce.Repositories;

public static class UserRepository
{
    private static readonly List<User> Users =
    [
        new User()
        {
            Username = "youssefhm",
            Email = "youssefhammam77@gmail.com",
            Password = "1234",
            Address = "5 St Ismail Basha - El Sayeda Zeinab"
        },
        new User()
        {
            Username = "admin",
            Email = "admin@admin.com",
            Password = "1234",
            Address = "5st Alexandria - Egypt"
        }
    ];

    public static User? currentUser { get; set; }

    public static User Create(User user)
    {
        bool isExistingUsername = FindByUsername(user.Username) is not null;

        bool isExistingEmail = FindByEmail(user.Email) is not null;

        if (isExistingUsername)
            throw new ArgumentException("username already exists!");

        if (isExistingEmail)
            throw new ArgumentException("email already exists!");

        user.Id = Users.Count + 1;

        Users.Add(user);

        return user;
    }

    public static User? Remove(long id)
    {
        User? user = Users.Find(user => user.Id == id);

        if (user is null) return null;

        Users.Remove(user);

        return user;
    }

    public static User? FindById(long id)
    {
        return Users.FirstOrDefault(user => user.Id == id);
    }

    public static User? FindByUsername(string username)
    {
        return Users.FirstOrDefault(user => user.Username == username);
    }

    public static User? FindByEmail(string email)
    {
        return Users.FirstOrDefault(user => user.Email == email);
    }

    public static List<User> GetAll()
    {
        return Users;
    }

}
