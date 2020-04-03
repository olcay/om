using System;

namespace Otomatik.Library.Web.Core.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
