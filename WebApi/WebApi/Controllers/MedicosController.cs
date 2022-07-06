using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MedicosController(IConfiguration config)
        {
            _configuration = config;
        }
        [HttpGet]
        [Route("ListarMedicos")]
        public List<SelectListItem> ListarMedicos()
        {
            List<SelectListItem> medicos = new();
            using (SqlConnection conn = new(_configuration["ConnectionString"]))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                try
                {
                    command.CommandText = "EXECUTE sp_ListarMedicos";
                    SqlDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        var medico = new SelectListItem();
                        medico.Value = Convert.ToString(rdr["IdMedico"]);
                        medico.Text = rdr["NombreMedico"].ToString();
                        medicos.Add(medico);
                    }
                    conn.Close();
                    return medicos;
                }
                catch (Exception e)
                {
                    conn.Close();
                    throw;
                }

            }
        }
    }
}
