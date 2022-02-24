using System;
using BCrypt.Net;

namespace _0effort_crm_api.Utility
{
	public class Hashing
	{
		public Hashing()
		{
			
		}

		public virtual string HashPassword(string password)
        {
			return BCrypt.Net.BCrypt.HashPassword(password);		
        }

		public Boolean VerifyPassword(string password, string hashedPassword)
        {
			return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }


	}
}

