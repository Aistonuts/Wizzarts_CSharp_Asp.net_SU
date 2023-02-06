using System.Text;

namespace SUS.HTTP
{
    public class ResponseCookie:Cookie
    {
        public ResponseCookie(string name, string value)
            : base(name, value)
        {
            this.Path = "/";
        }
        //Mac-Age
        //Expires the same, by seconds, the other by end date validtaion of cookie
        public int MaxAge { get; set; }

        public bool HttpOnly { get; set; }

        public string Path { get; set; }

        //domain, path, Secure


        //Polymorphism
        public override string ToString()
        {
            StringBuilder cookieBuilder = new StringBuilder();
            cookieBuilder.Append($"{this.Name}={this.Value}; Path={this.Path};");
            if (MaxAge != 0)
            {
                cookieBuilder.Append($" Max-Age={this.MaxAge};");
            }

            if (this.HttpOnly)
            {
                cookieBuilder.Append(" HttpOnly;");
            }

            return cookieBuilder.ToString();
            // if(!string.IsNullOrWhiteSpace(this.Path)  see ctor no need to check
        }
    }
}
