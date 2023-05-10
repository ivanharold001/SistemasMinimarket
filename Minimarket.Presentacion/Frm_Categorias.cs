using Minimarket.Entidades;
using Minimarket.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minimarket.Presentacion
{
    public partial class Frm_Categorias : Form
    {
        public Frm_Categorias()
        {
            InitializeComponent();
        }

        #region "Mis variables"
        int estadoGuarda = 0;
        int codigo_ca = 0;
        #endregion

        #region "Mis metodos"
        // Adicionar propiedades al datagridview
        private void formato_ca()
        {
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[0].HeaderText = "CODIGO";
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[1].HeaderText = "DESCRIPCION";
        }

        private void visibilidadBTN(bool estadoVisible)
        {
            this.btnCancelar.Visible = estadoVisible;
            this.btnGuardar.Visible = estadoVisible;
        }

        private void listado_ca(string cTexto)
        {
            try
            {
                dataGridView1.DataSource = N_Categorias.listado_ca(cTexto);
                this.formato_ca();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void estadoBotones(bool btnEstado)
        {
            this.btnNuevo.Enabled = btnEstado;
            this.btnActualizar.Enabled = btnEstado;
            this.btnEliminar.Enabled = btnEstado;
            this.btnReportes.Enabled = btnEstado;
            this.btnSalir.Enabled = btnEstado;
        }

        private void validarElementoSelecionado()
        {
            if (string.IsNullOrEmpty(Convert.ToString(dataGridView1.CurrentRow.Cells["codigo_ca"].Value)))
            {
                MessageBox.Show("No se tiene informacion para visualizar ", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.codigo_ca = Convert.ToInt32(dataGridView1.CurrentRow.Cells["codigo_ca"].Value);
                txtDesripcion_ca.Text = $"{dataGridView1.CurrentRow.Cells["descripcion_ca"].Value}";
            }
        }

        #endregion

        private void Frm_Categorias_Load(object sender, EventArgs e)
        {
            this.listado_ca("%");
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            estadoGuarda = 1; // nuevo registro
            estadoBotones(false);
            visibilidadBTN(true);
            this.txtDesripcion_ca.Text = "";
            this.tbcPrincipal.SelectedIndex = 1;
            this.txtDesripcion_ca.Focus();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            validarElementoSelecionado();
            estadoGuarda = 2; // actualizar
            estadoBotones(false);
            visibilidadBTN(true);
            this.tbcPrincipal.SelectedIndex = 1;
            this.txtDesripcion_ca.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(dataGridView1.CurrentRow.Cells["codigo_ca"].Value)))
            {
                MessageBox.Show("Error no se ha seleccionado un elemento", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult alerta;
                alerta = MessageBox.Show("¿Estas seguro de eliminar el registro?", "Aviso del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (alerta.Equals(DialogResult.Yes))
                {
                    string respuesta = "";
                    this.codigo_ca = Convert.ToInt32(dataGridView1.CurrentRow.Cells["codigo_ca"].Value);
                    respuesta = N_Categorias.eliminar_ca(this.codigo_ca);
                    if (respuesta.Equals("OK"))
                    {
                        this.listado_ca("%");
                        this.codigo_ca = 0;
                        MessageBox.Show("Registro eliminado correctamente", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            Reportes.Frm_Rpt_Categorias reporte = new Reportes.Frm_Rpt_Categorias();
            reporte.txtP1.Text = txtBuscar.Text;
            reporte.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            estadoGuarda = 0;
            estadoBotones(true);
            visibilidadBTN(false);
            this.txtDesripcion_ca.Text = "";
            this.tbcPrincipal.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDesripcion_ca.Text.Trim().Equals(""))
            {
                MessageBox.Show($"Falta ingresar datos requeridos {label1.Text}", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                E_Categorias oCa = new E_Categorias();
                string respuesta = "";
                oCa.codigo_ca = this.codigo_ca;
                oCa.descripcion_ca = txtDesripcion_ca.Text.Trim();
                respuesta = N_Categorias.guardar_ca(estadoGuarda, oCa);
                if (respuesta.Equals("OK"))
                {
                    MessageBox.Show("Los datos han sido guardados con exito", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    estadoGuarda = 0;
                    estadoBotones(true);
                    tbcPrincipal.SelectedIndex = 0;
                    txtDesripcion_ca.Text = "";
                    this.codigo_ca = 0;
                    visibilidadBTN(false);
                    this.listado_ca("%");
                }
                else
                {
                    MessageBox.Show(respuesta, "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            this.validarElementoSelecionado();
            tbcPrincipal.SelectedIndex = 1;
        }
    }
}
