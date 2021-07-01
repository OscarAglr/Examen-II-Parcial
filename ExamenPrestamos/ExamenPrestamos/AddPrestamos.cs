using ExamenPrestamos.Amortizacion;
using ExamenPrestamos.Enums;
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
    public partial class AddPrestamos : Form
    {
        CuotaProporcional ct;
        public PrestamoModel PrestamoModel { get; set; }
        public FrmPrestamos FrmPrestamos { get; set; }
        DataTable dt;
        public AddPrestamos()
        {
            InitializeComponent();
        }

        private void AddPrestamos_Load(object sender, EventArgs e)
        {
            cbPeriodos.Items.AddRange(Enum.GetValues(typeof(Periodos))
                                          .Cast<object>().ToArray());
            cbPeriodos.SelectedIndex = 0;
            cbPeriodos.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void BtnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                dgvPrestamoTable.DataSource = null;
                int plazo = Convert.ToInt32(txtPlazo.Text);

                decimal monto = Convert.ToDecimal(txtMonto.Text);
                decimal tasa = Convert.ToDecimal(txtTasa.Text) / 100;
                int index = cbPeriodos.SelectedIndex;
                Periodos periodo = (Periodos)Enum.GetValues(typeof(Periodos)).GetValue(index);
                Prestamo p = new Prestamo()
                {
                    Monto = monto,
                    Plazo = plazo,
                    Periodo = periodo,
                    Tasa = tasa
                };
                LoadTable(plazo);
                LoadAmortizacion(p);
            }
            catch
            {
                MessageBox.Show("Sucedio un error al calcular");
            }
        }

        public void LoadTable(int plazo)
        {
            dt = new DataTable();
            
            dt.Columns.Add("No");
            dt.Columns.Add("Cuota");
            dt.Columns.Add("Abono");
            dt.Columns.Add("Interes");
            dt.Columns.Add("Saldo");
            for (int i = 0; i <= plazo; i++)
            {
                dt.Rows.Add($"{i}");
            }
            dgvPrestamoTable.DataSource = dt;
        }

        public void LoadAmortizacion(Prestamo p)
        {
            if (p == null)
            {
                throw new Exception("No se pudo calcular la amortizacion.");
            }
            if (dgvPrestamoTable.DataSource == null)
            {
                throw new Exception("La tabla está vacía.");
            }
            ct = new CuotaProporcional();
            dgvPrestamoTable.Rows[0].Cells[4].Value = p.Monto;
            int colCount = dgvPrestamoTable.ColumnCount;
            decimal[] cuotas = ct.CalcularCuota(p.Monto, p.Tasa, p.Plazo);
            decimal[] abonos = ct.CalcularAbono(p.Monto, p.Tasa, p.Plazo);
            decimal[] intereses = ct.CalcularInteres(p.Monto, p.Tasa, p.Plazo);
            decimal[] saldos = ct.GetSaldoInsoluto(p.Monto, p.Tasa, p.Plazo);

            for (int i = 0; i < p.Plazo; i++)
            {
                dgvPrestamoTable.Rows[i + 1].Cells[1].Value = cuotas[i];
                dgvPrestamoTable.Rows[i + 1].Cells[2].Value = abonos[i];
                dgvPrestamoTable.Rows[i + 1].Cells[3].Value = intereses[i];
                dgvPrestamoTable.Rows[i + 1].Cells[4].Value = saldos[i + 1];
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal monto = Convert.ToDecimal(txtMonto.Text);
                int plazo = Convert.ToInt32(txtPlazo.Text);
                decimal tasa = Convert.ToDecimal(txtTasa.Text) / 100;
                int index = cbPeriodos.SelectedIndex;
                Periodos periodo = (Periodos)Enum.GetValues(typeof(Periodos)).GetValue(index);
                Prestamo p = new Prestamo()
                {
                    Monto = monto,
                    Plazo = plazo,
                    Periodo = periodo,
                    Tasa = tasa
                };
                PrestamoModel.AddPrestamo(p);
                FrmPrestamos.LoadTable();
            }
            catch
            {
                MessageBox.Show("Sucedio un error", "Matrakazo");
                return;
            }
            
        }
    }
}
