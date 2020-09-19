using System.Collections.Generic;
using Nop.Core.Domain.Messages;

namespace Nop.Service.Messages
{
    public interface ITokenizer
    {
        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in the current instance are replaced with another specified string
        /// </summary>
        /// <param name="original">Original string</param>
        /// <param name="pattern">The string to be replaced</param>
        /// <param name="replacement">The string to replace all occurrences of pattern string</param>
        /// <returns>A string that is equivalent to the current string except that all instances of pattern are replaced with replacement string</returns>
        string Replace(string original, string pattern, string replacement);

        /// <summary>
        /// Replace tokens
        /// </summary>
        /// <param name="template">The template with token keys inside</param>
        /// <param name="tokens">The sequence of tokens to use</param>
        /// <param name="htmlEncode">The value indicating whether tokens should be HTML encoded</param>
        /// <param name="stringWithQuotes">The value indicating whether string token values should be wrapped in quotes</param>
        /// <returns>Text with all token keys replaces by token value</returns>
        string ReplaceTokens(string template, IEnumerable<Token> tokens, bool htmlEncode = false, bool stringWithQuotes = false);

        /// <summary>
        /// Resolve conditional statements and replace them with appropriate values
        /// </summary>
        /// <param name="template">The template with token keys inside</param>
        /// <param name="tokens">The sequence of tokens to use</param>
        /// <returns>Text with all conditional statements replaces by appropriate values</returns>
        string ReplaceConditionalStatements(string template, IEnumerable<Token> tokens);

        /// <summary>
        /// Replace all of the token key occurrences inside the specified template text with corresponded token values
        /// </summary>
        /// <param name="template">The template with token keys inside</param>
        /// <param name="tokens">The sequence of tokens to use</param>
        /// <param name="htmlEncode">The value indicating whether tokens should be HTML encoded</param>
        /// <returns>Text with all token keys replaces by token value</returns>
        string Replace(string template, IEnumerable<Token> tokens, bool htmlEncode);
    }
}