using System.Security.Cryptography;
using Synchronization.Logs;

namespace Synchronization.Utils
{
    /// <summary>
    /// Class <c>CheckSumUtils</c> utility to render hash for the input data.
    /// </summary>
    public class CheckSumUtils
    {
        /// <summary>
        /// Method <c>CheckSumUtils</c> that returns the SHA256 hash for the input data
        /// </summary>
        /// <returns>
        /// A string representing hash for the input data.
        /// </returns>
        public string SHA256CheckSum(string filePath)
        {
            var _log = new ConsoleLog("SHA256CheckSum");
            string bitConverter = string.Empty;
            try
            {
                using var sha256 = SHA256.Create();
                using (var fileStream = File.OpenRead(filePath))
                    bitConverter = BitConverter.ToString(sha256.ComputeHash(fileStream)).Replace("-", "");
            }
            catch (Exception e)
            {
                _log.Error($"CheckSum exception: {e.Message}");
            }

            return bitConverter;
        }
    }
}

