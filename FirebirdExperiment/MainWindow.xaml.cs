using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Classes;

namespace FirebirdExperiment
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainDataContext mainDataContext;
        public MainWindow()
        {

            mainDataContext = new MainDataContext();
            this.DataContext = mainDataContext;
            //DataBase d = new DataBase(Directory.GetCurrentDirectory() + "\\dataBase\\TEMV1.GDB");
            //int count=0;
            //int index=0;
            //string text="";
            //Thread t= new Thread( ()=> { d.TranslateGeo(count, index, text); });
            //t.Start();
            //;
            //Thread.Sleep(TimeSpan.FromSeconds(2));
            //;
            //Thread.Sleep(TimeSpan.FromSeconds(5));
            //;
            //Thread.Sleep(TimeSpan.FromSeconds(2));
            //;
            //BingTranslator b = new BingTranslator();
            //string str = b.Translate("Украина").Text;
            //Translator t = new Translator();
            //Directory.GetCurrentDirectory() + "\\dataBase\\TEMV1.GDB"
            ;
            //Class1 c = new Class1();
            //c.Read();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainDataContext.TranslateGeo();
        }
    }
}
