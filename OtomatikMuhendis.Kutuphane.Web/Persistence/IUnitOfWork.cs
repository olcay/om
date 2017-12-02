using OtomatikMuhendis.Kutuphane.Web.Repositories;

namespace OtomatikMuhendis.Kutuphane.Web.Persistence
{
    public interface IUnitOfWork
    {
        FollowingRepository Followings { get; }

        ShelfRepository Shelves { get; }

        void Complete();
    }
}