using System.Threading.Tasks;
using AutoGeneratedSystem.DataAccess.Data;
using AutoGeneratedSystem.DataAccess.Models;
using System.Linq;
namespace AutoGeneratedSystem.Services
{
public partial class StoreService
{
private readonly AutogeneratedsystemDatabaseContext _autogeneratedsystemDatabaseContext;
public StoreService(AutogeneratedsystemDatabaseContext autogeneratedsystemDatabaseContext)
{
_autogeneratedsystemDatabaseContext = autogeneratedsystemDatabaseContext;
}
public async Task<Store> CreateStoreAsync(Store entity, CancellationToken cancellationToken)
{
await _autogeneratedsystemDatabaseContext.Store.AddAsync(entity,cancellationToken);
await _autogeneratedsystemDatabaseContext.SaveChangesAsync(cancellationToken);
return entity;
}
public IQueryable<Store> GetAllStore(CancellationToken cancellationToken)
{
return _autogeneratedsystemDatabaseContext.Store;
}
}
}