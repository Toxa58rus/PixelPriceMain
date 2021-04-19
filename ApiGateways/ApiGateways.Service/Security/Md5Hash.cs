using System;
using System.Security.Cryptography;
using System.Text;

namespace ApiGateways.Service.Security
{
    public class Md5Hash : IMd5Hash
    {
		public string GetMd5Hash(string value)
        {
            using (var md5 = MD5.Create())
            {
                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
                var sBuilder = new StringBuilder();

                foreach (var s in data) sBuilder.Append(s.ToString("x2"));
                return sBuilder.ToString();
            }
        }

        public bool VerifyMd5Hash(string input, string hash)
        {
            using (var md5 = MD5.Create())
            {
                var hashOfInput = GetMd5Hash(input);
                var comparer = StringComparer.OrdinalIgnoreCase;

                return 0 == comparer.Compare(hashOfInput, hash);
            }
        }

        public string GetMd5Hash(MD5 md5Hash, string value)
        {
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            var sBuilder = new StringBuilder();

            foreach (var s in data) sBuilder.Append(s.ToString("x2"));
            return sBuilder.ToString();
        }
	}
}
