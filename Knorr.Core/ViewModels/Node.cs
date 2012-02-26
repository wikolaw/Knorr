using System.Runtime.Serialization;
using Knorr.Core.FileDb;

namespace Knorr.Core.ViewModels
{
    [DataContract]
    public class Node : IModel
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Text { get; set; }
    }
}
