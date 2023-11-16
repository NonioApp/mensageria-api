using Mensageria.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mensageria.Util
{
    public class UtilNotificacao
    {
        public async Task<string> SendNotificacaoApp(NotificacaoAppModel notificacao)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://fcm.googleapis.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "AAAAJbga0_E:APA91bEJs0OudkmmXlLsDgYLOjwLSI1482aUQRHYReEHPsp9HztZ-Rw6lkVIM8eHO9-a7SQYaSiLJggCzpAMbGzVvjFNLnjNeYaxBRi5gRNlDiv12mggegOTzgVptMq9QSrS-I0IDcxn");

            var json = await Task.Run(() => JsonConvert.SerializeObject(notificacao));
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("/fcm/send", content);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        public dynamic SelectTokenUsuario(int IdEmpresa, int IdUsuario, out string campoUsuario)
        {
            campoUsuario = "";
            var responseUsuario = BuscaTokenUsuario(IdEmpresa, IdUsuario).Result;
            if (responseUsuario != "null")
            {
                var jsonSuperior = JObject.Parse(responseUsuario);

                foreach (var obj in jsonSuperior)
                {
                    campoUsuario = obj.Key;
                }

                dynamic dataUsuario = JObject.Parse(responseUsuario);
                return dataUsuario;
            }
            else
                return null;
        }


        public async Task<string> BuscaTokenUsuario(int IdEmpresa, int IdUsuario)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://nonio-195017.firebaseio.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("/contato/Empresa " + IdEmpresa + "/_id_" + IdUsuario + ".json");
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }


        public Retorno<RetornoNotificacaoAppModel> EnviarNotificacaoApp(NotificacaoAppModel notificacao)
        {
            Retorno<RetornoNotificacaoAppModel> retorno = new Retorno<RetornoNotificacaoAppModel>();
            try
            {
                var response = SendNotificacaoApp(notificacao).Result;
                retorno.Data = JsonConvert.DeserializeObject<RetornoNotificacaoAppModel>(response);
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
