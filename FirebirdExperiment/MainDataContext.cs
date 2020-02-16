using Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirebirdExperiment
{
    public class MainDataContext : INotifyPropertyChanged, MainDataContextInterface
    {
        public MainDataContext()
        {
            textGeo = "";
            indexGeo = 0;
            countGeo = 0;
        }
        public string TextGeo
        {
            get { return textGeo; }
            set { textGeo = value; OnPropertyChanged("TextGeo"); }
        }
        string textGeo;
        public int CountGeo
        {
            get { return countGeo; }
            set { countGeo = value; OnPropertyChanged("CountGeo"); }
        }
        private int countGeo;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public int IndexGeo
        {
            get { return indexGeo; }
            set { indexGeo = value; OnPropertyChanged("IndexGeo"); }
        }
        private int indexGeo;
        public DataBase WorkWithBase
        {
            get { return workWithBase; }
            set { workWithBase = value;OnPropertyChanged("WorkWithBase"); }
        }

        

        private DataBase workWithBase;
        public void TranslateGeo()
        {
            WorkWithBase = new DataBase(Directory.GetCurrentDirectory() + "\\dataBase\\TEMV1.GDB");
            Thread t =new Thread (()=> { WorkWithBase.TranslateGeo(this); });
            t.Start();
        }
    }
}
