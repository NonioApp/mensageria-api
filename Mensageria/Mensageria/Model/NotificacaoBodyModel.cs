using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mensageria.Model
{
    public class NotificacaoBodyModel
    {
        public string Titulo { get; set; }
        public string Messagem { get; set; }
        public string NomeUsuario { get; set; }
        public string IdUsuario { get; set; }
        public string TokenUsuario { get; set; }
        public string ImageUsuario { get; set; }
        public string IdTipoAlerta { get; set; }
        public string IdTipoNotificacao { get; set; }
        public string IdArea { get; set; }
    }
}
