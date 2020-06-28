using System;
using System.Collections.Generic;
using System.Text;
using Ng_MongoDB.BookStore.Localization;
using Volo.Abp.Application.Services;

namespace Ng_MongoDB.BookStore
{
    /* Inherit your application services from this class.
     */
    public abstract class BookStoreAppService : ApplicationService
    {
        protected BookStoreAppService()
        {
            LocalizationResource = typeof(BookStoreResource);
        }
    }
}
