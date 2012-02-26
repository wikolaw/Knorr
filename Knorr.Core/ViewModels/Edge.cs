using System;
using System.Runtime.Serialization;
using Knorr.Core.FileDb;

namespace Knorr.Core.ViewModels
{
    [DataContract]
    public class Edge : IModel
    {
        [DataMember]
        public string ConnectFromId { get; set; }
        [DataMember]
        public string ConnectToId { get; set; }
        [DataMember]
        public string Text { get; set; }

        private string _id;

        [DataMember]
        public string Id
        {
            get { return _id ?? (_id = Guid.NewGuid().ToString()); }
            set { _id = value; }
        }
    }
}
