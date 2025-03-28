﻿using Na.Interop;

namespace Na.Core
{
	/// <summary>
	/// Static class for random number generation.
	/// </summary>
	public static class NaRandom
	{
		/// <summary>
		/// The length of the seed used for deterministic random byte generation.
		/// </summary>
		public const int SeedLen = (int)Libsodium.randombytes_SEEDBYTES;

		/// <summary>
		/// Gets a random unsigned 32-bit integer.
		/// </summary>
		/// <returns>A random unsigned 32-bit integer.</returns>
		public static uint GetUInt32()
		{
			NaInitializer.EnsureInitialized();
			return Libsodium.randombytes_random();
		}

		/// <summary>
		/// Gets a random unsigned 32-bit integer less than the specified upper bound.
		/// </summary>
		/// <param name="upperBound">The upper bound (exclusive) for the random number.</param>
		/// <returns>A random unsigned 32-bit integer less than upperBound.</returns>
		public static uint GetUInt32(uint upperBound)
		{
			NaInitializer.EnsureInitialized();
			return Libsodium.randombytes_uniform(upperBound);
		}

		/// <summary>
		/// Fills the specified buffer with random bytes.
		/// </summary>
		/// <param name="buffer">The buffer to fill with random bytes.</param>
		public static void Fill(Span<byte> buffer)
		{
			NaInitializer.EnsureInitialized();
			Libsodium.randombytes_buf(buffer, (nuint)buffer.Length);
		}

		/// <summary>
		/// Fills the specified buffer with deterministic random bytes based on the provided seed.
		/// It produces the same sequence of random bytes for the same seed.
		/// </summary>
		/// <param name="buffer">The buffer to fill with deterministic random bytes.</param>
		/// <param name="seed">The seed used for deterministic random byte generation.</param>
		/// <exception cref="ArgumentException">Thrown when the seed length is not equal to SeedLen.</exception>
		public static void FillDeterministic(Span<byte> buffer, ReadOnlySpan<byte> seed)
		{
			NaInitializer.EnsureInitialized();
			if (seed.Length != SeedLen)
			{
				throw new ArgumentException($"seed must be {SeedLen} bytes in length", nameof(seed));
			}
			Libsodium.randombytes_buf_deterministic(buffer, (nuint)buffer.Length, seed);
		}

		/// <summary>
		/// Closes the random number generator.
		/// </summary>
		/// <exception cref="NaException">Thrown when randombytes_close() fails.</exception>
		public static void Close()
		{
			NaInitializer.EnsureInitialized();
			if (Libsodium.randombytes_close() != 0)
			{
				throw new NaException("randombytes_close() failed");
			}
		}

		/// <summary>
		/// Stirs the random number generator to ensure randomness.
		/// </summary>
		public static void Stir()
		{
			NaInitializer.EnsureInitialized();
			Libsodium.randombytes_stir();
		}
	}
}
