namespace Otomatik.Library.Web.Core.Enums
{
    public static class ProfileTabs
    {
        public const string Shelves = "Shelves";
        public const string Stars = "Stars";
        public const string Followers = "Followers";
        public const string Following = "Following";

        public static string[] All()
        {
            return new[] {Shelves, Stars, Followers, Following};
        }
    }
}
