using System;
namespace Demo.SiganlR.Chat
{
  public class ConnectionCollection
  {
    public static List<User> Users { get; set; } = new List<User>();
    public static void AddUser(User user)
    {
      Users.Add(user);
    }

    public static void RemoveUser(User user)
    {
      Users.Remove(user);
    }
  }
}

