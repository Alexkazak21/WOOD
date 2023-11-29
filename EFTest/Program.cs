namespace EFTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                if (context.Users != null)
                {
                    var users = context.Users.ToList();

                    

                    Console.WriteLine("Список объектов");
                    foreach (var user in users)
                    {
                        Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}");
                    }
                }
                
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age  { get; set; }
    }

    public class User2
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public int wieght { get; set; }
        public int haight { get; set; }
        public int Gender { get; set; }
    }
}