using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Authentication
{
	public interface IUserData
	{
		ExchangeVersion Version { get; }
		string EmailAddress { get; }
		SecureString SecurePassword { get; }
		Uri AutodiscoverUrl { get; set; }
		string Password {get;}
	}

	public class UserData : IUserData
	{
		private static UserData userData;

		public static IUserData CreateUserData(string emailaddress)
		{
			if (userData == null)
			{
				GetUserData(emailaddress);
			}

			return userData;
		}
		
		public static IUserData CreateUserData(string emailaddress, string password)
		{
			if (userData == null) {
				GetUserData(emailaddress);
			}
			userData.Password = password;
			userData.SecurePassword = password.ConvertToSecureString();
			return userData;
		}

		private static void GetUserData(string emailaddress)
		{
			userData = new UserData();
			userData.EmailAddress = emailaddress;
		}

		public ExchangeVersion Version { get { return ExchangeVersion.Exchange2010_SP1; } }

		public string EmailAddress
		{
			get;
			private set;
		}
		
		public SecureString SecurePassword
		{
			get;
			private set;
		}
		
		public string Password
		{
			get;
			private set;
		}

		public Uri AutodiscoverUrl
		{
			get;
			set;
		}

		[Obsolete]
		public static IUserData CreateUserData(string p, TraceListener traceListener)
		{
			throw new NotImplementedException();
		}
	}
	
	public class UserDataFromConsole : IUserData
	{
		private static UserDataFromConsole userData;

		public static IUserData GetUserData()
		{
			if (userData == null)
			{
				GetUserDataFromConsole();
			}

			return userData;
		}

		private static void GetUserDataFromConsole()
		{
			userData = new UserDataFromConsole();

			Console.Write("Enter email address: ");
			userData.EmailAddress = Console.ReadLine();

			userData.SecurePassword = new SecureString();

			Console.Write("Enter password: ");

			while (true)
			{
				ConsoleKeyInfo userInput = Console.ReadKey(true);
				if (userInput.Key == ConsoleKey.Enter)
				{
					break;
				}
				else if (userInput.Key == ConsoleKey.Escape)
				{
					return;
				}
				else if (userInput.Key == ConsoleKey.Backspace)
				{
					if (userData.SecurePassword.Length != 0)
					{
						userData.SecurePassword.RemoveAt(userData.Password.Length - 1);
					}
				}
				else
				{
					userData.SecurePassword.AppendChar(userInput.KeyChar);
					Console.Write("*");
				}
			}

			Console.WriteLine();

			userData.SecurePassword.MakeReadOnly();
			userData.Password = userData.SecurePassword.ConvertToUnsecureString();
		}

		public ExchangeVersion Version { get { return ExchangeVersion.Exchange2010_SP1; } }

		public string EmailAddress
		{
			get;
			private set;
		}

		public SecureString SecurePassword
		{
			get;
			private set;
		}
		
		public string Password
		{
			get;
			private set;
		}

		public Uri AutodiscoverUrl
		{
			get;
			set;
		}
	}
}
