using Ng_MongoDB.BookStore.MongoDB;
using Volo.Abp.Modularity;

namespace Ng_MongoDB.BookStore
{
    [DependsOn(
        typeof(BookStoreMongoDbTestModule)
        )]
    public class BookStoreDomainTestModule : AbpModule
    {

    }
}