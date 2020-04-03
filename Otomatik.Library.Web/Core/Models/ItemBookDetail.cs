using System;

namespace Otomatik.Library.Web.Core.Models
{
    public class ItemBookDetail
    {
        public Item Item { get; set; }

        public BookDetail BookDetail { get; set; }

        public int ItemId { get; set; }

        public int BookDetailId { get; set; }

        protected ItemBookDetail()
        {
        }

        public ItemBookDetail(Item item, BookDetail bookDetail)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            BookDetail = bookDetail ?? throw new ArgumentNullException(nameof(bookDetail));
        }
    }
}
