using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Entities;


namespace Api.Domain.Interfaces
{
  public interface IRepository<Table> where Table : BaseEntity
  {
    Task<Table> InsertAsync(Table item);
    Task<Table> UpdateAsync(Table item);
    Task<bool> DeleteAsync(Guid id);
    Task<Table> SelectAsync(Guid id);
    Task<IEnumerable<Table>> SelectAsync();
    Task<bool> ExistAsync(Guid id);
  }
}
