using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    //This is the main file for the WCF services. Inherits the IService interface and implements all
    //of the methods of the operation contracts
    public class Service1 : IService1
    {
        string connection_string = ConfigurationManager.ConnectionStrings["BooksConnectionString"].ConnectionString.ToString();
        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string GetBook(string bookId)
        {
            return string.Format("book");
        }
        public string InsertBook(BookDetails newBook)
        {
            string message;
            SqlConnection conn = new SqlConnection(connection_string);
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT into BookDetails(Isbn, Title, Author, Pages, Publisher) VALUES(@Isbn, @Title, @Author, @Pages, @Publisher)", conn);
            cmd.Parameters.Add("@Isbn", System.Data.SqlDbType.VarChar).Value = newBook.Isbn;
            cmd.Parameters.Add("@Title", System.Data.SqlDbType.VarChar).Value = newBook.Title;
            cmd.Parameters.Add("@Author", System.Data.SqlDbType.VarChar).Value = newBook.Author;
            cmd.Parameters.Add("@Pages", System.Data.SqlDbType.VarChar).Value = newBook.Pages;
            cmd.Parameters.Add("@Publisher", System.Data.SqlDbType.VarChar).Value = newBook.Publisher;
            int result = cmd.ExecuteNonQuery();
            if(result == 1)
            {
                message = newBook.Title + " entered successfully";
            }
            else
            {
                message = newBook.Title + " not entered successfully";
            }
            conn.Close();
            return message;
        }
    }
}
