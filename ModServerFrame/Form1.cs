using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModServerFrame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DgEntradasPLC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                ModServer.ListEntradasPLC[e.RowIndex].Value = !ModServer.ListEntradasPLC[e.RowIndex].Value;
            }
        }

        private void DgEntradasSIM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                ModServer.ListEntradasSim[e.RowIndex].Value = !ModServer.ListEntradasSim[e.RowIndex].Value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string ruta = "FicherosINI\\Cabeceras.ini";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
