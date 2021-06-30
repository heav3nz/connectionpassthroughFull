using Empleados.Models.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Empleados.Models.Entitites;

namespace Empleados.Models.Services
{
    public class EmpleadoService
    {
        private DbContext _Database = null;

        //Constructor
        public EmpleadoService()
        {
            _Database = new DbContext();
        }


        public List<EmpleadoEntities> GetEmployesJson()
        {
            //se recomienda encapsular las consultas en procedimientos, para mejorar la seguridad, por ejemplo, no mostrar los objetos que existen en la BD
            return _Database.SqlQuery<EmpleadoEntities>("SELECT * FROM dbo.empleados;");
        }


        //Destructor
        ~EmpleadoService()
        {
            _Database = null;
        }
    }
}
