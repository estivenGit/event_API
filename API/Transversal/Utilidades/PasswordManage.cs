using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Transversal.Models;

namespace Transversal.Utilidades
{
    public class PasswordManage : IPasswordManage
    {
        public object Base64CryptoMethod { get; private set; }
        private readonly string key = "<3mMHk|x9frVio||A";

        public bool VerificarContraseña(string contrasenaIngresada, string contrasenaAlmacenada)
        {
            return Verify(contrasenaIngresada, contrasenaAlmacenada);
        }

        public string encriptarContraseña(string Password)
        {
            return Encrypt(Password, key);
        }



        public string Encrypt(string message, string publicKey)
        {
            // Generar una clave y un IV utilizando la clave pública
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = new PasswordDeriveBytes(publicKey, null).GetBytes(32); // Clave de 32 bytes
                aesAlg.IV = new PasswordDeriveBytes(publicKey, null).GetBytes(16); // IV de 16 bytes

                // Crear el cifrador
                using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    // Convertir el mensaje a bytes
                    byte[] plainText = Encoding.UTF8.GetBytes(message);

                    // Cifrar el mensaje
                    byte[] cipherText = encryptor.TransformFinalBlock(plainText, 0, plainText.Length);

                    // Convertir el resultado a base64 para facilitar su almacenamiento y transmisión
                    return Convert.ToBase64String(cipherText);
                }
            }
        }

        // Método de verificación
        public bool Verify(string contrasenaIngresada, string contrasenaAlmacenada)
        {
            // Cifrar el mensaje original
            string encryptedMessage = Encrypt(contrasenaIngresada, key);

            // Verificar si el mensaje original es igual al mensaje descifrado
            return encryptedMessage == contrasenaAlmacenada;
        }


        /// <summary>
        /// Encrypts the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="publicKey">The public key.</param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, string publicKey)
        {
            // Obtener la clave y el IV desde la misma clave pública usada en la encriptación
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = new PasswordDeriveBytes(publicKey, null).GetBytes(32); // Clave de 32 bytes
                aesAlg.IV = new PasswordDeriveBytes(publicKey, null).GetBytes(16); // IV de 16 bytes

                // Crear el desencriptador
                using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                {
                    // Convertir el texto cifrado de base64 a bytes
                    byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                    // Decodificar el mensaje
                    byte[] decrypted = decryptor.TransformFinalBlock(cipherTextBytes, 0, cipherTextBytes.Length);

                    // Convertir los bytes del mensaje original a string
                    return Encoding.UTF8.GetString(decrypted);
                }
            }
        }

       
    }
}
