using Google.Apis.Books.v1.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OtomatikMuhendis.Kutuphane.Web.Services
{
    public interface IBookFinder
    {
        Task<IList<Volume>> Search(string searchTerm);

        Task<Volume> GetAsync(string id);
    }
}
