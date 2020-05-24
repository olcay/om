namespace Otomatik.Library.Web.Core.Enums
{
    public static class ProfileTabs
    {
        public const string Shelves = "Shelves";
        public const string Bookmarks = "Bookmarks";

        public static string[] All()
        {
            return new[] {Shelves, Bookmarks};
        }
    }
}
