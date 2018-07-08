using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfDemo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [FaultContract(typeof(ExceptionMessage))]
        List<Book> GetBooks();

        [OperationContract]
        [FaultContract(typeof(ExceptionMessage))]
        void InsertBook(Book newBook);

        [OperationContract]
        [FaultContract(typeof(ExceptionMessage))]
        void DeleteBook(int StudentId);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Book
    {
        [DataMember]
        public int isbn;
        [DataMember]
        public string title;
        [DataMember]
        public string author;
        [DataMember]
        public int pages;
        [DataMember]
        public string publisher;
    }

    [DataContract]
    public class ExceptionMessage
    {
        private string infoExceptionMessage;
        public ExceptionMessage(string Message)
        {
            this.infoExceptionMessage = Message;
        }
        [DataMember]
        public string errorMessageOfAction
        {
            get { return this.infoExceptionMessage; }
            set { this.infoExceptionMessage = value; }
        }
    }
}
