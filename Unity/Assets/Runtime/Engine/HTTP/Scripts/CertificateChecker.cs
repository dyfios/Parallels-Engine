using System.Security.Cryptography.X509Certificates;
using UnityEngine.Networking;

namespace FiveSQD.Parallels.HTTP
{
    public class CertificateChecker : CertificateHandler
    {
        private string publicKey;

        public CertificateChecker(string pubKey)
        {
            publicKey = pubKey;
        }

        protected override bool ValidateCertificate(byte[] certificateData)
        {
            X509Certificate2 certificate = new X509Certificate2(certificateData);
            string pk = certificate.GetPublicKeyString();
            if (pk.ToLower().Equals(publicKey.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}