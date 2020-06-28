using MongoDB.Driver;
using Ng_MongoDB.BookStore.Books;
using Ng_MongoDB.BookStore.Users;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Ng_MongoDB.BookStore.MongoDB
{
    [ConnectionStringName("Default")]
    public class BookStoreMongoDbContext : AbpMongoDbContext
    {
        public IMongoCollection<AppUser> Users => Collection<AppUser>();
        public IMongoCollection<Book> Books => Collection<Book>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.Entity<AppUser>(b =>
            {
                /* Sharing the same "AbpUsers" collection
                 * with the Identity module's IdentityUser class. */
                b.CollectionName = "AbpUsers";
            });

            modelBuilder.Entity<Book>(b =>
            {
                b.CollectionName = "Books";
            });
        }
    }
}