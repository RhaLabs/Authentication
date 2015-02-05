using System.Security;
using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.WebServices.Authentication
{
	public static class Extension
	{
		public static string ConvertToUnsecureString 
			(this SecureString securestring)
		{
			if (securestring == null)
				throw new ArgumentNullException 
					("securestring", "string cannot be null");
			IntPtr unmanagedString = IntPtr.Zero;
			try {
				unmanagedString = Marshal.SecureStringToGlobalAllocUnicode (
					securestring);
				return Marshal.PtrToStringUni (unmanagedString);
			} finally {
				Marshal.ZeroFreeGlobalAllocUnicode (unmanagedString);
			}
		}
		
		public static SecureString ConvertToSecureString 
			(this string unsecurestring)
		{
			if (unsecurestring == null)
				throw new ArgumentNullException
					("unsecurestring", "string cannot be null");
			unsafe {
				fixed (char* stringChars = unsecurestring) {
					var securestring = new SecureString (stringChars,
					                                     unsecurestring.Length);
					securestring.MakeReadOnly ();
					return securestring;
				}
			}
		}
	}
}