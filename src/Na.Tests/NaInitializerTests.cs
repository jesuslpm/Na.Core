using Na.Core;

namespace Na.Tests
{
	public class NaInitializerTests
	{
		[Test]
		public async Task EnsureInitializedTest()
		{
			NaInitializer.EnsureInitialized();
			
			await Assert.That(NaInitializer.IsInitialized).IsTrue();
			await Assert.That(Interop.Libsodium.sodium_init()).IsEqualTo(1);
		}
	}
}
