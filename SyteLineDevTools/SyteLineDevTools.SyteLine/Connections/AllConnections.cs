using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SyteLineDevTools.SyteLine.Connections
{
    public class AllConnections : Dictionary<string, IConnection>
    {
        public AllConnections() : base()
        {

        }
        public AllConnections(IEqualityComparer<string> comparer) : base (comparer)
        {

        }
    }
    /// <summary>
    /// We are using AES FIPS complaint encryption
    /// We need a connections file that is encrypted... yet the key cannot be in this app otherwise anyone can use the file and hack each other's work on other computers.
    /// So there is a key file that goes into the users/<user>/appdata/roaming/SyteLineDevTools/key.data
    /// If this file doesn't exist create the file and then use that key to encrypt the connections file.
    /// </summary>
    public static class AllConnectionsExtensions
    {
        private static byte[] IVKey = new byte[] { 0x22, 0x70, 0x29, 0x80, 0x12, 0x04, 0x80, 0x09, 0x25, 0x79, 0x04, 0x18, 0x97, 0x07, 0x31, 0x99, 0x06, 0x20, 0x02, 0x01, 0x14, 0x11 };
        public static async Task LoadConnections(this AllConnections connections)
        {
            await Task.Run(() =>
            {
                var path = GetStartupDirectory();
                var connectionFile = System.IO.Path.Combine(path, "connections.data");
                if (File.Exists(connectionFile))
                {
                    var fileData = File.ReadAllText(connectionFile);
                    var key = GetEncryptionKey();
                    var decryptText = DecryptFile(fileData, key);
                    //Deserialize
                    connections.Clear();
                    DeserializeConnections(decryptText);
                }
            });
        }
        public static async Task SaveConnection(this AllConnections connections)
        {
            await Task.Run(() =>
            {
                var path = GetStartupDirectory();
                var connectionFile = System.IO.Path.Combine(path, "connections.data");
                var key = GetEncryptionKey();
                //Serialize
                var decryptText = SerializeConnections(connections);
                var encryptText = EncryptFile(decryptText, key);
                File.WriteAllText(connectionFile, encryptText);
            });
        }
        private static string GetStartupDirectory()
        {
            var entryFile = new System.IO.FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
            return entryFile.Directory.FullName;
        }
        private static string GetEncryptionKey()
        {
            var dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SyteLineDevTools");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            var filename = Path.Combine(dirPath, "key.data");
            if (File.Exists(filename))
            {
                return File.ReadAllText(filename);                
            } 

            //doesn't exist

            //create value
            Random random = new Random();
            byte[] buffer = { };
            byte[] key = new byte[128];
            for (int i = 0; i < 128; i++)
            {
                random.NextBytes(buffer);
                key[i] = buffer[0];
            }

            //save file
            var newKey = Convert.ToBase64String(key);
            File.WriteAllText(filename, newKey);
            return newKey;
        }
        private static string EncryptFile(string inputFile, string skey)
        {
            string outputFile = String.Empty;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
                {
                    using (ICryptoTransform encryptor = aes.CreateEncryptor(key, IVKey))
                    {
                        using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                            {
                                int data;
                                while ((data = fsIn.ReadByte()) != -1)
                                {
                                    cs.WriteByte((byte)data);
                                }
                            }
                        }
                    }
                }
            }
            return outputFile;
        }
        private static string DecryptFile(string inputFile, string skey)
        {
            string outputFile = String.Empty;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                {
                    using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                    {
                        using (ICryptoTransform decryptor = aes.CreateDecryptor(key, IVKey))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                            {
                                int data;
                                while ((data = cs.ReadByte()) != -1)
                                {
                                    fsOut.WriteByte((byte)data);
                                }
                            }
                        }
                    }
                }
            }
            return outputFile;
        }
        private static string SerializeConnections(AllConnections connections)
        {
            JObject document = new JObject();
            JArray items = new JArray();
            foreach (var key in connections.Keys)
            {
                var obj = new JObject();
                obj.Add(key);
                var objDetail = JObject.FromObject(connections[key]);
                obj.Add(objDetail);
                items.Add(obj);
            }
            document.Add(items);
            return document.ToString();
        }
        private static void DeserializeConnections(string text)
        {

        }
    }
}
