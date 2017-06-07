using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

    }

    public class SiteContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SiteContext())
            {
                context.Users.Add(new User()
                {
                    UserName = "user1",
                    Password = "user1"
                });
                context.SaveChanges();
            }
        }
    }
}
