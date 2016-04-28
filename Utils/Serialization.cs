using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using HCI_2016_Project.DataClasses;

namespace HCI_2016_Project.Utils
{
    public class Serialization
    {
        private const String MANIFESTATIONS_FILENAME = "data.manifestations.dmg";
        private const String MANIFESTATION_TYPES_FILENAME = "data.manifestation_types.dmg";
        private const String TAGS_FILENAME = "data.tags.dmg";

        #region Method for Manifestations serialization
        public static void SerializeManifestations()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                stream = File.Open(MANIFESTATIONS_FILENAME, FileMode.OpenOrCreate);
                formatter.Serialize(stream, AppData.GetInstance().Manifestations);
            }
            catch {}
            finally
            {
                if (stream != null) stream.Dispose();
            }
        }
        #endregion

        #region Method for Manifestations deserialization
        public static void DeserializeManifestations()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            if (File.Exists(MANIFESTATIONS_FILENAME))
            {
                try
                {
                    stream = File.Open(MANIFESTATIONS_FILENAME, FileMode.Open);
                    AppData.GetInstance().Manifestations = (List<Manifestation>)formatter.Deserialize(stream);
                }
                catch {}
                finally
                {
                    if (stream != null) stream.Dispose();
                }
            }
            else
            {
                AppData.GetInstance().Manifestations = new List<Manifestation>();
            }
        }
        #endregion

        #region Method for Manifestation Types serialization
        public static void SerializeManifestationTypes()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                stream = File.Open(MANIFESTATION_TYPES_FILENAME, FileMode.OpenOrCreate);
                formatter.Serialize(stream, AppData.GetInstance().ManifestationTypes);
            }
            catch { }
            finally
            {
                if (stream != null) stream.Dispose();
            }
        }
        #endregion

        #region Method for Manifestation Types deserialization
        public static void DeserializeManifestationTypes()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            if (File.Exists(MANIFESTATION_TYPES_FILENAME))
            {
                try
                {
                    stream = File.Open(MANIFESTATION_TYPES_FILENAME, FileMode.Open);
                    AppData.GetInstance().ManifestationTypes = (List<ManifestationType>)formatter.Deserialize(stream);
                }
                catch { }
                finally
                {
                    if (stream != null) stream.Dispose();
                }
            }
            else
            {
                AppData.GetInstance().ManifestationTypes = new List<ManifestationType>();
            }
        }
        #endregion

        #region Method for Tags serialization
        public static void SerializeTags()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                stream = File.Open(TAGS_FILENAME, FileMode.OpenOrCreate);
                formatter.Serialize(stream, AppData.GetInstance().Tags);
            }
            catch { }
            finally
            {
                if (stream != null) stream.Dispose();
            }
        }
        #endregion

        #region Method for Tags deserialization
        public static void DeserializeTags()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            if (File.Exists(TAGS_FILENAME))
            {
                try
                {
                    stream = File.Open(TAGS_FILENAME, FileMode.Open);
                    AppData.GetInstance().Tags = (List<Tag>)formatter.Deserialize(stream);
                }
                catch { }
                finally
                {
                    if (stream != null) stream.Dispose();
                }
            }
            else
            {
                AppData.GetInstance().Tags = new List<Tag>();
            }
        }
        #endregion
    }
}
