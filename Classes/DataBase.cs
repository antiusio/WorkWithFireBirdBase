using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace Classes
{
    public interface MainDataContextInterface
    {
        string TextGeo { get; set; }
        int CountGeo { get; set; }
        int IndexGeo { get; set; }
    }
    public class DataBase
    {
        public string Path;
        public string Host;
        public DataBase(string path,string host="localhost")
        {
            this.Path = path;
            this.Host = host;
        }
        public void TranslateGeo(MainDataContextInterface mainDataContext)
        {
            //TranslateGeo(mainDataContext.CountGeo,mainDataContext.IndexGeo,mainDataContext.TextGeo);
            mainDataContext.CountGeo = 0;
            mainDataContext.IndexGeo = 0;
            mainDataContext.TextGeo = "";
            using (var connection = new FbConnection("database=" + Host + ":" + Path + ";user=sysdba;password=masterkey"))
            {
                connection.Open();
                var command1 = new FbCommand("select count(*) from GEO_REGIONS", connection);
                using (var command = new FbCommand("select count(*) from GEO_REGIONS", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var values = new object[reader.FieldCount];
                            reader.GetValues(values);
                            //Console.WriteLine(string.Join("|", values));
                            mainDataContext.CountGeo = Convert.ToInt32(values[0]);
                        }
                    }
                }
                BingTranslator bingTranslator = new BingTranslator();
                using (var command = new FbCommand("select GEO_NAME, ID from GEO", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        mainDataContext.IndexGeo = 0;
                        while (reader.Read())
                        {

                            var values = new object[reader.FieldCount];
                            reader.GetValues(values);

                            mainDataContext.IndexGeo++;
                            mainDataContext.TextGeo = values[0] + "";
                            string ukrText = bingTranslator.Translate(mainDataContext.TextGeo).Text;
                            mainDataContext.TextGeo = mainDataContext.TextGeo + " - " + ukrText;
                            using (var command2 = new FbCommand("UPDATE GEO SET GEO_NAME = '"+ukrText+"' WHERE ID = '" + values[1] + "'", connection))
                            {
                                   var v = command2.ExecuteNonQuery();

                                 ;
                            }
                                
                            //Console.WriteLine(string.Join("|", values));
                            //string str = values[0] + "\r\n";
                        }
                    }
                }
            }
        }
        

    }
}
