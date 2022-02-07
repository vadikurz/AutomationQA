using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace FifthTask.Serialization
{
    [Serializable]
    [XmlType("GroupedByTransmissionType")]
    public class GroupSerializationModel<TKey, TElement>
    {
        public TKey Key { get; set; }

        public List<TElement> Elements { get; set; }

        private GroupSerializationModel() { }

        public GroupSerializationModel(IGrouping<TKey, TElement> group)
        {
            Key = group.Key;
            Elements = group.ToList();
        }
    }
}
