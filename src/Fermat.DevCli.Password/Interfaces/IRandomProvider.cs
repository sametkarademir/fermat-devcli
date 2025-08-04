namespace Fermat.DevCli.Password.Interfaces;

/// <summary>
/// Interface for random number and byte generation.
/// </summary>
/// <remarks>
/// This interface defines methods for generating random integers and byte arrays.
/// </remarks>
public interface IRandomProvider
{
    /// <summary>
    /// Generates a random integer within the specified range.
    /// </summary>
    /// <param name="minInclusive">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxExclusive">The exclusive upper bound of the random number returned.</param>
    /// <returns>A random integer between minInclusive and maxExclusive.</returns>
    /// <remarks>
    /// This method uses a cryptographically secure random number generator to produce a random integer.
    /// </remarks>
    int GetRandomInt(int minInclusive, int maxExclusive);

    /// <summary>
    /// Generates a random byte array of the specified length.
    ///</summary>
    ///<param name="length">The length of the byte array to generate.</param>
    ///<returns>A byte array filled with random bytes.</returns>
    ///<remarks>
    /// This method uses a cryptographically secure random number generator to produce a byte array.
    ///</remarks>
    byte[] GetRandomBytes(int length);
}
