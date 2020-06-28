using Ng_MongoDB.BookStore.Books;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Ng_MongoDB.BookStore
{
    public interface IBookAppService : ICrudAppService<
        BookDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateBookDto,
        CreateUpdateBookDto>
    {
    }
}