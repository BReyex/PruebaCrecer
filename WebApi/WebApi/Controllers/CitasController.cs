using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        //readonly string connectionString = "Data Source=LAPTOP-TR4E5L1R;Initial Catalog=CLINICA;Persist Security Info=True;User ID=sa;Password=sa";
        private readonly IConfiguration _configuration;
        public CitasController(IConfiguration config)
        {
            _configuration = config;
        }
        [HttpPost]
        [Route("CrearCita")]
        public IActionResult CrearCita([FromBody] Citas citas)
        {
            using (SqlConnection conn = new(_configuration["ConnectionString"]))
            {
                conn.Open();
                SqlTransaction sqlTran = conn.BeginTransaction();
                SqlCommand command = conn.CreateCommand();
                command.Transaction = sqlTran;
                try
                {
                    command.CommandText = "EXECUTE sp_CrearCita @IdPaciente='"+citas.IdPaciente+"',@IdMedico='"+citas.IdMedico+"',@FechaHora='"+citas.FechaHora+"'";
                    command.ExecuteNonQuery();
                    sqlTran.Commit();
                    conn.Close();
                    return Ok();
                }
                catch (Exception e)
                {
                    sqlTran.Rollback();
                    return NotFound(new { meessage = e.ToString() });
                }
                
            }
        }
        [HttpPost]
        [Route("RegistrarDiagnostico")]
        public IActionResult RegistrarDiagnostico([FromBody] Citas citas)
        {
            using (SqlConnection conn = new(_configuration["ConnectionString"]))
            {
                conn.Open();
                SqlTransaction sqlTran = conn.BeginTransaction();
                SqlCommand command = conn.CreateCommand();
                command.Transaction = sqlTran;
                try
                {
                    command.CommandText = "EXECUTE sp_RegistrarDiagnostico @IdCita='"+citas.IdCita+"',@Diagnostico='"+citas.Diagnostico+"'";
                    command.ExecuteNonQuery();
                    sqlTran.Commit();
                    conn.Close();
                    return Ok();
                }
                catch (Exception e)
                {
                    sqlTran.Rollback();
                    return NotFound(new { meessage = e.ToString() });
                }
            }
        }
        [HttpGet]
        [Route("ListarCitas")]
        public List<Citas> ListarCitas()
        {
            List<Citas> citas = new();
            using (SqlConnection conn = new(_configuration["ConnectionString"]))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                try
                {
                    command.CommandText = "EXECUTE sp_ListarCitas";
                    SqlDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        var cita = new Citas();
                        cita.IdCita = Convert.ToInt32(rdr["IdCita"]);
                        cita.IdPaciente = Convert.ToInt32(rdr["IdPaciente"]);
                        cita.IdMedico = Convert.ToInt32(rdr["IdMedico"]);
                        cita.FechaHora = Convert.ToDateTime(rdr["FechaHora"]);
                        cita.Diagnostico = rdr["Diagnostico"].ToString();
                        citas.Add(cita);
                    }
                    conn.Close();
                    return citas;
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
