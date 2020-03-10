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
    public partial class frmNuevaVenta : Form
    {
        private List<VTemp2> Medicamentos { get; set; } // de acá tomo los medicamentos

        private List<int> PasarAForm = new List<int>();
        public frmNuevaVenta()
        {
            InitializeComponent();
            Farmacia contexto = new Farmacia();
            Medicamentos = contexto.VTemp2.ToList(); // de acá asigno los datos desde sql
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) // este método me permite hacer el "autocompletado" en la lista
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                TextBox autoText = e.Control as TextBox;
                if (autoText != null)
                {
                    autoText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                    addItems(DataCollection); // llamo al método que matchea
                    autoText.AutoCompleteCustomSource = DataCollection;
                }
            }
        }

        public void addItems(AutoCompleteStringCollection col) // este es el método que matchea con sql
        {
            foreach (VTemp2 item in Medicamentos)
            {
                col.Add(item.NOMBRE + " | " + item.PRESENTACIÓN); // tendria que agregar un ID oculto, para luego referenciar al medicamento (tiene id, pero no lo traje)
            }
        }

        private void Cerrar(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    PasarAForm.Add(int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()));
                    PasarAForm.Add(int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                }
            }
            frmBuscarVentClientes form = new frmBuscarVentClientes(PasarAForm); // tengo pensado pasar al proximo form una lista con int1 = ID medicamento y int2 = CANTIDAD para luego asignarle un cliente
            form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(Cerrar);
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

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value != null)
                {
                    bool bandera = false;
                    foreach (var item in Medicamentos)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString().ToUpper().Contains(item.NOMBRE))
                        {
                            bandera = true;
                            dataGridView1.Rows[i].Cells[2].Value = item.PRECIO;
                            dataGridView1.Rows[i].Cells[3].Value = item.ID;
                        }
                    }
                    if (!bandera)
                    {
                        MessageBox.Show("Debe ingresar un nombre de medicamento válido", "Error");
                        dataGridView1.Rows[i].Cells[0].Value = null;
                    }
                }
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[1].Value = "1";
        }
    }
}