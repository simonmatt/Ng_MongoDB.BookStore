using System.Threading.Tasks;

namespace Ng_MongoDB.BookStore.Data
{
    public interface IBookStoreDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
