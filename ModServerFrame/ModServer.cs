using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using EasyModbus;
using INIGestor;

namespace ModServerFrame
{
    public class EntradaCabecera : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool _value;
        public bool Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Valor");
            }
        }
        public string Name { get; private set; }
        public EntradaCabecera(string name)
        {
            Value = false;
            Name = name;
        }
    }
    public class SalidaCabecera : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool _value;
        public bool Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Valor");
            }
        }
        public string Name { get; private set; }
        public SalidaCabecera(string name)
        {
            Value = false;
            Name = name;
        }
    }
    class ModServer : INotifyPropertyChanged
    {
        static public ModServer PLCserver;
        static public ModServer SIMserver;

        private ModbusServer _modbusServer;
        static private ModbusServer.DiscreteInputs _plcInputs;
        static private ModbusServer.Coils _plcOutputs;
        static private ModbusServer.DiscreteInputs _simInputs;
        static private ModbusServer.Coils _simOutputs;
        
        static private IniManager s_IniManager;
        
        
        
        static public int NumEntradas { get; private set; }
        static public int NumSalidas { get; private set; }
        static public BindingList<EntradaCabecera> ListEntradasPLC;
        static public BindingList<SalidaCabecera> ListSalidasPLC;

        static public BindingList<EntradaCabecera> ListEntradasSim;
        static public BindingList<SalidaCabecera> ListSalidasSim;
        public bool Connected
        {
            get { return _connected; }
            set
            {
                _connected = value;
                OnPropertyChanged("Valor");
            }
        }
        static private bool _connected;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //Constructor
        public ModServer(int port)
        {
            if (port == 502)
            {
                
            }
            if (port == 503)
            {

            }
        }
        static public bool CreateCabecerasFromINI(string iniFileName, ref List<string> sectionsError)
        {
            s_IniManager = new IniManager(iniFileName);
            int numcabeceras = s_IniManager.GetInt("GENERAL", "NumeroCabeceras", 1, ref sectionsError);
            int i = 0;
            string type = s_IniManager.GetString("CABECERA_" + i.ToString(), "Tipo", "Weidmuller/VIPA", ref sectionsError);
            if (type != "Weidmuller" && type != "VIPA")
            {
                s_IniManager.SetValue("CABECERA_" + i.ToString(), "Tipo", "Weidmuller/VIPA");
            }
            
            int numModulosEntrada = s_IniManager.GetInt("CABECERA_" + i.ToString(), "NumModulosEntrada", 1, ref sectionsError);
            int numModulosSalida = s_IniManager.GetInt("CABECERA_" + i.ToString(), "NumModulosSalida", 1, ref sectionsError);

            ListEntradasPLC = new BindingList<EntradaCabecera>();
            ListSalidasPLC = new BindingList<SalidaCabecera>();
            ListEntradasSim = new BindingList<EntradaCabecera>();
            ListSalidasSim = new BindingList<SalidaCabecera>();

            for (int j = 0; j < numModulosEntrada; j++)
            {
                string moduleSection = "CABECERA_" + i.ToString() + ",ModuloEntrada_" + j.ToString();
                int numInputs = s_IniManager.GetInt(moduleSection, "NumeroEntradas", 16, ref sectionsError);

                for (int k = 0; k < numInputs; k++)
                {
                    string key = "IN_" + k.ToString();
                    string entradaProperties = s_IniManager.GetString(moduleSection, key, "", ref sectionsError);
                    int globalINPos = 0;
                    int isNegated = 0;
                    int msFilterRising = 0;
                    int msFilterFalling = 0;
                    string inputName = "";
                    IniManager.GetCabeceraInputProperties(moduleSection, key, entradaProperties, out globalINPos, out isNegated, out msFilterRising, out msFilterFalling, out inputName, ref sectionsError);
                    ListEntradasPLC.Add(new EntradaCabecera(inputName));
                    ListSalidasSim.Add(new SalidaCabecera(inputName));
                }
            }

            for (int j = 0; j < numModulosSalida; j++)
            {
                string moduleSection = "CABECERA_" + i.ToString() + ",ModuloSalida_" + j.ToString();
                int numOut = s_IniManager.GetInt(moduleSection, "NumeroSalidas", 16, ref sectionsError);

                for (int k = 0; k < numOut; k++)
                {
                    string key = "OUT_" + k.ToString();
                    string entradaProperties = s_IniManager.GetString(moduleSection, key, "", ref sectionsError);
                    int globalINPos = 0;
                    int isNegated = 0;
                    string outputName = "";
                    IniManager.GetCabeceraOutputProperties(moduleSection, key, entradaProperties, out globalINPos, out isNegated, out outputName, ref sectionsError);
                    ListSalidasPLC.Add(new SalidaCabecera(outputName));
                    ListEntradasSim.Add(new EntradaCabecera(outputName));
                }
            }
            return true;
        }
        public bool StartPLCServer(int port)
        {
            _modbusServer = new ModbusServer();
            _modbusServer.Port = port;
            _modbusServer.Listen();
            _plcInputs = _modbusServer.discreteInputs;
            _plcOutputs = _modbusServer.coils;
            _modbusServer.NumberOfConnectedClientsChanged += NumberOfConnectedClients_Changed;
            _modbusServer.CoilsChanged += Coils_Changed;
            return false;
        }
        public bool StartSIMServer(int port)
        {
            _modbusServer = new ModbusServer();
            _modbusServer.Port = port;
            _modbusServer.Listen();
            _simInputs = _modbusServer.discreteInputs;
            _simOutputs = _modbusServer.coils;
            _modbusServer.NumberOfConnectedClientsChanged += NumberOfConnectedClients_Changed;
            _modbusServer.CoilsChanged += Coils_Changed;
            return false;
        }
        
        private void NumberOfConnectedClients_Changed()
        {
            Connected = _modbusServer.NumberOfConnections == 1;
        }

        public void Coils_Changed(int register, int numberOfRegisters)
        {
            int a = 0;
            
        }
    }
}