using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Knorr.Core.FileDb
{
    public class FileDB<T> : IRepository<T> where T : class, IModel, new()
    {
        private readonly JsonSerializer _serializer;
        private readonly FileInfo _file;

        public FileDB(string directory)
        {
            _serializer = new JsonSerializer();
            var collectionName = typeof(T).Name;
            collectionName = collectionName.EndsWith("s")
                                 ? collectionName + "es"
                                 : collectionName + "s";

            var directoryInfo = new DirectoryInfo(directory);
            
            _file = new FileInfo(directory + collectionName);
            
        }

        public void Initialize()
        {
            if (!_file.Exists)
                _file.Create().Close();
        }

        public void Create(T model)
        {
            var models = ReadAll();
            if(models.Any(a => a.Id == model.Id))
                throw new ConstraintException("Id already exists");
            Save(models.Union(new[] {model}));
        }

        public IEnumerable<T> Read()
        {
            return ReadAll();
        }

        public void Update(T model)
        {
            var models =  ReadAll()
                .Where(w => w.Id != model.Id)
                .Union(new [] {model});
            Save(models);
        }

        public void Delete(T model)
        {
            var models = ReadAll()
                .Where(w => w.Id != model.Id);
            Save(models);
        }

        public void DeleteAll()
        {
            Save(Enumerable.Empty<T>());
        }

        IEnumerable<T> ReadAll()
        {
            var list = new List<T>();

            using (var sr = _file.OpenText())
            {
                while (true)
                {
                    var json = sr.ReadLine();
                    if (json == string.Empty) continue;
                    if (json == null) break;
                    list.Add(_serializer.To<T>(json));
                }
            }
            return list;
        }
        void Save(IEnumerable<T> models)
        {
            using (var ct = _file.CreateText())
            {
                foreach (var model in models)
                    ct.WriteLine(_serializer.ToJson(model));
            }
        }
    }
}