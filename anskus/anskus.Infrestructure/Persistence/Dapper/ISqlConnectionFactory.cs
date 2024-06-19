using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Infrestructure.Persistence.Dapper
{
    public interface ISqlConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}
