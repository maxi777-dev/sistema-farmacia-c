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
    public partial class frmBuscarVentClientes : Form
    {
        private List<int> Medicamentos { get; set; }

        public frmBuscarVentClientes(List<int> medicamentos)
        {
            InitializeComponent();
            Medicamentos = medicamentos;
        }

        private void frmBuscarVentClientes_Load(object sender, EventArgs e)
        {
            Farmacia contexto = new Farmacia();
            dataGridView1.DataSource = contexto.VClientes_.ToList();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id_cliente = 0;
            int id_producto = 0;
            foreach (DataGridViewRow rowselected in dataGridView1.SelectedRows)
            {
                id_cliente=(int.Parse(rowselected.Cells[0].Value.ToString()));
            }
            Farmacia contexto = new Farmacia();
            for (int i = 0; i < Medicamentos.Count; i++)
            {
                if (i % 2 == 0)
                {
                    id_producto = Medicamentos[i];
                }
                else
                {
                    contexto.Agregar_Venta(id_cliente, id_producto, DateTime.Today, byte.Parse(Medicamentos[i].ToString()));
                }
            }
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.CharacterCasing = CharacterCasing.Upper;
            Farmacia contexto = new Farmacia();
            List<VClientes_> lista = contexto.VClientes_.ToList();
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                lista = lista.Where(x => x.NOMBRE.Contains(textBox1.Text)).ToList();
            }
            dataGridView1.DataSource = lista;                    
        }

        private void btnAgregarNuevo_Click(object sender, EventArgs e)
        {
            frmNuevoCliente form = new frmNuevoCliente();
            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(textBox1_TextChanged);
            form.MdiParent = frmMDI.ActiveForm;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.FormClosed += F_FormClosed;
            form.Show();
            this.Enabled = false;
        }

        private void F_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
        }
    }
}
