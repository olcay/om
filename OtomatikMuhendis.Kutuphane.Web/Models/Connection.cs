using System;

namespace OtomatikMuhendis.Kutuphane.Web.Models
{
    /// <summary>
    /// Formats postgres url to connection string
    /// </summary>
    public class Connection
    {
        private readonly Uri _url;

        public Connection(string databaseUri)
        {
            if (!Uri.TryCreate(databaseUri, UriKind.Absolute, out _url))
            {
                throw new ArgumentException(nameof(databaseUri));
            }
        }

        public string String =>
            $"User ID={_url.UserInfo.Split(':')[0]};Password={_url.UserInfo.Split(':')[1]};Host={_url.Host};Port={_url.Port};Database={_url.LocalPath.Substring(1)};SSL Mode=Require;Trust Server Certificate=true";

    }
}
