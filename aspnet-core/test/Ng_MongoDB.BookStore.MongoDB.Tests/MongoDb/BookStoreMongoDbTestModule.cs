﻿using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace Ng_MongoDB.BookStore.MongoDB
{
    [DependsOn(
        typeof(BookStoreTestBaseModule),
        typeof(BookStoreMongoDbModule)
        )]
    public class BookStoreMongoDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var connectionString = MongoDbFixture.ConnectionString.EnsureEndsWith('/') +
                                    "Db_" +		
                                    Guid.NewGuid().ToString("N");

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });
        }
    }
}