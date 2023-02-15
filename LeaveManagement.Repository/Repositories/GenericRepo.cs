using Microsoft.EntityFrameworkCore;
using LeaveManagement.Data;
using LeaveManagement.Repository.Interfaces;

namespace LeaveManagement.Repository.Repositories;

/* SETUP ASYNCHRONOUS DATABASE REPOSITORY PATTERN AND DEPENDENCY INJECTION
------------------------------------------------------------------------------*/

public class GenericRepo<T> : IGenericRepo<T> where T : class
{
    public readonly ApplicationDbContext context;
    public GenericRepo(ApplicationDbContext context) => this.context = context;

    public async Task<T> AddAsync(T entity)
    {
        await context.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task AddRangeAsync(List<T> entities)
    {
        await context.AddRangeAsync(entities);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            context.Set<T>().Remove(entity);
        }

        await context.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await context.Set<T>().ToListAsync(); 
    }

    public async Task<T?> GetAsync(int? id)
    {
        if(id == null)
        {
            return null;
        }

        return await context.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        /*context.Update(entity);*/
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
}
