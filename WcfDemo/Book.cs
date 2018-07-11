using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfDemo
{
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
}