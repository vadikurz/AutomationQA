using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace FifthTask
{
    [Serializable]
    [XmlType("GroupedByTransmissionType")]
    public class GroupSerializationModel<TKey, TElement>
    {
        public TKey Key { get; set; }
        public List<TElement> Elements { get; set; }

        public GroupSerializationModel() { }

        public GroupSerializationModel(IGrouping<TKey, TElement> group)
        {
            Key = group.Key;
            Elements = group.ToList();
        }

    }
}
