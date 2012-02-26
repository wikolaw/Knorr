using System.Collections.Generic;
using Knorr.Core.ViewModels;
using System.Linq;

namespace Knorr.Core
{
    public static class NodeFactory
    {
        public static IEnumerable<Node> CreateNodes(IEnumerable<QlikViewFile> qlikViewFiles )
        {
            var files =  qlikViewFiles
                .Select(s => new Node {Id = s.Id, Text = s.FileInfo.Name});

            var sources = qlikViewFiles
                .Where(w => w.Script.Sources != null)
                .SelectMany(s => s.Script.Sources)
                .Select(s => new Node {Id = s, Text = s});

            var targets = qlikViewFiles
                .Where(w => w.Script.Targets != null)
                .SelectMany(s => s.Script.Targets)
                .Select(s => new Node { Id = s, Text = s });

            return GetDistinct(files.Union(sources).Union(targets).ToList());

        }

        static IEnumerable<Node> GetDistinct (IEnumerable<Node> nodes)
        {
            var list = new List<Node>();

            foreach(var node in nodes)
            {
                if(list.Exists(n => n.Id == node.Id)) continue;
                list.Add(node);
            }
            return list;
        }

        public static IEnumerable<Edge> CreateEdges(IEnumerable<QlikViewFile> qlikViewFiles)
        {
            var edgesFromSources = new List<Edge>();
            foreach (var qlikViewFile in qlikViewFiles)
            {
                if(qlikViewFile.Script.Sources == null) continue;

                foreach(var source in qlikViewFile.Script.Sources)
                    edgesFromSources.Add(new Edge{ConnectFromId = source, ConnectToId = qlikViewFile.Id});
            }

            var edgesFromTargets = new List<Edge>();
            foreach (var qlikViewFile in qlikViewFiles)
            {
                if(qlikViewFile.Script.Targets == null) continue;

                foreach(var target in qlikViewFile.Script.Targets)
                    edgesFromTargets.Add(new Edge{ConnectFromId = qlikViewFile.Id,ConnectToId = target});
            }

            return edgesFromSources.Union(edgesFromTargets);
        }
    }
}
