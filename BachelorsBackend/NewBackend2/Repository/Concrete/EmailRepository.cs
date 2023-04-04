using Microsoft.EntityFrameworkCore;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Concrete
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ProjectDatabaseConfiguration database;

        public EmailRepository(ProjectDatabaseConfiguration database)
        {
            this.database = database;
        }

        public async Task AddEmailAsync(EmailEntity email)
        {
            database.emails.Add(email);
            await database.SaveChangesAsync();
        }
    }
}
