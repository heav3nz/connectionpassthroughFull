using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empleados.Models.Data;

namespace Empleados.Models.Entitites
{
    public class EmpleadoEntities
    {
        public string numero_empleado { get; set; }
        public int CodigoLife { get; set; }
        public string NumeroEmpleado { get; set; }
        public int? IdDepartamento { get; set; }
        public int? IdCargo { get; set; }
        public int IdGrupoNomina { get; set; }
        public int? IdDistrito { get; set; }
        public int? IdBanco { get; set; }
        public int? IdSexo { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdSucursal { get; set; }
        public int? IdJefe { get; set; }
        public int? IdModalidadContrato { get; set; }
        public DateTime? FechaFinContraro { get; set; }
        public string NumeroInss { get; set; }
        public string NumeroRuc { get; set; }
        public bool? Activo { get; set; }
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public double SalarioPeriodo { get; set; }
        public bool? SalarioDolarizado { get; set; }
        public bool? ExcentoInss { get; set; }
        public bool? ExcentoIr { get; set; }
        public bool? Liquidado { get; set; }
        public DateTime FechaContrato { get; set; }
        public DateTime FechaVacaciones { get; set; }
        public DateTime? FechaCumpleanos { get; set; }
        public string Direccion { get; set; }
        public string ReferenciaDomiciliar { get; set; }
        public string TelefonoReferenciaDomiciliar { get; set; }
        public string TelefonoPersonal { get; set; }
        public bool? TpWhatsAppActivo { get; set; }
        public string CuentaBancaria { get; set; }
        public string CorreoInstitucional { get; set; }
        public bool? TpEnviarColilla { get; set; }
        public string ContactoEmergencia { get; set; }
        public string TelefonoContactoEmergencia { get; set; }
        public string CorreoPersonal { get; set; }
        public bool? CpEnviarColilla { get; set; }
        public bool? CalculaHrasExtra { get; set; }
        public string TelefonoInstitucional { get; set; }
    }
}
