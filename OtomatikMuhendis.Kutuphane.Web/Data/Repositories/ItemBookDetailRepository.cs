using System.Linq;
using Microsoft.EntityFrameworkCore;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Core.Repositories;

namespace OtomatikMuhendis.Kutuphane.Web.Data.Repositories
{
    public class ItemBookDetailRepository : IItemBookDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemBookDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save(ItemBookDetail itemBookDetail)
        {
            _context.ItemBookDetails.Add(itemBookDetail);
        }

        public BookDetail GetBookDetailByItemId(int itemId)
        {
            var itemBookDetail = _context.ItemBookDetails
                .Include(b => b.BookDetail)
                .Include(b => b.BookDetail.BookAuthorList)
                .SingleOrDefault(b => b.ItemId == itemId);

            return itemBookDetail?.BookDetail;
        }

        public Item GetItemByBookDetailId(int bookDetailId, string userId)
        {
            var itemBookDetail = _context.ItemBookDetails
                .Include(b => b.Item)
                .SingleOrDefault(b => b.BookDetailId == bookDetailId && b.Item.CreatedById == userId);

            return itemBookDetail?.Item;
        }
    }
}
