using DeliveryApp.API.DataLayers;
using DeliveryApp.API.Repository;
using Microsoft.EntityFrameworkCore;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DataContext _context;

    public Repository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetById(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Update(T entity, int id)
    {
        var existingEntity = await _context.Set<T>().FindAsync(id); // Assuming the entity has an Id property
        if (existingEntity == null)
        {
            throw new Exception("Entity not found");
        }

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<T> Delete(int id)
    {
        var entity = await GetById(id);
        if (entity == null)
        {
            throw new Exception("The entity dosent exist");
        }

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}