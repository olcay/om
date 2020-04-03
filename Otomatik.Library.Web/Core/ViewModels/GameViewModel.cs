using System;

namespace Otomatik.Library.Web.Core.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageLink { get; internal set; }

        public string Description { get; set; }

        public string TitleOriginal { get; set; }

        public DateTimeOffset? Released { get; set; }
    }
}
