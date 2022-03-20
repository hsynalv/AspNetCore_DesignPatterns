using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Decorator.Models;

namespace BaseProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context =scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
                context.Database.Migrate();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                if (!userManager.Users.Any())
                {
                    userManager.CreateAsync(new AppUser{UserName = "user1",Email = "user1@gmail.com",},"Password12*").Wait();
                    userManager.CreateAsync(new AppUser{UserName = "user2",Email = "user2@gmail.com",},"Password12*").Wait();
                    userManager.CreateAsync(new AppUser{UserName = "user3",Email = "user3@gmail.com",},"Password12*").Wait();
                    userManager.CreateAsync(new AppUser{UserName = "user4",Email = "user4@gmail.com",},"Password12*").Wait();
                    userManager.CreateAsync(new AppUser{UserName = "user5",Email = "user5@gmail.com",},"Password12*").Wait();
                }
            }

                host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
