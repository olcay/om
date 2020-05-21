using System.Collections.Generic;

namespace Otomatik.Library.Web.Core.ViewModels
{
    public class PaginatedViewModel<T>
    {
        public int Total { get; set; }

        public List<T> List { get; set; }
    }
}
