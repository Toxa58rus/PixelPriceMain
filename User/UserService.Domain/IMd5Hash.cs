﻿using System.Security.Cryptography;

namespace UserService.Domain
{
    public interface IMd5Hash
    {
        string GetMd5Hash(string value);
        string GetMd5Hash(MD5 md5Hash, string value);
        bool VerifyMd5Hash(string value, string hash);
    }
}
