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
    public class PacientesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PacientesController(IConfiguration config)
        {
            _configuration = config;
        }
        [HttpGet]
        [Route("ListarPacientes")]
        public List<SelectListItem> ListarPacientes()
        {
            List<SelectListItem> pacientes = new();
            using (SqlConnection conn = new(_configuration["ConnectionString"]))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                try
                {
                    command.CommandText = "EXECUTE sp_ListarPacientes";
                    SqlDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        var paciente = new SelectListItem();
                        paciente.Value = Convert.ToString(rdr["IdPaciente"]);
                        paciente.Text = rdr["NombrePaciente"].ToString();
                        pacientes.Add(paciente);
                    }
                    conn.Close();
                    return pacientes;
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
