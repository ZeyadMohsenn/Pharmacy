using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities;
namespace Pharmacy.Infrastructure.Context;

public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
    where TContext : DbContext
{
    private readonly TContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(TContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        entity.Is_Deleted = true;
    }

    public Task AddAsync(TEntity entity)
    {
        return Task.FromResult(_dbSet.AddAsync(entity));
    }

    public Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        return _dbSet.AddRangeAsync(entities);
    }

    public Task<TEntity?> FindAsync(
    Expression<Func<TEntity, bool>> filterPredicate,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? Include = null,
    Expression<Func<TEntity, TEntity>>? select = null,
    bool asNoTracking = false,
    bool asSplit = false,
    bool IgnoreFilter = false,
    bool withDeleted = false
)
    {
        var query = _dbSet.AsQueryable();
        query = query.Where(filterPredicate);

        if (!withDeleted)
            query = query.Where(a => !a.Is_Deleted);
        if (IgnoreFilter)
            query = query.IgnoreQueryFilters();
        if (Include is not null)
            query = Include(query);
        if (asNoTracking)
            query = query.AsNoTracking();
        if (select is not null)
            query = query.Select(select);
        if (asSplit)
            query = query.AsSplitQuery();
        if (orderBy is not null)
            query = orderBy(query);

        return query.FirstOrDefaultAsync();
    }

    public Task<TResult?> FindAsync<TResult>(
        Expression<Func<TEntity, bool>> filterPredicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? Include = null,
        bool asNoTracking = false,
        Expression<Func<TEntity, TResult>>? select = null,
        bool asSplit = false,
        bool IgnoreFilter = false,
        bool withDeleted = false
    )
    {
        var query = _dbSet.AsQueryable();
        if (!withDeleted)
            query = query.Where(a => !a.Is_Deleted);

        query = query.Where(filterPredicate);

        if (IgnoreFilter)
            query = query.IgnoreQueryFilters();
        if (Include is not null)
            query = Include(query);
        if (asNoTracking)
            query = query.AsNoTracking();
        if (asSplit)
            query = query.AsSplitQuery();
        if (select is not null)
            return query.Select(select).FirstOrDefaultAsync()!;

        return query.Cast<TResult>().FirstOrDefaultAsync()!;
    }

    public Task<List<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filterPredicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? Include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, TEntity>>? select = null,
        int? take = null,
        bool ignoreFilter = false,
        bool asSplit = false
    )
    {
        var query = _dbSet.AsQueryable();
        query = query.Where(a => !a.Is_Deleted).Where(filterPredicate);

        if (ignoreFilter)
            query = query.IgnoreQueryFilters();
        if (Include is not null)
            query = Include(query);
        if (orderBy is not null)
            query = orderBy(query);
        if (take is not null)
            query = query.Take(take.Value);
        if (select is not null)
            query = query.Select(select);
        if (asSplit)
            query = query.AsSplitQuery();

        return query.AsNoTrackingWithIdentityResolution().ToListAsync();
    }

    public Task<List<TResult>> GetAllAsync<TResult>(
        Expression<Func<TEntity, bool>> filterPredicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? Include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Expression<Func<TEntity, TResult>>? select = null,
        int? take = null,
        bool ignoreFilter = false,
        bool asSplit = false
    )
    {
        var query = _dbSet.AsQueryable();
        query = query.Where(a => !a.Is_Deleted).Where(filterPredicate);

        if (ignoreFilter)
            query = query.IgnoreQueryFilters();
        if (Include is not null)
            query = Include(query);
        if (orderBy is not null)
            query = orderBy(query);
        if (take is not null)
            query = query.Take(take.Value);
        if (asSplit)
            query = query.AsSplitQuery();
        if (select is not null)
            return query.Select(select).ToListAsync();

        return query.AsNoTrackingWithIdentityResolution().Cast<TResult>().ToListAsync();
    }

    public Task<List<TEntity>> GetAllAsync(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null
    )
    {
        var query = _dbSet.AsQueryable();

        if (orderBy is not null)
            query = orderBy(query);

        return query.Where(a => !a.Is_Deleted).ToListAsync();
    }

    public async Task<IQueryable<TEntity>> GetAllQueryableAsync(
        Expression<Func<TEntity, bool>> filterPredicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? Include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool withDeleted = false,
        bool asSplit = false,
        bool asNoTracking = false
    )
    {
        var query = _dbSet.AsQueryable();

        if (Include is not null)
            query = Include(query);
        if (orderBy is not null)
            query = orderBy(query);
        if (asSplit)
            query = query.AsSplitQuery();
        if (asNoTracking)
            query = query.AsNoTracking();
        if (withDeleted)
            return await Task.FromResult(query.Where(filterPredicate));

        return await Task.FromResult(query.Where(a => !a.Is_Deleted).Where(filterPredicate));
    }

    public async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>>? filterPredicate = null)
    {
        var query = _dbSet.Where(a => !a.Is_Deleted);

        if (filterPredicate is not null)
            return await  query.AsNoTracking().AnyAsync(filterPredicate);

        return await query.AsNoTracking().AnyAsync();
    }

    public async Task<bool> IsExistsAsync(IEnumerable<Guid> ids)
    {
        if (!ids.Any())
            return true;

        var query = _dbSet.Where(a => !a.Is_Deleted);

        var existingIds = await query
            .AsNoTracking()
            .Select(x => x.Id)
            .Where(id => ids.Contains(id))
            .ToListAsync();

        return existingIds.Count == ids.Count();
    }

    public async Task<(List<TEntity>, int)> GetPaginatedAsync(
        Expression<Func<TEntity, bool>>? filterPredicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? Include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Pagination? pagination = null,
        Expression<Func<TEntity, TEntity>>? select = null
    )
    {
        int count = 0;
        var query = _dbSet.AsNoTracking().AsQueryable();
        query = query.Where(a => !a.Is_Deleted);

        if (Include is not null)
            query = Include(query);
        if (orderBy is not null)
            query = orderBy(query);
        if (filterPredicate != null)
            query = query.Where(filterPredicate);
        if (select is not null)
            query = query.Select(select);
        if (pagination is not null)
        {
            count = await query.CountAsync();
            query = query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize);
        }
        return (await query.ToListAsync(), count);
    }

    public IQueryable<TEntity> GetPaginatedQuerableAsync(
        Expression<Func<TEntity, bool>>? filterPredicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? Include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Pagination? pagination = null,
        Expression<Func<TEntity, TEntity>>? select = null,
        bool withDeleted = false
    )
    {
        var query = _dbSet.AsQueryable();

        if (!withDeleted)
            query = query.Where(a => !a.Is_Deleted);
        if (Include is not null)
            query = Include(query);
        if (orderBy is not null)
            query = orderBy(query);
        if (filterPredicate != null)
            query = query.Where(filterPredicate);
        if (select is not null)
            query = query.Select(select);
        if (pagination is not null)
            query = query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize);

        return query;
    }

    public Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? filterPredicate = null,
        bool withDeleted = false
    )
    {
        var query = _dbSet.AsQueryable().AsNoTracking();

        if (!withDeleted)
            query = query.Where(a => !a.Is_Deleted);
        if (filterPredicate is not null)
            query = query.Where(filterPredicate);

        return query.CountAsync();
    }

    public Task<decimal> SumAsync(
        Expression<Func<TEntity?, decimal>> sum,
        Expression<Func<TEntity?, bool>>? filter = null
    )
    {
        var query = _dbSet.AsNoTracking().AsQueryable().Cast<TEntity?>();

        if (filter is not null)
            query = query.Where(filter);

        return query.DefaultIfEmpty().SumAsync(sum);
    }

    public Task<int> SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}

