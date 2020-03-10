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
    public partial class frmNuevoTemp : Form
    {
        private int Id { get; set; }
        public frmNuevoTemp()
        {
            InitializeComponent();
        }

        public frmNuevoTemp(int id, bool mod)
        {
            Id = id;
            InitializeComponent();
            Farmacia contexto = new Farmacia();
            Temp2 temp = contexto.Temp2.Where(x => x.ID_TEMP == id).FirstOrDefault();
            txtNombre.Text = temp.NOMBRE;
            txtComponentes.Text = temp.COMPONENTES;
            txtPrecio.Text = temp.PRECIO.ToString();
            cmbPresentacion.Items.Insert(0, "Seleccionar");
            cmbPresentacion.SelectedIndex = 0;
            button1.Text = "Modificar";
            if (!mod)
            {
                button1.Text = "Cerrar";
                txtComponentes.Enabled = false;
                txtNombre.Enabled = false;
                cmbPresentacion.Enabled = false;
                txtPrecio.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Farmacia contexto = new Farmacia();
            if (button1.Text=="Agregar")
            {                
                contexto.Agregar_Temp(txtNombre.Text, byte.Parse(cmbPresentacion.SelectedValue.ToString()), decimal.Parse(txtPrecio.Text), txtComponentes.Text);
            }
            else
            {
                if (button1.Text=="Modificar")
                {
                    contexto.Modificar_temp(Id, txtNombre.Text, byte.Parse(cmbPresentacion.SelectedValue.ToString()), decimal.Parse(txtPrecio.Text), txtComponentes.Text);
                }
            }
            this.Close();
        }

        private void NuevoTemp_Load(object sender, EventArgs e)
        {
            Farmacia contexto = new Farmacia();
            List<Presentaciones> presentaciones = contexto.Presentaciones.ToList();
            cmbPresentacion.DisplayMember = "DETALLE_PRESENTACION";
            cmbPresentacion.ValueMember = "ID_PRESENTACION";
            cmbPresentacion.DataSource = presentaciones;            
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            txtNombre.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtComponentes_TextChanged(object sender, EventArgs e)
        {
            txtComponentes.CharacterCasing = CharacterCasing.Upper;
        }
    }
}
