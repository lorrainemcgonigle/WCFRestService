using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]//The operations available for the service to consume
    public interface IService1
    {

        [OperationContract]//used to expose a method as a service method and also transaction flow
        [WebInvoke(Method ="GET", ResponseFormat =WebMessageFormat.Json, BodyStyle =WebMessageBodyStyle.Bare, UriTemplate ="GetData/{value}")]
        string GetData(string value);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBook/{bookId}")]
        string GetBook(string bookId);
        [OperationContract]
        string InsertBook(BookDetails newBook);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]//describes the data that needs to be exchanged between provider and consumer
    public class BookDetails
    {
        string isbn = string.Empty;
        string title = string.Empty;
        string author = string.Empty;
        string pages = string.Empty;
        string publisher = string.Empty;

        [DataMember]
        public string Isbn
        {
            get { return isbn; }
            set { isbn = value; }
        }

        [DataMember]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        [DataMember]
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        [DataMember]
        public string Pages
        {
            get { return pages; }
            set { pages = value; }
        }

       
        [DataMember]
        public string Publisher
        {
            get { return publisher; }
            set { publisher = value; }
        }
    }
}
