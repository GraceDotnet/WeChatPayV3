using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Text;

namespace WeChatPayV3.Utility
{
    public class AesGcm256Utility
    {
        /// <summary>
        /// AesGcm256解密
        /// </summary>
        /// <see cref="https://pay.weixin.qq.com/wiki/doc/apiv3/wechatpay/wechatpay4_2.shtml"/>
        /// <param name="associatedData"></param>
        /// <param name="nonce"></param>
        /// <param name="ciphertext"></param>
        /// <param name="aesKey"></param>
        /// <returns></returns>
        public static byte[] AesGcmDecrypt(string associatedData, string nonce, string ciphertext, string aesKey)
        {
            GcmBlockCipher gcmBlockCipher = new GcmBlockCipher(new AesEngine());
            AeadParameters aeadParameters = new AeadParameters(
                new KeyParameter(Encoding.UTF8.GetBytes(aesKey)),
                128,
                Encoding.UTF8.GetBytes(nonce),
                Encoding.UTF8.GetBytes(associatedData));
            gcmBlockCipher.Init(false, aeadParameters);

            byte[] data = Convert.FromBase64String(ciphertext);
            byte[] plaintext = new byte[gcmBlockCipher.GetOutputSize(data.Length)];
            int length = gcmBlockCipher.ProcessBytes(data, 0, data.Length, plaintext, 0);
            gcmBlockCipher.DoFinal(plaintext, length);
            return plaintext;
        }
    }

}
