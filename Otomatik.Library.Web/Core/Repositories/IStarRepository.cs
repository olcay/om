using System.Linq;
using Otomatik.Library.Web.Core.Models;

namespace Otomatik.Library.Web.Core.Repositories
{
    public interface IStarRepository
    {
        ILookup<int, Star> GetStarLookup(string userId);
    }
}
