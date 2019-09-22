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
    public class AnimalController : Controller
    {
        public IActionResult Animal()
        {
            return View();
        }
        public JsonResult Gravar ([FromBody]Dictionary<string, string> dados)
        {
            Models.Animal a = new Models.Animal
            {
                Nome = dados["nome"],
                Valor = Convert.ToSingle(dados["valor"]),
                Mes_nascimento = dados["mesNas"],
                Ano_nascimento = dados["anoNas"],
                Cor = dados["cor"],
                Adotado = Convert.ToBoolean(dados["adotado"]),
                Especie = dados["especie"],
                Raca = dados["raca"]
            };
            if (dados["id"] != "0")
            {
                a.Id = Convert.ToInt32(dados["id"]);
            }

            List<string> lista = new List<string>();
            a.GravarBd();
            lista = a.Notificar();
            var retorno = new
            {
                lista
            };

            return Json(retorno);
        }
    }
}