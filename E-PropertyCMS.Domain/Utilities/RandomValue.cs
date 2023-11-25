using System;
namespace E_PropertyCMS.Domain.Utilities
{
	public static class RandomValue
	{
		public static string RandomString(int length)
		{
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            char[] value = new char[length];
            for (int i = 0; i < length; i++)
            {
                value[i] = allowedChars[random.Next(allowedChars.Length)];
            }

            return new string(value);
        }
	}
}

