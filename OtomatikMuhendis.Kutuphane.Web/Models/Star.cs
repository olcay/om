﻿namespace OtomatikMuhendis.Kutuphane.Web.Models
{
    public class Star
    {
        public Shelf Shelf { get; set; }

        public ApplicationUser User { get; set; }
        
        public int ShelfId { get; set; }
        
        public string UserId { get; set; }
    }
}
