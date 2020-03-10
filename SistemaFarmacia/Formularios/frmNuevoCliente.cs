using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaFarmacia.Modelos;

namespace SistemaFarmacia.Formularios
{
    public partial class frmNuevoCliente : Form
    {
        private List<Provincia> Provincias { get; set; }
        private List<Ciudad> Ciudades { get; set; }
        public frmNuevoCliente()
        {
            InitializeComponent();
            Farmacia contexto = new Farmacia();
            Provincias = contexto.Provincia.Distinct().OrderBy(x => x.ID_PROVINCIA).ToList();
            Ciudades = contexto.Ciudad.Distinct().OrderBy(x => x.NOMBRE_CIUDAD).ToList();
        }

        private void frmNuevoCliente_Load(object sender, EventArgs e)
        {
            Farmacia contexto = new Farmacia();

            cmbProvincias.DisplayMember = "NOMBRE_PROVINCIA";
            cmbProvincias.ValueMember = "ID_PROVINCIA";
            cmbProvincias.DataSource = Provincias;        
        }

        private void cmbProvincias_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCiudades.DisplayMember = "NOMBRE_CIUDAD";
            cmbCiudades.ValueMember = "ID_CIUDAD";
            cmbCiudades.DataSource = Ciudades.Where(x => x.ID_PROVINCIA == int.Parse(cmbProvincias.SelectedValue.ToString())).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Farmacia contexto = new Farmacia();
            if (button1.Text == "Agregar")
            {
                contexto.Agregar_Cliente(txtNombre.Text,txtApellido.Text, int.Parse(txtTelefono1.Text), int.Parse(txtTelefono2.Text), int.Parse(cmbCiudades.SelectedValue.ToString()), txtDireccion.Text, short.Parse(txtCaract1.Text), short.Parse(txtCaract2.Text));
            }
            else
            {
                if (button1.Text == "Modificar")
                {
                    //contexto.Modificar_Cliente(Id, txtNombre.Text, byte.Parse(cmbPresentacion.SelectedValue.ToString()), decimal.Parse(txtPrecio.Text), txtComponentes.Text);
                }
            }
            this.Close();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            txtNombre.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            txtApellido.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            txtDireccion.CharacterCasing = CharacterCasing.Upper;
        }
    }
}
