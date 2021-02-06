using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Clocks.Desktop.Tools.Managers
{
    [Serializable]
    internal static class SerializationManager
    {
        private const string FilePath = "current.user";

        internal static void Serialize<TObject>(TObject obj)
        {
            try
            {
                FileFolderHelper.CheckAndCreateFile(FilePath);
                var formatter = new BinaryFormatter();
                using var stream = new FileStream(FilePath, FileMode.Create);
                formatter.Serialize(stream, obj);
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to serialize data to file {FilePath}", ex);
                throw;
            }
        }

        internal static TObject Deserialize<TObject>() where TObject : class
        {
            try
            {
                var formatter = new BinaryFormatter();
                using var stream = new FileStream(FilePath, FileMode.Open);
                return (TObject)formatter.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to Deserialize Data From File {FilePath}", ex);
                return null;
            }
        }

        internal static void DeleteSerialization()
        {
            try
            {
                File.Delete(FilePath);
            }
            catch
            {
                // ignored
            }
        }
    }
}
