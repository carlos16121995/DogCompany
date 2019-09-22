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
    public class VendaController : Controller
    {   
        [AllowAnonymous]
        public IActionResult Login()
        {
            HttpContext.Session.Remove("logado");
            return View();
        }
        [AllowAnonymous]
        public JsonResult Entrar([FromBody]Dictionary<string, string> dados)
        {
            var url = "";
            bool logado = false;
            if(dados["login"] == "admin@hotmail.com" && dados["senha"] == "admin")
            {
                url = "/Venda/Home";
                HttpContext.Session.SetString("logado", "logado");
                logado = true;
            }

            var retorno = new
            {
                logado,
                url
            };

            return Json(retorno);
        }
        public IActionResult Home()
        {
            return View("Venda");
        }
        public IActionResult Venda()
        {
            return View();
        }
        public IActionResult Pesquisar()
        {
            return View();
        }
        public JsonResult AddObserver([FromBody]Dictionary<string, string> dados)
        {
            Models.Cliente c = new Models.Cliente();
            Models.Animal a = new Models.Animal();
            c.Cpf = dados["cliente"];
            a.Id = Convert.ToInt32(dados["animal"]);
            a.Adicionar(c);
            var retorno = new
            {
                cpf = c.Cpf,
                id = a.Id,
            };

            return Json(retorno);
        }
        public JsonResult PesquisarById([FromBody]Dictionary<string, string> dados)
        {

            bool existe = false;
            Models.Animal a = new Models.Animal();
            List<object> an = new List<object>();

            if (dados == null)
            {

                an = a.PegarTodos(out existe);
            }
            else
            {
                a.Id = Convert.ToInt32(dados["id"]);
                a.PesquisarById(out existe);
                an.Add(a);

            }

            var retorno = new
            {
                animal = an,
                existe
            };

            return Json(retorno);
        }
        public JsonResult RemoverOb([FromBody]Dictionary<string, string> dados)
        {
            Models.Cliente c = new Models.Cliente();
            Models.Animal a = new Models.Animal();
            c.Cpf = dados["cpf"];
            a.Id = Convert.ToInt32(dados["id"]);
            a.Remover(c);
            var retorno = new
            {
                cpf = c.Cpf,
                id = a.Id,
            };

            return Json(retorno);
        }
    }
}
