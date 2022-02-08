using System;
using System.IO;
using System.Xml.Serialization;

namespace FifthTask.Serialization
{
    public class ViewSerializer<T>
    {
        public Func<T, object> View { get; init; }

        public string Path { get; init; }

        public void Execute(T data)
        {
            var view = View.Invoke(data);
            XmlSerializer serializer = new XmlSerializer(view.GetType());
            using FileStream fileStream = new FileStream(Path, FileMode.OpenOrCreate);
            serializer.Serialize(fileStream, view);
        }
    }
}
