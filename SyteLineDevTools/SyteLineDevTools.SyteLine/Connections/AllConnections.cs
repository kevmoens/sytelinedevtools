using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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
        public static void LoadConnections(this AllConnections connections)
        {
            var path = GetStartupDirectory();
            var connectionFile = System.IO.Path.Combine(path, "connections.data");

        }
        public static void SaveConnection(this AllConnections connections)
        {

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
                //Read File
                //return value
            } 

            //doesn't exist
            //create value
            //save file
            //return value
        }
        private static void EncryptFile(string inputFile, string outputFile, string skey)
        {
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

        }
        private static void DecryptFile(string inputFile, string outputFile, string skey)
        {
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
        }
    }
}
