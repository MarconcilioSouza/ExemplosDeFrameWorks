using Microsoft.Web.Services3.Security.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WSConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            UsernameToken token = new UsernameToken("admin", "123456", PasswordOption.SendPlainText);

            string passwordDigest = GetSHA1String(token.Nonce + token.Created.ToString() + token.Password);
            string tokennamespace = "woss";
            string nonce = Convert.ToBase64String(token.Nonce);

            string texto = string.Format(
                "<{0}:UsernameToken u:Id=\"" + token.Id +
                "\" xmlns:u=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\">" +
                "<{0}:Username>" + token.Username + "</{0}:Username>" +
                "<{0}:Password Type=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#Digest\">" + passwordDigest + "</{0}:Password>" +
                "<{0}:Nonce EncodingType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\">" + nonce + "</{0}:Nonce>" +
                "<u:Created>" + token.Created.ToString() + "</u:Created></{0}:UsernameToken>", tokennamespace);

            Console.WriteLine(texto);
        }

        protected static string GetSHA1String(string phrase)
        {
            SHA1CryptoServiceProvider sha1Hasher = new SHA1CryptoServiceProvider();
            byte[] hashedDataBytes = sha1Hasher.ComputeHash(Encoding.UTF8.GetBytes(phrase));
            return Convert.ToBase64String(hashedDataBytes);
        }
    }
}
