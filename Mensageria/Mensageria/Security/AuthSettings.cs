using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mensageria.Security
{
    public class AuthSettings
    {
        public string Secret { get; set; }
        public int ExpireIn { get; set; }
    }
}
