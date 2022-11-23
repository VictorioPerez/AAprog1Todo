using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ABMPersonaMio1
{
    internal class Acceso
    {
        SqlConnection cnn = new SqlConnection(@"Data Source=DESKTOP-HJBQUI6\SQLEXPRESS;Initial Catalog=TUPPI;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        public void Conectar()
        {
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.Text;
        }
        public void Desconectar()
        {
            cnn.Close();
        }
        public DataTable Consultar(string consultaSQL)
        {
            DataTable dt = new DataTable();
            Conectar();
            cmd.CommandText = consultaSQL;
            dt.Load(cmd.ExecuteReader());
            Desconectar();
            return dt;
        }
        public void oDB (string oDB)
        {
            Conectar();
            cmd.CommandText = oDB;
            cmd.ExecuteNonQuery();
            Desconectar();
        }
    }
}
