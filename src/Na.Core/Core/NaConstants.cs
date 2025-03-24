using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Na.Core
{
	/// <summary>
	/// Provides constant values used throughout the Na.Core library.
	/// </summary>
	public static class NaConstants
	{
		/// <summary>
		/// The maximum size, in bytes, allowed for stack allocations using <c>stackalloc</c>.
		/// This constant is set to 640 bytes to balance performance and safety.
		/// <para>
		/// The Na.Core library is designed to never exceed this limit for stack allocations.
		/// </para>
		/// <para>
		/// Keeping stack allocations small helps prevent stack overflows and improves performance.
		/// </para>
		/// </summary>
		public const int MaxStackAlloc = 640;
	}
}
