using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mensageria.Model
{
    public class NotificacaoAppModel
    {
        public string to { get; set; }
        public NotificacaoBodyModel data { get; set; }
    }
}
