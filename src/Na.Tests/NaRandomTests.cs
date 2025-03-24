using Na.Core;
using TUnit.Assertions.AssertConditions.Throws;

namespace Na.Tests
{
	public class NaRandomTests
	{
		[Test]
		public async Task GetUInt32_ReturnsRandomUInt32()
		{
			uint random1 = NaRandom.GetUInt32();
			uint random2 = NaRandom.GetUInt32();
			
			await Assert.That(random1).IsNotEqualTo(random2);
			
		}

		[Test]
		public async Task GetUInt32_WithUpperBound_ReturnsRandomUInt32LessThanUpperBound()
		{
			uint upperBound = 100;
			uint random = NaRandom.GetUInt32(upperBound);

			

			await Assert.That(random).IsLessThan(upperBound);
		}

		[Test]
		public async Task Fill_FillsBufferWithRandomBytes()
		{
			var b1 = new byte[32];
			var b2 = new byte[32];
			var zeroes = new byte[32];

			NaRandom.Fill(b1);
			NaRandom.Fill(b2);


			await Assert.That(b1).IsNotSequenceEqualTo(b2);
			await Assert.That(b1).IsNotSequenceEqualTo(zeroes);
			await Assert.That(b2).IsNotSequenceEqualTo(zeroes);

		}

		[Test]
		public async Task FillDeterministic_FillsBufferWithDeterministicRandomBytes()
		{
			Span<byte> s1 = stackalloc byte[NaRandom.SeedLen];
			Span<byte> s2 = stackalloc byte[NaRandom.SeedLen];

			Random random = new Random();
			random.NextBytes(s1);
			random.NextBytes(s2);

			var b1s1 = new byte[32];
			var b2s1 = new byte[32];
			var b3s2 = new byte[32];

			NaRandom.FillDeterministic(b1s1, s1);
			NaRandom.FillDeterministic(b2s1, s1);
			NaRandom.FillDeterministic(b3s2, s2);

			await Assert.That(b1s1).IsSequenceEqualTo(b2s1);
			await Assert.That(b1s1).IsNotSequenceEqualTo(b3s2);

		}

		[Test]
		public async Task FillDeterministic_ThrowsArgumentException_WhenSeedLengthIsInvalid()
		{
			byte[] seed = new byte[NaRandom.SeedLen - 1];
			byte[] buffer = new byte[32];

			await Assert.That(() => NaRandom.FillDeterministic(buffer, seed)).Throws<ArgumentException>();
		}

		//[Test]
		//public async Task CloseAndStir_WorksAsExpected()
		//{
		//	try
		//	{
		//		await Assert.That(() => SodiumRandom.Stir()).ThrowsNothing();
		//		await Assert.That(() => SodiumRandom.Stir()).ThrowsNothing();
		//		await Assert.That(() => SodiumRandom.Close()).ThrowsNothing();
		//		await Assert.That(() => SodiumRandom.Close()).Throws<SodiumException>();
		//	}
		//	finally
		//	{
		//		SodiumRandom.Stir();
		//	}			
		//}
	}
}
