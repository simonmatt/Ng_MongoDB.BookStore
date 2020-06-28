using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Xunit;

namespace Ng_MongoDB.BookStore.Books
{
    public class BookAppService_Tests : BookStoreApplicationTestBase
    {
        private readonly IBookAppService _bookAppService;

        public BookAppService_Tests()
        {
            _bookAppService = GetRequiredService<IBookAppService>();
        }

        [Fact]
        public async Task Should_Get_List_Of_Books()
        {
            // Act
            var result = await _bookAppService.GetListAsync(new PagedAndSortedResultRequestDto());

            // Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(b => b.Name == "1984");
        }

        [Fact]
        public async Task Should_Create_A_Valid_Book()
        {
            // Act
            var result = await _bookAppService.CreateAsync(new CreateUpdateBookDto
            {
                Name = "Deep into C#",
                Price = 30.09f,
                PublishDate = new DateTime(2009, 8, 3),
                Type = BookType.ScienceFiction
            });

            // Assert
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("Deep into C#");
        }

        [Fact]
        public async Task Should_Not_Create_A_Book_Without_Name()
        {
            // Act
            var exception = await Assert.ThrowsAsync<Volo.Abp.Validation.AbpValidationException>(async () =>
            {
                await _bookAppService.CreateAsync(new CreateUpdateBookDto
                {
                    Name = "",
                    Price = 30.09f,
                    PublishDate = new DateTime(2009, 8, 3),
                    Type = BookType.ScienceFiction
                });
            });

            exception.ValidationErrors.ShouldContain(err => err.MemberNames.Any(mem => mem == "Name"));
        }
    }
}