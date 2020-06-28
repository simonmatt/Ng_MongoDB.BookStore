using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Ng_MongoDB.BookStore.Books;
using Ng_MongoDB.BookStore.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace Ng_MongoDB.BookStore.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(BookStoreMongoDbModule),
        typeof(BookStoreApplicationContractsModule)
        )]
    public class BookStoreDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);

            // Remove the contributor for migrator module
            context.Services.RemoveAll(t => t.ImplementationType == typeof(IDataSeedContributor));

            // Add custom data seed contributor
            context.Services.AddTransient<IDataSeedContributor, BookStoreDataSeederContributor>();
        }
    }
}
