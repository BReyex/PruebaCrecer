using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public class PacientesController : Controller
    {
        private readonly IConfiguration _configuration;
        public PacientesController(IConfiguration config)
        {
            _configuration = config;
        }
        public IActionResult Index()
        {
            //using (SqlConnection conn = new(_configuration["ConnectionString"]))
            //{
            //    conn.Open();
            //    SqlTransaction sqlTran = conn.BeginTransaction();
            //    SqlCommand command = conn.CreateCommand();
            //    command.Transaction = sqlTran;
            //    try
            //    {
            //        command.CommandText = "EXECUTE sp_CrearCita @IdPaciente='" + citas.IdPaciente + "',@IdMedico='" + citas.IdMedico + "',@FechaHora='" + citas.FechaHora + "'";
            //        command.ExecuteNonQuery();
            //        sqlTran.Commit();
            //        conn.Close();
            //        return Ok();
            //    }
            //    catch (Exception e)
            //    {
            //        sqlTran.Rollback();
            //        return NotFound(new { meessage = e.ToString() });
            //    }

            //}
            return View();
        }
    }
}
