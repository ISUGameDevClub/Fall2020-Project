using UnityEngine;
using System.Collections;
using System;
using System.Security.Cryptography;
using System.IO;
/**
* @Author Jake Botka
* A class to encrypt dynamic data files
*/
public class TenebrisDataEncryptor
{
    /**
     * Encrypts data
     * @Param payload
     */
    public static byte[] EncryptPayload(byte[] payload)
    {
        try
        {
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {

                myRijndael.GenerateKey();
                myRijndael.GenerateIV();

                byte[] encrypted = Encrypt(payload, myRijndael.Key, myRijndael.IV);

                // Decrypt the bytes to a string. 
                //string roundtrip = DecryptStringFromBytes(encrypted, myRijndael.Key, myRijndael.IV);

                //Display the original data and the decrypted data.
                Console.WriteLine("Payload encrypted");
                return encrypted;
                // Console.WriteLine("Round Trip: {0}", roundtrip);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: {0}", e.Message);
            Debug.LogError("Error: " + e.Message);
            return null;
           
        }

        return null;

    }

    /**
     * Encrypts payload with key and initalized vector using AES
     */
    private static byte[] Encrypt(byte[] payload, byte[] Key, byte[] IV)
    {

        if (Key != null && IV != null && payload != null)
        {
            if (payload.Length > 0 && IV.Length > 0 && Key.Length > 0)
            {

                // Create an RijndaelManaged object 
                // with the specified key and IV. 
                using (RijndaelManaged rijAlg = new RijndaelManaged())
                {
                    rijAlg.Key = Key;
                    rijAlg.IV = IV;

                    // Create a decryptor to perform the stream transform.
                    ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                    // Create the streams used for encryption. 
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {

                                //Write all data to the stream.
                                swEncrypt.Write(payload);
                            }
                            return msEncrypt.ToArray();
                        }
                    }
                }
            }
        }

        return null;
    }



}
