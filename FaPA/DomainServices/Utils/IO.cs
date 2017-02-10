using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FaPA.DomainServices.Utils
{
    public static class IO
    {

        public static DirectoryInfo GetOrCreateFolder(string dirOutput)
        {
            try
            {
                return !Directory.Exists(dirOutput) ? Directory.CreateDirectory(dirOutput) : new DirectoryInfo(dirOutput);
            }
            catch (IOException e)
            {
                return null;
            }

        }

        // Regex version
        /// <summary>
        /// "(?i)\.mp3$|\.mp4$" for case insensitive
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPatternExpression"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetFiles(string path, string searchPatternExpression = "",
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var reSearchPattern = new Regex(searchPatternExpression, RegexOptions.IgnoreCase);

            return Directory.EnumerateFiles(path, "*", searchOption).Where(file =>
                 reSearchPattern.IsMatch(Path.GetExtension(file)));
        }

        /// <summary>
        /// Takes same patterns, and executes in parallel (case insensitive)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPatterns"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetFiles(string path, string[] searchPatterns,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return searchPatterns.AsParallel().SelectMany(searchPattern =>
                   Directory.EnumerateFiles(path, searchPattern, searchOption));
        }
    }
}
