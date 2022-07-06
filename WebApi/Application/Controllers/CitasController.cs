using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Application.Controllers
{
    public class CitasController : Controller
    {
        private readonly IConfiguration _configuration;
        public CitasController(IConfiguration config)
        {
            _configuration = config;
        }
        public IActionResult Index()
        {
            ViewBag.Pacientes = ListarPacientes();
            ViewBag.Medicos = ListarMedicos();
            return View(ListarCitas());
        }

        public List<Citas> ListarCitas()
        {
            List<Citas> citas = new();
            var url = $"https://localhost:5001/api/Citas/ListarCitas";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            citas = JsonConvert.DeserializeObject<List<Citas>>(responseBody);
                        }
                    }
                }
                return citas;
            }
            catch (WebException ex)
            {
                throw;
            }
        }
        public List<SelectListItem> ListarPacientes()
        {
            List<SelectListItem> selectListItems = new();
            var url = $"https://localhost:5001/api/Pacientes/ListarPacientes";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            selectListItems = JsonConvert.DeserializeObject<List<SelectListItem>>(responseBody);
                        }
                    }
                }
                return selectListItems;
            }
            catch (WebException ex)
            {
                throw;
            }
        }
        public List<SelectListItem> ListarMedicos()
        {
            List<SelectListItem> selectListItems = new();
            var url = $"https://localhost:5001/api/Medicos/ListarMedicos";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            selectListItems = JsonConvert.DeserializeObject<List<SelectListItem>>(responseBody);
                        }
                    }
                }
                return selectListItems;
            }
            catch (WebException ex)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult Crear()
        {
            try
            {
                //return Content("Hello, " + HttpContext.Request.Form["FechaHora"] + HttpContext.Request.Form["Paciente"]);
                var url = $"https://localhost:5001/api/Citas/CrearCita";
                var Cita = new Citas();
                // Cita.FechaHora = Convert.ToDateTime(HttpContext.Request.Form["FechaHora"]);
                Cita.FechaHora = "2022-07-06";
                Cita.IdMedico = Convert.ToInt32(HttpContext.Request.Form["Medico"]);
                Cita.IdPaciente = Convert.ToInt32(HttpContext.Request.Form["Paciente"]);
                string JsonBody = JsonConvert.SerializeObject(Cita);
                byte[] byteArray = Encoding.UTF8.GetBytes(JsonBody);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                request.Accept = "application/json";
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
            }
            catch (Exception)
            {

                throw;
            }
           
            return(RedirectToAction("Index"));

        }
    }
}
