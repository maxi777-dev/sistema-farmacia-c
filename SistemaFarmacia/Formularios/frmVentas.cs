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
    public partial class frmVentas : Form
    { 
        public frmVentas()
        {
            InitializeComponent();
            dtpFecha.ValueChanged += new EventHandler(f_ValueChanged);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Farmacia contexto = new Farmacia();
            dataGridView1.DataSource = contexto.VVentas.ToList();
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 40;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            chkTodo.Checked = true;
        }

        private void Buscar()
        {
            Farmacia contexto = new Farmacia();
            List<VVentas> lista = contexto.VVentas.ToList();
            if (txtCliente.Text!=null)
            {
                lista = lista.Where(x => x.NOMBRE.Contains(txtCliente.Text)).ToList();
                if (txtMedicamento.Text!=null)
                {
                    lista = lista.Where(x => x.MEDICAMENTO.Contains(txtMedicamento.Text)).ToList();
                    if (dtpFecha.Value!=dtpFecha.MinDate)
                    {
                        lista = lista.Where(x => x.FECHA == dtpFecha.Value).ToList();
                    }
                }
            }
            dataGridView1.DataSource = lista;
        }

        private void ActivarForm(Form form)
        {
            //form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(textBox1_TextChanged);
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

        private void button1_Click(object sender, EventArgs e)
        {
            frmNuevaVenta form = new frmNuevaVenta();
            ActivarForm(form);
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            txtCliente.CharacterCasing = CharacterCasing.Upper;
            Buscar();
        }

        private void txtMedicamento_TextChanged(object sender, EventArgs e)
        {
            txtMedicamento.CharacterCasing = CharacterCasing.Upper;
            Buscar();
        }

        private void f_ValueChanged(object sender, EventArgs e)
        {
            Buscar();
            if (dtpFecha.Value!=DateTime.Parse("04/12/2018"))
            {
                chkTodo.Checked = false;
            }
        }

        private void chkTodo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodo.Checked)
            {
                dtpFecha.Value = DateTime.Parse("04 / 12 / 2018");
            }
        }
    }
}