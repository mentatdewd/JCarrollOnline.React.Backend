using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jcarrollonline.react.backend.Models.ViewModels
{

    public class AuthResultDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
