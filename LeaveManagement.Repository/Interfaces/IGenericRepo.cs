using LeaveManagement.Repository;

namespace LeaveManagement.Repository.Interfaces;

/* SETUP ASYNCHRONOUS DATABASE INTERFACE
------------------------------------------------------------------------------*/
public interface IGenericRepo<T> where T : class
{
    Task<T?> GetAsync(int? id);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(List<T> entity);
    Task<bool> Exists(int id);
    Task DeleteAsync(int id);
    Task UpdateAsync(T entity);
}
