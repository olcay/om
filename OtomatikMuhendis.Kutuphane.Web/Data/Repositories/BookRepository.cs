﻿using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using OtomatikMuhendis.Kutuphane.Web.Core.Repositories;
using System.Linq;

namespace OtomatikMuhendis.Kutuphane.Web.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Save(Book book)
        {
            _context.Books.Add(book);
        }

        public Book GetBook(int bookId)
        {
            return _context.Books
                .Single(b => b.Id == bookId);
        }
    }
}
