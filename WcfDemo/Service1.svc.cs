using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfDemo
{
    public class Service1 : IService1
    {
        //create the connection string
        public static String ConnectionString = ConfigurationManager.ConnectionStrings["MyDbConn"].ConnectionString;
        public List<Book> GetBooks()
        {
            List<Book> BookList = new List<Book>();
            DataTable resourceTable = new DataTable();
            //datareader object is used to hold the data specified by the query
            SqlDataReader resultReader = null;
            //create the connection with the db
            SqlConnection connection = new SqlConnection(ConnectionString);
            //GetBooks is the stored procedure from the database
            //sqlCommand is used to perform operations of reading and writing 
            SqlCommand command = new SqlCommand("GetBooks", connection);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                //open the connection
                connection.Open();
                //execute the datareader command to fetch the rows
                //ExecuteReader returns an object to iterate over
                resultReader = command.ExecuteReader();
                resourceTable.Load(resultReader);
                resultReader.Close();
                //close the connection
                connection.Close();
                //put the details into our booklist
                BookList = (from DataRow dr in resourceTable.Rows
                               select new Book()
                               {
                                   isbn = Convert.ToInt32(dr["isbn"]),
                                   title = dr["title"].ToString(),
                                   author = dr["author"].ToString(),
                                   pages = Convert.ToInt32(dr["pages"]),
                                   publisher = dr["publisher"].ToString()
                               }).ToList();
            }
            catch (Exception exception)
            {
                if (resultReader != null || connection.State == ConnectionState.Open)
                {
                    resultReader.Close();
                    connection.Close();
                }
                throw new FaultException<ExceptionMessage>(new ExceptionMessage(exception.Message));
            }
            //return the list
            return BookList;
        }

        public void InsertBook(Book newBook)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertBook", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@isbn", newBook.isbn));
                    command.Parameters.Add(new SqlParameter("@title", newBook.title));
                    command.Parameters.Add(new SqlParameter("@author", newBook.author));
                    command.Parameters.Add(new SqlParameter("@pages", newBook.pages));
                    command.Parameters.Add(new SqlParameter("@publisher", newBook.publisher));
                    //ExecuteScaler can be used when only one value is being returned
                    object result = command.ExecuteScalar();
                    connection.Close();
                }
            }
            catch (SqlException exception)
            {
                throw new FaultException<ExceptionMessage>(new ExceptionMessage(exception.Message));
            }
        }

        public void DeleteBook(int isbn)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DeleteBook", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@isbn", isbn));
                    //ExecuteNonQuery doesnt return data
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException exception)
            {
                throw new FaultException<ExceptionMessage>(new ExceptionMessage(exception.Message));
            }
        }
    }
}
