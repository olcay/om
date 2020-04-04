using Otomatik.Library.Web.Core.Repositories;

namespace Otomatik.Library.Web.Core
{
    public interface IUnitOfWork
    {
        IFollowingRepository Followings { get; }
        IShelfRepository Shelves { get; }
        IItemRepository Items { get; }
        IBookDetailRepository BookDetails { get; }
        IBookAuthorRepository BookAuthors { get; }
        IAuthorRepository Authors { get; }
        IItemBookDetailRepository ItemBookDetails { get; }
        IUserRepository Users { get; }
        IStarRepository Stars { get; }

        void Complete();
    }
}