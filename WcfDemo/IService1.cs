using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfDemo
{
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
}
