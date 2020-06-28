using Ng_MongoDB.BookStore.Books;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Ng_MongoDB.BookStore
{
    public class BookAppService : CrudAppService<Book, BookDto, Guid,
        PagedAndSortedResultRequestDto, CreateUpdateBookDto, CreateUpdateBookDto>, IBookAppService
    {
        public BookAppService(IRepository<Book, Guid> repoistory) : base(repoistory)
        {
        }
    }
}