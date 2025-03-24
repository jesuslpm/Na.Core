using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Na.Interop
{
	internal static partial class Libsodium
	{

		[LibraryImport("libsodium")]
		[UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
		internal static partial int template();
	}
}
