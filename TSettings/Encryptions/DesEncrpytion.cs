using System.Security.Cryptography;
using System.Text;

namespace TSettings.Encryptions
{
    public class DesEncrpytion : IEncrpytion
    {
        private readonly DESCryptoServiceProvider _cryptic;

        public DesEncrpytion(string key, string iv)
        {
            _cryptic = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(key),
                IV = Encoding.ASCII.GetBytes(iv)
            };

        }
        public ICryptoTransform CreateDecryptor()
        {
            return _cryptic.CreateDecryptor();
        }

        public ICryptoTransform CreateEncryptor()
        {
            return _cryptic.CreateEncryptor();
        }
    }
}
