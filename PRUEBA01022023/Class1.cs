using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA01022023
{
    internal class STORE
    {
        //conexion de la base de datos. 
        public static SqlConnection Conectar()
        {
            //SqlConnection cn = new SqlConnection("Data Source= 136.166.11.14;Database=DBSO_MX;Integrated Security=True");
            SqlConnection cn = new SqlConnection("Server=136.166.11.14;Database=DBSO_MX;User Id=becario.cpfr;Password=Lgems135;");
            cn.Open();
            return cn;
        }
    }
}
