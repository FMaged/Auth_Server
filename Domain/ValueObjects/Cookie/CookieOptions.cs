

using Domain.Enums;

namespace Domain.ValueObjects.Cookie
{

    // the Cookie hard coded for now the 
    // Issues with the DI, the Password and the PasswordValidator   
    public class CookieOptions
    {
        public const string Section = "CookieOptions";

        private List<string>? _extensions;





        /// <summary>
        /// Gets or sets the domain to associate the cookie with.
        /// </summary>
        /// <returns>The domain to associate the cookie with.</returns>
        public string Domain { get; set; } = "example.com";

        /// <summary>
        /// Gets or sets the cookie path.
        /// </summary>
        /// <returns>The cookie path.</returns>
        public string Path { get; set; } = "/";


        /// <summary>
        /// Gets or sets the max-age for the cookie.
        /// </summary>
        /// <returns>The max-age date and time for the cookie.</returns>
        private TimeSpan? _maxAge;
        /// Gets or sets the max-age for the cookie (relative expiration).
        /// If both MaxAge and Expires are set, MaxAge takes precedence.
        /// </summary>
        public TimeSpan? MaxAge 
        {
            get
            {
                if (_maxAge.HasValue)
                    return _maxAge;

                if (_expires.HasValue)
                    return _expires.Value - DateTime.UtcNow;

                return null;
            }

            set
            {
                //_maxAge = value;
                _maxAge = new TimeSpan(00, 30, 00);
            } 
        
        }




        /// <summary>
        /// Gets or sets the expiration date and time for the cookie.
        /// </summary>
        /// <returns>The expiration date and time for the cookie.</returns>
        private DateTime? _expires;
        /// <summary>
        /// Gets or sets the absolute expiration time for the cookie.
        /// If MaxAge is set, this will reflect MaxAge.
        /// </summary>
        public DateTime? Expires
        {
            get 
            {
                if (_expires.HasValue)
                    return _expires;

                if (_maxAge.HasValue)
                    return DateTime.UtcNow.Add(_maxAge.Value);

                return null; 
            }
            set => _expires = value;
        }
        /// <summary>
        /// Gets or sets a value that indicates whether to transmit the cookie using Secure Sockets Layer (SSL)--that is, over HTTPS only.
        /// </summary>
        /// <returns>true to transmit the cookie only over an SSL connection (HTTPS); otherwise, false.</returns>
        public bool Secure { get; set; } = true;

        /// <summary>
        /// Gets or sets the value for the SameSite attribute of the cookie. The default value is <see cref="SameSiteType.Unspecified"/>
        /// </summary>
        /// <returns>The <see cref="SameSiteType"/> representing the enforcement mode of the cookie.</returns>
        public SameSiteType SameSite { get; set; } =SameSiteType.None;

        /// <summary>
        /// Gets or sets a value that indicates whether a cookie is inaccessible by client-side script.
        /// </summary>
        /// <returns>true if a cookie must not be accessible by client-side script; otherwise, false.</returns>
        public bool HttpOnly { get; set; } = true;



        /// <summary>
        /// Indicates if this cookie is essential for the application to function correctly. If true then
        /// consent policy checks may be bypassed. The default value is false.
        /// </summary>
        public bool IsEssential { get; set; }=true;

        /// <summary>
        /// Gets a collection of additional values to append to the cookie.
        /// </summary>
        public IList<string> Extensions
        {
            get => _extensions ??= new List<string>();
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Extensions cannot be null");
                _extensions = value.ToList();
            }
        }

        public CookieOptions() 
        {
            _extensions = default!;
            Domain = default!;
            Path = default!;
            Secure = default!;
            SameSite = default!;
            HttpOnly = default!;
            MaxAge = default!;
            IsEssential = default!;
        }
        public CookieOptions(List<string>? extensions, string domain, string path, TimeSpan maxAge, bool secure, 
            SameSiteType sameSite, bool httpOnly, bool isEssential)
        {
            _extensions = extensions;
            Domain = domain;
            Path = path;
            Secure = secure;
            SameSite = sameSite;
            HttpOnly = httpOnly;
            MaxAge = maxAge;
            IsEssential = isEssential;
        }




        
    }
}
