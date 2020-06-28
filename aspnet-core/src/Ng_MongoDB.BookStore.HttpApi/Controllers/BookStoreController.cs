using Ng_MongoDB.BookStore.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Ng_MongoDB.BookStore.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class BookStoreController : AbpController
    {
        protected BookStoreController()
        {
            LocalizationResource = typeof(BookStoreResource);
        }
    }
}