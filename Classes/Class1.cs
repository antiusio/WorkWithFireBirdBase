using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace Classes
{
    public class Class1
    {
        public void Read()
        {
            string path = Directory.GetCurrentDirectory()+ "\\dataBase\\TEMV1.GDB";
            string str = "";
            using (var connection = new FbConnection("database=localhost:"+ path + ";user=sysdba;password=masterkey"))
            {
                connection.Open();
                //using (var command = new FbCommand("UPDATE GEO_COUNTRIES SET COUNTRY_NAME = 'Азербайджан1' WHERE COUNTRY_ID = '81'", connection))
                //{
                //    var v = command.ExecuteNonQuery();

                //    ;
                //}
                using (var transaction = connection.BeginTransaction())
                {
                    //using (var command = new FbCommand("SELECT DISTINCT RDB$RELATION_NAME FROM RDB$RELATION_FIELDS WHERE RDB$SYSTEM_FLAG = 0; ", connection, transaction))
                    //{
                    //    using (var reader = command.ExecuteReader())
                    //    {
                    //        while (reader.Read())
                    //        {
                    //            var values = new object[reader.FieldCount];
                    //            reader.GetValues(values);
                    //            //Console.WriteLine(string.Join("|", values));
                    //            str += values[0] + "\r\n";
                    //        }
                    //    }
                    //    ;
                    //}
                    using (var command = new FbCommand("select RDB$FIELD_NAME from RDB$RELATION_FIELDS where RDB$RELATION_NAME = 'GEO_REGIONS'", connection, transaction))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var values = new object[reader.FieldCount];
                                reader.GetValues(values);
                                //Console.WriteLine(string.Join("|", values));
                                str += values[0] + "\r\n";
                            }
                        }
                        ;
                    }
                    using (var command = new FbCommand("select * from GEO_REGIONS", connection, transaction))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var values = new object[reader.FieldCount];
                                reader.GetValues(values);
                                //Console.WriteLine(string.Join("|", values));
                                str += values[1] + "\r\n";
                            }
                        }
                        ;
                    }
                    using (var command = new FbCommand("select COUNTRY_ID, COUNTRY_NAME from GEO_COUNTRIES where COUNTRY_ID = '81'", connection, transaction))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var values = new object[reader.FieldCount];
                                reader.GetValues(values);
                                //Console.WriteLine(string.Join("|", values));
                                str += values[1] + "\r\n";
                            }
                        }
                        ;
                    }
                    //UPDATE GEO_COUNTRIES SET COUNTRY_NAME = 'Азербайджан1' WHERE COUNTRY_ID = '81';
                    using (var command = new FbCommand("UPDATE GEO_COUNTRIES SET COUNTRY_NAME = 'Азербайджан1' WHERE COUNTRY_ID = '81'", connection, transaction))
                    {
                        var v = command.ExecuteNonQuery();
                        
                        ;
                    }
                    
                    transaction.Save("save_night");
                }
            }
        }
    }
}
