using System.Security.Cryptography;

namespace MyoFibril.WebAPI.Common.Utils.Jwt;

public static class JwtUtils
{
    public static RSACryptoServiceProvider CreatePublicRSAProviderFromPem(string pemKey)
    {
        RSA rsa = RSA.Create();
        rsa.ImportFromPem(pemKey);
        RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
        rsaProvider.ImportParameters(rsa.ExportParameters(false));
        return rsaProvider;
    }
    public static RSACryptoServiceProvider CreatePrivateRSAProviderFromPem(string pemKey)
    {
        RSA rsa = RSA.Create();
        rsa.ImportFromPem(pemKey);
        RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
        rsaProvider.ImportParameters(rsa.ExportParameters(true));
        return rsaProvider;
    }
}
