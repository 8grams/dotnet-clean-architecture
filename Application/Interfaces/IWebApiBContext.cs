using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using StoredProcedureEFCore;

namespace WebApi.Application.Interfaces
{
    public interface IWebApiDbContext
    {
        DbSet<User> Users { set; get; }
        IStoredProcBuilder loadStoredProcedureBuilder(string val);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
