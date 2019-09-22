using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//Habilitar session no controller.
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Dog_Company.Controllers
{
    [Autorizacao]
    public class ClienteController : Controller
    {
        public IActionResult Cliente()
        {
            return View();
        }
        [HttpPost]
        public JsonResult PesquisarCpf([FromBody]Dictionary<string, string> dados)
        {
            bool existe = false;
            Models.Cliente c = new Models.Cliente
            {
                Cpf = dados["cpf"]
            };
            bool valido = c.IsCpf();
            if (valido)
            {
                c.PesqByCpf(out existe);
            }
            var retorno = new
            {
                cliente = c,
                valido,
                existe
            };


            return Json(retorno);
        }

        public JsonResult Gravar([FromBody]Dictionary<string, string> dados)
        {
            
            bool gravou = false;
            Models.Cliente c = new Models.Cliente
            {
                Cpf = dados["cpf"],
                Nome = dados["nome"],
                Sobrenome = dados["sobrenome"],
                Celular = dados["celular"],
                Cidade = dados["cidade"],
                Bairro = dados["bairro"],
                Rua = dados["rua"],
                Numero = Convert.ToInt32(dados["numero"])
            };


            c.ValidaDados(out List<string> msg);

            if (msg.Count == 0)
            {
             gravou = c.GravarDados();
            }
            var retorno = new
            {
                gravou,
                msg
            };
            return Json(retorno);

        }

        public JsonResult Excluir([FromBody]Dictionary<string, string> dados)
        {
            Models.Cliente c = new Models.Cliente
            {
                Cpf = dados["cpf"]
            };
            c.Remover(out bool excluiu);

            var retorno = new
            {
                excluiu,
            };

            return Json(retorno);
        }
    }
}