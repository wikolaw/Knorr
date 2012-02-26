using System.IO;
using System.Runtime.Serialization;
using Knorr.Core.FileDb;

namespace Knorr.Core
{
    [DataContract]
    public class QlikViewFile: IModel
    {
        [DataMember]
        public Script Script { get; set; }

        [DataMember]
        public FileInfo FileInfo { get; set; }

        [DataMember]
        public string Id { get; set; }
    }
}