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
    public partial class frmBuscarMedicamentos : Form
    {
        public frmBuscarMedicamentos()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Farmacia contexto = new Farmacia();
            List<Presentaciones> presentaciones = contexto.Presentaciones.ToList();
            Presentaciones present = new Presentaciones() { ID_PRESENTACION = 0, DETALLE_PRESENTACION = ""};
            presentaciones.Insert(0, present);
            cmbPresentacion.DataSource = presentaciones;
            cmbPresentacion.DisplayMember = "DETALLE_PRESENTACION";
            cmbPresentacion.ValueMember = "ID_PRESENTACION";
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[3].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.DataSource = Buscar();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Buscar();
        }

        private List<VTemp2> Buscar()
        {
            txtNombre.CharacterCasing = CharacterCasing.Upper;
            Farmacia contexto = new Farmacia();
            List<VTemp2> lista = contexto.VTemp2.ToList();
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                lista = lista.Where(x => x.NOMBRE.Contains(txtNombre.Text)).ToList();
                if (cmbPresentacion.SelectedIndex > 0)
                {
                    List<VTemp2> lista1 = contexto.VTemp2.Where(x => x.PRESENTACIÓN == cmbPresentacion.Text).ToList();
                    lista = lista.Intersect(lista1).ToList();
                }
            }
            else
            {
                if (cmbPresentacion.SelectedIndex > 0)
                {
                    lista = lista.Where(x => x.PRESENTACIÓN == cmbPresentacion.Text).ToList();
                }
            }
            return lista.OrderBy(x=>x.PRESENTACIÓN).ToList();
        }

        private void cmbPresentacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }

        private void ActivarForm(Form form)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            frmNuevoTemp form = new frmNuevoTemp();
            ActivarForm(form);
        }

        private void btnVerMas_Click(object sender, EventArgs e)
        {
            int id = 0;
            foreach (DataGridViewRow rowselected in dataGridView1.SelectedRows)
            {
                id = int.Parse(rowselected.Cells[0].Value.ToString());
            }
            frmNuevoTemp form = new frmNuevoTemp(id, false);
            ActivarForm(form);
        }

        private void btnPrecio_Click(object sender, EventArgs e)
        {
            List<int> id = new List<int>();
            foreach (DataGridViewRow rowselected in dataGridView1.SelectedRows)
            {
                id.Add(int.Parse(rowselected.Cells[0].Value.ToString()));
            }
            frmPrecio form = new frmPrecio(id);
            ActivarForm(form);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("¿Está seguro que desea eliminar? \n Si elimina un item, no se podrá recuperar.", "Precaución!", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Farmacia contexto = new Farmacia();
                foreach (DataGridViewRow rowselected in dataGridView1.SelectedRows)
                {
                    contexto.Eliminar_Temp(int.Parse(rowselected.Cells[0].Value.ToString()));
                }
            }
            dataGridView1.DataSource = Buscar().ToList();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int id = 0;
            foreach (DataGridViewRow rowselected in dataGridView1.SelectedRows)
            {
                id = int.Parse(rowselected.Cells[0].Value.ToString());
            }
            frmNuevoTemp form = new frmNuevoTemp(id, true);
            ActivarForm(form);
        }
    }
}