using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XBitApi.Utilities
{
	public static class Hashing
	{
		public static string sha256_hash(string value)
		{
			StringBuilder sb = new StringBuilder();

			using (var hash = SHA256.Create())
			{
				Encoding enc = Encoding.UTF8;
				Byte[] result = hash.ComputeHash(enc.GetBytes(value));

				foreach (Byte b in result)
					sb.Append(b.ToString("x2"));
			}

			return sb.ToString();
		}
	}
}
