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
        private BindingSource _bsEntradasPLC { get; set; }
        private BindingSource _bsSalidasPLC { get; set; }
        private BindingSource _bsEntradasSIM { get; set; }
        private BindingSource _bsSalidasSIM { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            string ruta = "FicherosINI\\Cabeceras.ini";
            List<string> sectionErr = new List<string>();
            ModServer.CreateCabecerasFromINI(ruta, ref sectionErr);
            ModServer.s_PLCserver.PropertyChanged += server_PropertyChanged;
            ModServer.s_SIMserver.PropertyChanged += server_PropertyChanged;

            _bsEntradasPLC = new BindingSource();
            _bsEntradasPLC.DataSource = ModServer.s_ListEntradasPLC;

            DgEntradasPLC.AutoGenerateColumns = false;
            
            DgEntradasPLC.Columns.Add("PosGlobalColumn", "Pos Global");
            DgEntradasPLC.Columns["PosGlobalColumn"].DataPropertyName = "PosGlobal";
            DgEntradasPLC.Columns["PosGlobalColumn"].Width = 50;
            DgEntradasPLC.Columns["PosGlobalColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            DgEntradasPLC.Columns.Add("ValueColumn", "Value");
            DgEntradasPLC.Columns["ValueColumn"].DataPropertyName = "Value";
            DgEntradasPLC.Columns["ValueColumn"].Width = 50;
            DgEntradasPLC.Columns["ValueColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            DgEntradasPLC.Columns.Add("NameColumn", "Name");
            DgEntradasPLC.Columns["NameColumn"].DataPropertyName = "Name";
            DgEntradasPLC.Columns["NameColumn"].Width = 200;
            DgEntradasPLC.Columns["NameColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DgEntradasPLC.DataSource = _bsEntradasPLC;

            //

            _bsSalidasPLC = new BindingSource();
            _bsSalidasPLC.DataSource = ModServer.s_ListSalidasPLC;

            DgSalidasPLC.AutoGenerateColumns = false;

            DgSalidasPLC.Columns.Add("PosGlobalColumn", "Pos Global");
            DgSalidasPLC.Columns["PosGlobalColumn"].DataPropertyName = "PosGlobal";
            DgSalidasPLC.Columns["PosGlobalColumn"].Width = 50;
            DgSalidasPLC.Columns["PosGlobalColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            DgSalidasPLC.Columns.Add("ValueColumn", "Value");
            DgSalidasPLC.Columns["ValueColumn"].DataPropertyName = "Value";
            DgSalidasPLC.Columns["ValueColumn"].Width = 50;
            DgSalidasPLC.Columns["ValueColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DgSalidasPLC.Columns.Add("NameColumn", "Name");
            DgSalidasPLC.Columns["NameColumn"].DataPropertyName = "Name";
            DgSalidasPLC.Columns["NameColumn"].Width = 200;
            DgSalidasPLC.Columns["NameColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DgSalidasPLC.DataSource = _bsSalidasPLC;

            //

            _bsEntradasSIM = new BindingSource();
            _bsEntradasSIM.DataSource = ModServer.s_ListEntradasSIM;

            DgEntradasSIM.AutoGenerateColumns = false;

            DgEntradasSIM.Columns.Add("PosGlobalColumn", "Pos Global");
            DgEntradasSIM.Columns["PosGlobalColumn"].DataPropertyName = "PosGlobal";
            DgEntradasSIM.Columns["PosGlobalColumn"].Width = 50;
            DgEntradasSIM.Columns["PosGlobalColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            DgEntradasSIM.Columns.Add("ValueColumn", "Value");
            DgEntradasSIM.Columns["ValueColumn"].DataPropertyName = "Value";
            DgEntradasSIM.Columns["ValueColumn"].Width = 50;
            DgEntradasSIM.Columns["ValueColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DgEntradasSIM.Columns.Add("NameColumn", "Name");
            DgEntradasSIM.Columns["NameColumn"].DataPropertyName = "Name";
            DgEntradasSIM.Columns["NameColumn"].Width = 200;
            DgEntradasSIM.Columns["NameColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DgEntradasSIM.DataSource = _bsEntradasSIM;

            //

            _bsSalidasSIM = new BindingSource();
            _bsSalidasSIM.DataSource = ModServer.s_ListSalidasSIM;

            DgSalidasSIM.AutoGenerateColumns = false;

            DgSalidasSIM.Columns.Add("PosGlobalColumn", "Pos Global");
            DgSalidasSIM.Columns["PosGlobalColumn"].DataPropertyName = "PosGlobal";
            DgSalidasSIM.Columns["PosGlobalColumn"].Width = 50;
            DgSalidasSIM.Columns["PosGlobalColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            DgSalidasSIM.Columns.Add("ValueColumn", "Value");
            DgSalidasSIM.Columns["ValueColumn"].DataPropertyName = "Value";
            DgSalidasSIM.Columns["ValueColumn"].Width = 50;
            DgSalidasSIM.Columns["ValueColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DgSalidasSIM.Columns.Add("NameColumn", "Name");
            DgSalidasSIM.Columns["NameColumn"].DataPropertyName = "Name";
            DgSalidasSIM.Columns["NameColumn"].Width = 200;
            DgSalidasSIM.Columns["NameColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DgSalidasSIM.DataSource = _bsSalidasSIM;
        }
        

        private void DgEntradasPLC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //mitico try catch vacio para fumarnos el error
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 1)
                {
                    ModServer.s_ListEntradasPLC[e.RowIndex].Value = !ModServer.s_ListEntradasPLC[e.RowIndex].Value;
                }
            }
            catch (Exception ex){ }
            
        }

        private void DgEntradasSIM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //mitico try catch vacio para fumarnos el error
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 1)
                {
                    ModServer.s_ListEntradasSIM[e.RowIndex].Value = !ModServer.s_ListEntradasSIM[e.RowIndex].Value;
                }
            }
            catch (Exception ex) { }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void server_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Invoke
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => Invoked_PropertyChanged(sender, e)));
                return;
            }
            
        }
        private void Invoked_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Connected")
            {
                if (ModServer.s_PLCserver.Connected)
                {
                    LblPLCConnected.Text = "Conectado";
                    LblPLCConnected.ForeColor = Color.Green;
                }
                else
                {
                    LblPLCConnected.Text = "Desconectado";
                    LblPLCConnected.ForeColor = Color.Red;
                }
                if (ModServer.s_SIMserver.Connected)
                {
                    LblSIMConnected.Text = "Conectado";
                    LblSIMConnected.ForeColor = Color.Green;
                }
                else
                {
                    LblSIMConnected.Text = "Desconectado";
                    LblSIMConnected.ForeColor = Color.Red;
                }
            }
        }

        private void DgEntradasPLC_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DgEntradasPLC.Columns[e.ColumnIndex].DataPropertyName == "Value")
            {
                if (e.Value != null)
                {
                    bool value = (bool)e.Value;

                    if (value)
                    {
                        e.Value = "1";
                        e.CellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        e.Value = "0";
                        e.CellStyle.BackColor = Color.LightGray;
                    }

                    e.FormattingApplied = true;
                }
            }
        }

        private void DgSalidasPLC_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DgSalidasPLC.Columns[e.ColumnIndex].DataPropertyName == "Value")
            {
                if (e.Value != null)
                {
                    bool value = (bool)e.Value;

                    if (value)
                    {
                        e.Value = "1";
                        e.CellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        e.Value = "0";
                        e.CellStyle.BackColor = Color.LightGray;
                    }

                    e.FormattingApplied = true;
                }
            }
        }

        private void DgEntradasSIM_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DgEntradasSIM.Columns[e.ColumnIndex].DataPropertyName == "Value")
            {
                if (e.Value != null)
                {
                    bool value = (bool)e.Value;

                    if (value)
                    {
                        e.Value = "1";
                        e.CellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        e.Value = "0";
                        e.CellStyle.BackColor = Color.LightGray;
                    }

                    e.FormattingApplied = true;
                }
            }
        }

        private void DgSalidasSIM_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DgSalidasSIM.Columns[e.ColumnIndex].DataPropertyName == "Value")
            {
                if (e.Value != null)
                {
                    bool value = (bool)e.Value;

                    if (value)
                    {
                        e.Value = "1";
                        e.CellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        e.Value = "0";
                        e.CellStyle.BackColor = Color.LightGray;
                    }

                    e.FormattingApplied = true;
                }
            }
        }

        private void DgEntradasSIM_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Nos fumamos el error haciando nada
        }
    }
}
