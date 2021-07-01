using ExamenPrestamos.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenPrestamos
{
    public partial class Form1 : Form
    {
        PrestamoModel prestamoModel;
        public Form1()
        {
            InitializeComponent();
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PrestamosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPrestamos frmPrestamos = new FrmPrestamos();
            frmPrestamos.MdiParent = this;
            frmPrestamos.PrestamoModel = prestamoModel;
            frmPrestamos.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            prestamoModel = new PrestamoModel();
        }
    }
}
