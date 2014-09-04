using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Odbc;


namespace InfojectiveReports
{
    public class DBConnHelper
    {
        private string _connection;
        private string _connType;
        private List<IParameter> _parameters;

        public DBConnHelper(string conn, List<IParameter> parameters, string connType = "default")
        {
            _parameters = parameters;
            if (conn.Contains(";"))
            {
                _connection = conn;
                _connType = connType;
            }
            else
            {
                _connection = ConfigurationManager.ConnectionStrings[conn].ConnectionString;
                if (_connection.StartsWith("DSN="))
                {
                    _connType = "DSN";
                }
                else
                {
                    _connType = connType;
                }
               
            }
            
        }

        
        
        public DataTable GetDataTableSQL(string sql)
        {
            //   string sql = this.sproc_name;
            //   string conn = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString;
            DataTable myTable = new DataTable();
            try
            {
                

                if (_connType == "default")
                {
                    using (SqlConnection myConnection = new SqlConnection(_connection))
                    {
                        using (SqlCommand myCommand = new SqlCommand(sql, myConnection))
                        {

                            if (sql.ToLower().StartsWith("select"))
                            {
                                myCommand.CommandType = CommandType.Text;
                            }
                            else
                            {
                                myCommand.CommandType = CommandType.StoredProcedure;
                                foreach (var item in _parameters)
                                {
                                    if (item.ParameterType == null)
                                    {
                                        try
                                        {
                                            int.Parse(item.Value);
                                            item.ParameterType = "int";
                                        }
                                        catch
                                        {
                                            item.ParameterType = "string";
                                        }
                                        
                                    }
                                   
                                    
                                    if (item.ParameterType.ToLower() == "int") 
                                    {
                                        myCommand.Parameters.Add(item.Name, SqlDbType.Int).Value = item.Value;
                                    }
                                    if (item.ParameterType.ToLower() == "string")
                                    {
                                        myCommand.Parameters.Add(item.Name, SqlDbType.VarChar).Value = item.Value;
                                    }
                                    
                                }
                                

                            }

                            // myCommand.Parameters.AddWithValue("@report_id", report_id);
                            myConnection.Open();
                            using (SqlDataReader myReader = myCommand.ExecuteReader())
                            {

                                myTable.Load(myReader);
                                myConnection.Close();
                                //columns = myTable.Rows.Count;

                            }

                        }
                    }
                }
                if (_connType == "DSN")
                {
                    using (OdbcConnection myConnection = new OdbcConnection(_connection))
                    {
                        using (OdbcCommand myCommand = new OdbcCommand(sql, myConnection))
                        {
                            if (sql.StartsWith("select"))
                            {
                                myCommand.CommandType = CommandType.Text;
                            }
                            else
                            {
                                myCommand.CommandType = CommandType.StoredProcedure;
                            }

                            // myCommand.Parameters.AddWithValue("@report_id", report_id);
                            myConnection.Open();
                            using (OdbcDataReader myReader = myCommand.ExecuteReader())
                            {

                                myTable.Load(myReader);
                                myConnection.Close();
                                //columns = myTable.Rows.Count;
                                return myTable;
                            }

                        }
                    }

                }
                return myTable;
            }
            catch (Exception)
            {
                //return myTable;
                throw;
                
            }
            
        }
    }
}
