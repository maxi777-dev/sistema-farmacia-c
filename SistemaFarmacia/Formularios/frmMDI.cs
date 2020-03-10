using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaFarmacia.Modelos;

namespace SistemaFarmacia.Formularios
{
    public partial class frmMDI : Form
    {
        public frmMDI()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized; 
        }

        private void AbrirFormMDI(Form form)
        {
            form.MdiParent = this;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }

        private void ingresarVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNuevaVenta form = new frmNuevaVenta();
            AbrirFormMDI(form);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAcerca_de form = new frmAcerca_de();
            AbrirFormMDI(form);
        }

        private void Medicamentos_listar_Click(object sender, EventArgs e)
        {
            frmBuscarMedicamentos form = new frmBuscarMedicamentos();
            AbrirFormMDI(form);
        }

        private void listarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmVentas form = new frmVentas();
            AbrirFormMDI(form);
        }

        private void Medicamentos_agregar_Click(object sender, EventArgs e)
        {
            frmNuevoTemp form = new frmNuevoTemp();
            AbrirFormMDI(form);
        }

        private void Clientes_agregar_Click(object sender, EventArgs e)
        {
            frmNuevoCliente form = new frmNuevoCliente();
            AbrirFormMDI(form);
        }

        private void Clientes_listar_Click(object sender, EventArgs e)
        {
            frmBuscarClientes form = new frmBuscarClientes();
            AbrirFormMDI(form);
        }

        private void agregarNuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNuevaVenta form = new frmNuevaVenta();
            AbrirFormMDI(form);
        }
    }
}
