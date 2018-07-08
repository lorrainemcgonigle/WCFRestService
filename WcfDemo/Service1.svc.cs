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
        public static String ConnectionString = ConfigurationManager.ConnectionStrings["MyDbConn"].ConnectionString;
        public List<Book> GetBooks()
        {
            List<Book> BookList = new List<Book>();
            DataTable resourceTable = new DataTable();
            SqlDataReader resultReader = null;
            SqlConnection connection = new SqlConnection(ConnectionString);
            //GetBooks is the stored procedure from the database
            SqlCommand command = new SqlCommand("GetBooks", connection);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                connection.Open();
                resultReader = command.ExecuteReader();
                resourceTable.Load(resultReader);
                resultReader.Close();
                connection.Close();
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
