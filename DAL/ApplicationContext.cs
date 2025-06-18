using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WorkOutWebHookBot.DAL.Entities;

namespace WorkOutWebHookBot.DAL
{
    public class ApplicationContext : DbContext
    {

        public DbSet<BotUser> BotUsers { get; set; }
        public DbSet<Partner> Partners { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=141.8.194.74,3306; User=a0400904_WorkOutBot;Password=Lm9jhnFm;Database=a0400904_WorkOutBot;";
            ServerVersion ver = ServerVersion.AutoDetect(connectionString);
            //"Имя: a0400904_WorkOutBot\r\nПользователь: a0400904_WorkOutBot\r\nПароль: Lm9jhnFm\r\nАдрес хоста: localhost"
            optionsBuilder.UseMySql(connectionString, ver);
        }

    }
}
