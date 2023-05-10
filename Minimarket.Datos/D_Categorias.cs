using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Minimarket.Entidades;

namespace Minimarket.Datos
{
    public class D_Categorias
    {
        public DataTable listado_ca(string ctexto)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon = Conexion.getInstancia().crearConexion();
                SqlCommand comando = new SqlCommand("USP_Listado_ca", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@cTexto", SqlDbType.VarChar).Value = ctexto;
                sqlCon.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
        }

        public string guardar_ca(int nOpcion, E_Categorias oCa)
        {
            string respuesta = "";

            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon = Conexion.getInstancia().crearConexion();
                SqlCommand comando = new SqlCommand("USP_Guardar_ca", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nOpcion", SqlDbType.Int).Value = nOpcion;
                comando.Parameters.Add("@nCodigo_ca", SqlDbType.Int).Value = oCa.codigo_ca;
                comando.Parameters.Add("@cDescripcion_ca", SqlDbType.VarChar).Value = oCa.descripcion_ca;
                sqlCon.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se puedo registrar los datos";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }

            return respuesta;
        }

        public string eliminar_ca(int nCodigo_ca)
        {
            string respuesta = "";

            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon = Conexion.getInstancia().crearConexion();
                SqlCommand comando = new SqlCommand("USP_Eliminar_ca", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nCodigo_ca", SqlDbType.Int).Value = nCodigo_ca;
                sqlCon.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo eliminar";

            }
            catch (Exception ex)
            {

                respuesta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }

            return respuesta;
        }
    }
}
