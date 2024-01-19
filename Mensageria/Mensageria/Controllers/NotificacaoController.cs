using Mensageria.Model;
using Mensageria.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mensageria.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificacaoController : ControllerBase
    {
        private readonly ILogger<NotificacaoController> _logger;

        public NotificacaoController(ILogger<NotificacaoController> logger)
        {
            _logger = logger;
        }

        [HttpPost("SendNotificacaoApp")]
        public Retorno<RetornoNotificacaoAppModel> SendNotificacaoApp([FromQuery] NotificacaoAppModel notificacao)
        {
            Retorno<RetornoNotificacaoAppModel> retorno = new Retorno<RetornoNotificacaoAppModel>();

            UtilNotificacao utilNotificacao = new UtilNotificacao();


            try
            {
                var response = utilNotificacao.SendNotificacaoApp(notificacao).Result;
                retorno.Data = JsonConvert.DeserializeObject<RetornoNotificacaoAppModel>(response);
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao enviar notificação";
            }

            return retorno;
        }

        [HttpPost("SendNotificacaoAppParametros")]
        public Retorno<RetornoNotificacaoAppModel> SendNotificacaoAppParametros(int IdEmpresa, int IdUsuario, string Titulo, string Mensagem, string Nome, string ImagemUsuario)
        {
            Retorno<RetornoNotificacaoAppModel> retorno = new Retorno<RetornoNotificacaoAppModel>();

            UtilNotificacao utilNotificacao = new UtilNotificacao();

            try
            {
                //string corpo = "", titulo = "";
                bool enviaNotificacao = true;
                NotificacaoAppModel notificacao = new NotificacaoAppModel();
                notificacao.data = new NotificacaoBodyModel();
                var campoUsuario = "";
                dynamic fireBaseUsuario = utilNotificacao.SelectTokenUsuario(IdEmpresa, IdUsuario, out campoUsuario);

                notificacao.to = fireBaseUsuario[campoUsuario].token;
                notificacao.data.Titulo = Titulo + " - " + Nome;
                notificacao.data.Messagem = Mensagem;
                notificacao.data.NomeUsuario = Nome;
                notificacao.data.IdUsuario = IdUsuario.ToString();
                notificacao.data.TokenUsuario = "";
                notificacao.data.ImageUsuario = ImagemUsuario;
                notificacao.data.IdTipoAlerta = "5502"; //Notificação
                notificacao.data.IdTipoNotificacao = "6904"; //Push
                notificacao.data.IdArea = "1";

                if (enviaNotificacao)
                {
                    utilNotificacao.EnviarNotificacaoApp(notificacao);
                }
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.Mensagem = "Erro ao enviar notificação";
            }

            return retorno;
        }
    }
}        