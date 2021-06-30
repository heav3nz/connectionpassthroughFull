using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Empleados.Models.Data
{
    public class DbContext : ConnectionByPass.Connection
    {
        public DbContext(int CommandTimeout = 0) : base("Data source=tcp:demetechserver.database.windows.net,1433; Initial Catalog=life; User Id=cliffmedisut@demetechserver; password=Pa$$w0rd2010sqlupdate", CommandTimeout)
        {

        }
    }
}
