using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MusicalQuiz.Main.Modules.Users.Storage;

namespace MusicalQuiz.Main.Infrastructure.Database
{
    public class DatabaseUpdater : IDatabaseUpdater
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseUpdater(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UpdateDatabase()
        {
            var dbContexts = new List<DbContext>
            {
                (DbContext)_serviceProvider.GetService(typeof(UsersStorage))
            };

            foreach (var context in dbContexts)
            {
                context.Database.Migrate();
            }

            Console.WriteLine("The database has been successfully updated.");
        }
    }
}
