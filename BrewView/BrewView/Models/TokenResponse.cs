using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BrewView.Models
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string IdToken { get; set; }
        public string RefreshToken { get; set; }
        public string ExpiresIn { get; set; }
    }
}
