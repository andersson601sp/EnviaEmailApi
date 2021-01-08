using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Email.BLL;
using EnviaEmailApi.Models;
using Newtonsoft;
using Newtonsoft.Json;
using System.Data;
using Email.DML;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EnviaEmailApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [System.Web.Http.RoutePrefix("")]
    public class EmailController : Controller
    {
        // GET: Email
        public JsonResult Lista()
        {
            var strcnn = System.Configuration.ConfigurationManager.AppSettings["connectionString"].ToString();
            ContatosBLL contatos = new ContatosBLL(strcnn);
            DataTable result = contatos.CONSULTA();
            List<Contatos> listaContatos = new List<Contatos>();

            foreach (DataRow row in result.Rows)
            {
                Contatos contato = new Contatos();
                contato.id = Convert.ToByte(row["ID"]);
                contato.nome = row["NOME"].ToString();
                contato.email = row["EMAIL"].ToString();
                listaContatos.Add(contato);
            }

            return Json(JsonConvert.SerializeObject(listaContatos), JsonRequestBehavior.AllowGet);
        }

        public  ActionResult Inserir(Contatos _contato)
        {
            var strcnn = System.Configuration.ConfigurationManager.AppSettings["connectionString"].ToString();
            ContatosBLL contatos = new ContatosBLL(strcnn);
            Contato contato = new Contato()
            {
                Nome = _contato.nome,
                Email = _contato.email
            };
            contatos.Incluir(contato);
            return Lista();
        }

        public ActionResult Alterar(Contatos _contato)
        {
            var strcnn = System.Configuration.ConfigurationManager.AppSettings["connectionString"].ToString();
            ContatosBLL contatos = new ContatosBLL(strcnn);
            Contato contato = new Contato()
            {
                Id = _contato.id,
                Nome = _contato.nome,
                Email = _contato.email
            };
            contatos.Alterar(contato);
            return Lista();
        }

        public ActionResult Excluir(byte id)
        {
            var strcnn = System.Configuration.ConfigurationManager.AppSettings["connectionString"].ToString();
            ContatosBLL contatos = new ContatosBLL(strcnn);

            contatos.Excluir(id);
            return Lista();;
        }

        public JsonResult Enviar(Contatos model)
        {
            return Lista();
        }

    }
}