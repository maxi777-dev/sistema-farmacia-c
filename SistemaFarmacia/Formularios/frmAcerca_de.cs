using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SistemaFarmacia.Formularios
{
    public partial class frmAcerca_de : Form
    {
        public frmAcerca_de()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.linkedin.com/in/maximiliano-mainero-a91852169/");
        }

        private void frmAcerca_de_Load(object sender, EventArgs e)
        {

        }
    }
}
