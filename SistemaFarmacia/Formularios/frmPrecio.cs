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
    public partial class frmPrecio : Form
    {
        private List<int> Lista { get; set; }
        public frmPrecio(List<int> lista)
        {
            InitializeComponent();
            Lista = lista;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Farmacia contexto = new Farmacia();
            foreach (var item in Lista)
            {
                contexto.Aumentar_precios(item, int.Parse(textBox1.Text));
            }
            this.Close();
        }
    }
}
