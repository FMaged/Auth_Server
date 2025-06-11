

using Domain.Enums;

namespace Domain.ValueObjects.Cookie
{
    public class CookieOptions
    {
        private List<string>? _extensions;







        /// <summary>
        /// Gets or sets the domain to associate the cookie with.
        /// </summary>
        /// <returns>The domain to associate the cookie with.</returns>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the cookie path.
        /// </summary>
        /// <returns>The cookie path.</returns>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the expiration date and time for the cookie.
        /// </summary>
        /// <returns>The expiration date and time for the cookie.</returns>
        public DateTime Expires { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to transmit the cookie using Secure Sockets Layer (SSL)--that is, over HTTPS only.
        /// </summary>
        /// <returns>true to transmit the cookie only over an SSL connection (HTTPS); otherwise, false.</returns>
        public bool Secure { get; set; }

        /// <summary>
        /// Gets or sets the value for the SameSite attribute of the cookie. The default value is <see cref="SameSiteType.Unspecified"/>
        /// </summary>
        /// <returns>The <see cref="SameSiteType"/> representing the enforcement mode of the cookie.</returns>
        public SameSiteType SameSite { get; set; } = SameSiteType.Unspecified;

        /// <summary>
        /// Gets or sets a value that indicates whether a cookie is inaccessible by client-side script.
        /// </summary>
        /// <returns>true if a cookie must not be accessible by client-side script; otherwise, false.</returns>
        public bool HttpOnly { get; set; }

        /// <summary>
        /// Gets or sets the max-age for the cookie.
        /// </summary>
        /// <returns>The max-age date and time for the cookie.</returns>
        public TimeSpan? MaxAge { get; set; }

        /// <summary>
        /// Indicates if this cookie is essential for the application to function correctly. If true then
        /// consent policy checks may be bypassed. The default value is false.
        /// </summary>
        public bool IsEssential { get; set; }

        /// <summary>
        /// Gets a collection of additional values to append to the cookie.
        /// </summary>
        public IList<string> Extensions
        {
            get => _extensions ??= new List<string>();
        }


        public CookieOptions(List<string>? extensions, string domain, string path, DateTime expires, bool secure, SameSiteType sameSite, bool httpOnly, TimeSpan? maxAge, bool isEssential)
        {
            _extensions = extensions;
            Domain = domain;
            Path = path;
            Expires = expires;
            Secure = secure;
            SameSite = sameSite;
            HttpOnly = httpOnly;
            MaxAge = maxAge;
            IsEssential = isEssential;
        }
    }
}
