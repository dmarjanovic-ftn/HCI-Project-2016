using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.IO;
using HCI_2016_Project.DataClasses;

namespace HCI_2016_Project.Utils
{
    public class Serialization
    {
        private const String MANIFESTATIONS_FILENAME = "data.manifestations.xml";
        private const String MANIFESTATION_TYPES_FILENAME = "data.manifestation_types.xml";
        private const String TAGS_FILENAME = "data.tags.xml";

        #region Method for Manifestations serialization
        public static void SerializeManifestations()
        {
            var serializer = new XmlSerializer(typeof(List<Manifestation>));
            using (var stream = File.OpenWrite(MANIFESTATIONS_FILENAME))
            {
                serializer.Serialize(stream, AppData.GetInstance().Manifestations);
            }
        }
        #endregion

        #region Method for Manifestations deserialization
        public static void DeserializeManifestations()
        {
            var serializer = new XmlSerializer(typeof(List<Manifestation>));
            using (var stream = File.OpenRead(MANIFESTATIONS_FILENAME))
            {
                var manifestations = (List<Manifestation>)(serializer.Deserialize(stream));
                AppData.GetInstance().Manifestations = manifestations;
            }
        }
        #endregion

        #region Method for Manifestation Types serialization
        public static void SerializeManifestationTypes()
        {
            var serializer = new XmlSerializer(typeof(List<ManifestationType>));
            using (var stream = File.OpenWrite(MANIFESTATION_TYPES_FILENAME))
            {
                serializer.Serialize(stream, AppData.GetInstance().ManifestationTypes);
            }
        }
        #endregion

        #region Method for Manifestation Types deserialization
        public static void DeserializeManifestationTypes()
        {
            var serializer = new XmlSerializer(typeof(List<ManifestationType>));
            using (var stream = File.OpenRead(MANIFESTATION_TYPES_FILENAME))
            {
                var types = (List<ManifestationType>)(serializer.Deserialize(stream));
                AppData.GetInstance().ManifestationTypes = types;
            }
        }
        #endregion

        #region Method for Tags serialization
        public static void SerializeTags()
        {
            var serializer = new XmlSerializer(typeof(List<Tag>));
            using (var stream = File.OpenWrite(TAGS_FILENAME))
            {
                serializer.Serialize(stream, AppData.GetInstance().Tags);
            }
        }
        #endregion

        #region Method for Tags deserialization
        public static void DeserializeTags()
        {
            var serializer = new XmlSerializer(typeof(List<Tag>));
            using (var stream = File.OpenRead(TAGS_FILENAME))
            {
                var tags = (List<Tag>)(serializer.Deserialize(stream));
                AppData.GetInstance().Tags = tags;
            }
        }
        #endregion
    }
}
