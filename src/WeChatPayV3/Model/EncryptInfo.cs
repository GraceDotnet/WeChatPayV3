using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatPayV3.Utility;

namespace WeChatPayV3.SDK.Model
{
    /// <summary>
    /// 解密信息
    /// </summary>
    public class EncryptInfo
    {
        [JsonProperty(PropertyName = "algorithm")]
        public string Algorithm { get; set; }
        [JsonProperty(PropertyName = "nonce")]
        public string Nonce { get; set; }

        [JsonProperty(PropertyName = "associated_data")]
        public string AssociatedData { get; set; }

        [JsonProperty(PropertyName = "ciphertext")]
        public string CipherText { get; set; }


        /// <summary>
        /// AEAD_AES_256_GCM
        /// https://tools.ietf.org/html/rfc5116#page-15
        /// GCM加密后附加128位TAG形成密文
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte[] Decrypt(string key)
        {
            var data = Convert.FromBase64String(CipherText);
            var cipher = data.AsEnumerable().Take(data.Length - 16).ToArray();
            var tag = data.AsEnumerable().Skip(data.Length - 16).ToArray();

            return AesGcm256Utility.AesGcmDecrypt
                  (AssociatedData, Nonce, CipherText, key);
        }
    }
}
