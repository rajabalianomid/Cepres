using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cepres.IdentityServer.Model
{
    public class LogoutViewModel
    {
        public bool ShowLogoutPrompt { get; set; } = true;
        public string LogoutId { get; set; }
    }
}
