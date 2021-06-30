using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empleados.Models.Services;

namespace Empleados.Controllers
{
    [Route("Empleados/Services")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {

        [HttpGet("v1/GetAll")]
        public ActionResult ObtenerEmpleados()
        {

            var Empleados = new EmpleadoService().GetEmployesJson();

            return StatusCode(200, Empleados);
        }

    }
}
