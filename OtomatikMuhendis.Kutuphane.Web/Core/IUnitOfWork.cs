﻿using OtomatikMuhendis.Kutuphane.Web.Core.Repositories;

namespace OtomatikMuhendis.Kutuphane.Web.Core
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

        void Complete();
    }
}