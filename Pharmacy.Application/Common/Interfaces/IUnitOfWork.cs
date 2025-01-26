using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GetRepository<T>()
        where T : BaseEntity;
    Task<int> SaveChangesAsync();
}

public interface IUnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
{
    TContext Context { get; }
}
