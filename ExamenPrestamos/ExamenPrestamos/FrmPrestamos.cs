using ExamenPrestamos.Model;
using ExamenPrestamos.Poco;
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
    public partial class FrmPrestamos : Form
    {
        public PrestamoModel PrestamoModel { get; set; }
        DataTable dt;
        public FrmPrestamos()
        {
            InitializeComponent();
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            AddPrestamos addPrestamos = new AddPrestamos();
            addPrestamos.FrmPrestamos = this;
            addPrestamos.PrestamoModel = PrestamoModel;
            addPrestamos.ShowDialog();
        }

        public void LoadTable()
        {
            dataGridView1.DataSource = null;
            dt = new DataTable();
            dt.Columns.Add("Monto");
            dt.Columns.Add("Plazo");
            dt.Columns.Add("Periodos");
            dt.Columns.Add("Tasa");
            dataGridView1.DataSource = dt;
            LoadData();
        }

        public void LoadData()
        {
            if (PrestamoModel.GetAll() == null)
            {
                return;
            }
            foreach (Prestamo p in PrestamoModel.GetAll())
            {
                DataRow dr = dt.NewRow();
                dr["Monto"] = p.Monto;
                dr["Plazo"] = p.Plazo;
                dr["Periodos"] = p.Periodo;
                dr["Tasa"] = p.Tasa;
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
        }

        private void FrmPrestamos_Load(object sender, EventArgs e)
        {
            List<Prestamo> prestamos = PrestamoModel.GetAll();
            if (prestamos != null)
            {
                LoadTable();
            } 
        }

        private void BtnVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridView1.CurrentCell.RowIndex;
                Prestamo p = PrestamoModel.GetPrestamo(index);
                AddPrestamos addPrestamos = new AddPrestamos();
                addPrestamos.FrmPrestamos = this;
                addPrestamos.PrestamoModel = PrestamoModel;
                addPrestamos.LoadTable(p.Plazo);
                addPrestamos.LoadAmortizacion(p);
                addPrestamos.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo visualizar el elemento!");
            }
        }
    }
}
