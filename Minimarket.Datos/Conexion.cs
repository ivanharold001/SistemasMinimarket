using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Minimarket.Datos
{
    public class Conexion
    {
        private string bd;
        private string servidor;
        private string usuarioId;
        private string clave;
        private bool seguridad;
        private static Conexion con = null;


        private Conexion()
        {
            this.bd = "BD_MINIMARKET";
            this.servidor = "DESKTOP-QRHGLSN";
            this.usuarioId = "sistemasAdmin";
            this.clave = "sistemas12345";
            this.seguridad = false;
        }

        public SqlConnection crearConexion()
        {
            SqlConnection cadena = new SqlConnection();
            try
            {
                cadena.ConnectionString = $"Server={this.servidor}; Database={this.bd};";
                if (seguridad)
                {
                    cadena.ConnectionString = cadena.ConnectionString + "Integrated Segurity = SSPI";
                }
                else
                {
                    cadena.ConnectionString = cadena.ConnectionString + $"User Id={this.usuarioId}; Password={this.clave};";
                }
            }
            catch (Exception ex)
            {
                cadena = null;
                throw ex;
            }

            return cadena;
        }

        public static Conexion getInstancia()
        {
            if (con == null)
            {
                con = new Conexion();
            }

            return con;
        }
    }
}
