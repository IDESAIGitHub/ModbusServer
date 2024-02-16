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
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }catch (Exception ex) { }
        }
        public int PosGlobal { get; private set; }
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
        public EntradaCabecera(string name, int posGlobal)
        {
            Value = false;
            Name = name;
            PosGlobal = posGlobal;
        }
    }
    public class SalidaCabecera : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex) { }
        }
        public int PosGlobal { get; private set; }
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
        public SalidaCabecera(string name, int posGlobal)
        {
            Value = false;
            Name = name;
            PosGlobal = posGlobal;
        }
    }
    class ModServer : INotifyPropertyChanged
    {
        static public ModServer s_PLCserver = new ModServer();
        static public ModServer s_SIMserver = new ModServer();

        private ModbusServer _modbusServer;
        //los registros de entrada y salida empiezan en 1
        static private ModbusServer.DiscreteInputs _plcInputs;
        static private ModbusServer.Coils _plcOutputs;
        static private ModbusServer.DiscreteInputs _simInputs;
        static private ModbusServer.Coils _simOutputs;

        private ushort _decStartReadAddress;
        private ushort _decStartWriteAddress;

        static private IniManager s_IniManager;
        
        
        
        static public int NumEntradas { get; private set; }
        static public int NumSalidas { get; private set; }
        static public BindingList<EntradaCabecera> s_ListEntradasPLC;
        static public BindingList<SalidaCabecera> s_ListSalidasPLC;

        static public BindingList<EntradaCabecera> s_ListEntradasSIM;
        static public BindingList<SalidaCabecera> s_ListSalidasSIM;
        public bool Connected
        {
            get { return _connected; }
            private set
            {
                _connected = value;
                OnPropertyChanged("Connected");
            }
        }
        static private bool _connected;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex) { }
        }
        //Constructor
        public ModServer()
        {
            
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
            ushort readStartAddress = 0;
            ushort writeStartAddress = 0;
            byte slaveReadAddress = 0;
            byte slaveWriteAddress = 0;
            if (type == "Weidmuller")
            {
                slaveReadAddress = 0;
                slaveWriteAddress = 0;
                readStartAddress = 0x0001;  //Read bit Address 
                writeStartAddress = 0x8001; //Write bit Address 
            }
            else if (type == "VIPA")
            {
                slaveReadAddress = 0;
                slaveWriteAddress = 0;
                readStartAddress = 0x0001;  //1x Bit access to input area
                writeStartAddress = 0x0001; //0x Bit access to output area
            }
            s_PLCserver._decStartReadAddress = readStartAddress;
            s_PLCserver._decStartWriteAddress = writeStartAddress;

            s_SIMserver._decStartReadAddress = readStartAddress;
            s_SIMserver._decStartWriteAddress = writeStartAddress;

            int numModulosEntrada = s_IniManager.GetInt("CABECERA_" + i.ToString(), "NumModulosEntrada", 1, ref sectionsError);
            int numModulosSalida = s_IniManager.GetInt("CABECERA_" + i.ToString(), "NumModulosSalida", 1, ref sectionsError);

            s_ListEntradasPLC = new BindingList<EntradaCabecera>();
            s_ListSalidasPLC = new BindingList<SalidaCabecera>();
            s_ListEntradasSIM = new BindingList<EntradaCabecera>();
            s_ListSalidasSIM = new BindingList<SalidaCabecera>();

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
                    s_ListEntradasPLC.Add(new EntradaCabecera(inputName, globalINPos));
                    s_ListSalidasSIM.Add(new SalidaCabecera(inputName, globalINPos));
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
                    s_ListSalidasPLC.Add(new SalidaCabecera(outputName, globalINPos));
                    s_ListEntradasSIM.Add(new EntradaCabecera(outputName, globalINPos));
                }
            }
            s_PLCserver.StartPLCServer(502);
            s_SIMserver.StartSIMServer(503);
            return true;
        }
        private bool StartPLCServer(int port)
        {
            _modbusServer = new ModbusServer();
            _modbusServer.Port = port;
            _modbusServer.Listen();
            _plcInputs = _modbusServer.discreteInputs;
            _plcOutputs = _modbusServer.coils;
            //los registros de entrada y salida empiezan en 1
            //for (int i = 1; i < ListEntradasPLC.Count +1 ; i++)
            //{
            //    if (i % 2 == 0)
            //    {
            //        _plcInputs[i] = true;
            //    }
            //    else
            //    {
            //        _plcInputs[i] = false;
            //    }
            //}
            _modbusServer.NumberOfConnectedClientsChanged += NumberOfConnectedClients_Changed;
            _modbusServer.CoilsChanged += PLCCoils_Changed;
            //entradas property changed handler
            s_ListEntradasPLC.ListChanged += ListEntradasPLC_Changed;
            return false;
        }
        private void ListEntradasPLC_Changed(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                _plcInputs[e.NewIndex + _decStartReadAddress] = s_ListEntradasPLC[e.NewIndex].Value;
            }
        }
        
        private bool StartSIMServer(int port)
        {
            _modbusServer = new ModbusServer();
            _modbusServer.Port = port;
            _modbusServer.Listen();
            _simInputs = _modbusServer.discreteInputs;
            _simOutputs = _modbusServer.coils;
            _modbusServer.NumberOfConnectedClientsChanged += NumberOfConnectedClients_Changed;
            _modbusServer.CoilsChanged += SIMCoils_Changed;
            s_ListEntradasSIM.ListChanged += ListEntradasSIM_ListChanged;
            return false;
        }
        private void ListEntradasSIM_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                _simInputs[e.NewIndex + _decStartReadAddress] = s_ListEntradasSIM[e.NewIndex].Value;
            }
        }
        public static bool CloseServer()
        {
            s_PLCserver._modbusServer.StopListening();
            s_SIMserver._modbusServer.StopListening();
            return true;
        }
        private void NumberOfConnectedClients_Changed()
        {
            Connected = _modbusServer.NumberOfConnections == 1;
        }

        public void PLCCoils_Changed(int register, int numberOfRegisters)
        {
            //los registros de entrada y salida empiezan en 1
            for (int i = register; i < register + numberOfRegisters; i++)
            {
                if (i- _decStartWriteAddress < s_ListSalidasPLC.Count)
                {
                    if (s_ListSalidasPLC[i - _decStartWriteAddress].Value != _plcOutputs[i])
                    {
                        s_ListSalidasPLC[i - _decStartWriteAddress].Value = _plcOutputs[i];
                        s_ListEntradasSIM[i - _decStartWriteAddress].Value = _plcOutputs[i];
                    }
                }
            }
        }
        public void SIMCoils_Changed(int register, int numberOfRegisters)
        {
            //los registros de entrada y salida empiezan en 1
            for (int i = register; i < register + numberOfRegisters; i++)
            {
                if (i - _decStartWriteAddress < s_ListSalidasSIM.Count)
                {
                    if (s_ListSalidasSIM[i - _decStartWriteAddress].Value != _simOutputs[i])
                    {
                        s_ListSalidasSIM[i - _decStartWriteAddress].Value = _simOutputs[i];
                        s_ListEntradasPLC[i - _decStartWriteAddress].Value = _simOutputs[i];
                    }
                }
            }
        }
    }
}