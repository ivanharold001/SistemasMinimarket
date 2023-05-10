using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Minimarket.Entidades;
using Minimarket.Datos;
using System.Data;

namespace Minimarket.Negocio
{
    public class N_Categorias
    {
        public static DataTable listado_ca(string ctexto)
        {
            D_Categorias datos = new D_Categorias();
            return datos.listado_ca(ctexto);
        }

        public static string guardar_ca(int nOpcion, E_Categorias oCa)
        {
            D_Categorias datos = new D_Categorias();
            return datos.guardar_ca(nOpcion, oCa);
        }

        public static string eliminar_ca(int nCodigo_ca)
        {
            D_Categorias datos = new D_Categorias();
            return datos.eliminar_ca(nCodigo_ca);
        }
    }
}
