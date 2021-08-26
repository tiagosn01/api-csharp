using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
  public class BaseRepository<Table> : IRepository<Table> where Table : BaseEntity
  {
    protected readonly MyContext _context;
    private DbSet<Table> _dataset;
    public BaseRepository(MyContext context)
    {
      _context = context;
      _dataset = _context.Set<Table>();
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
      try
      {
        var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));

        if (result == null)
        {
          return false;
        }

        _dataset.Remove(result);

        await _context.SaveChangesAsync();
        return true;
      }
      catch (Exception Error)
      {

        throw Error;
      }

    }


    public async Task<bool> ExistAsync(Guid id)
    {
      return await _dataset.AnyAsync(p => p.Id.Equals(id));
    }

    public async Task<Table> InsertAsync(Table item)
    {
      try
      {
        if (item.Id == Guid.Empty)
        {
          item.Id = Guid.NewGuid();
        }

        item.CreatedAt = DateTime.UtcNow;
        _dataset.Add(item);


        await _context.SaveChangesAsync();

      }
      catch (Exception Error)
      {

        throw Error;
      }

      return item;
    }

    public async Task<Table> SelectAsync(Guid id)
    {
      try
      {
        return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
      }
      catch (Exception Error)
      {
        throw Error;
      }
    }

    public async Task<IEnumerable<Table>> SelectAsync()
    {
      try
      {
        return await _dataset.ToListAsync();
      }
      catch (Exception Error)
      {

        throw Error;
      }
    }

    public async Task<Table> UpdateAsync(Table item)
    {
      try
      {
        var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(item.Id));

        if (result == null)
        {
          return null;
        }

        item.UpdatedAt = DateTime.UtcNow;
        item.CreatedAt = result.CreatedAt;

        _context.Entry(result).CurrentValues.SetValues(item);
        await _context.SaveChangesAsync();
      }
      catch (Exception Error)
      {

        throw Error;
      }

      return item;
    }
  }
}
