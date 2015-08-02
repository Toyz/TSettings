using System.Security.Cryptography;

namespace TSettings.Encryptions
{
    public interface IEncrpytion
    {
        ICryptoTransform CreateDecryptor();
        ICryptoTransform CreateEncryptor();
    }
}
