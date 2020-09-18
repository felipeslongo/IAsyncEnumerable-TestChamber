using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace IAsyncEnumerable.ConsoleApp.RangeAsyncNS
{
    /// <summary>
    ///     Generates a sequence of integral numbers within a specified range.
    /// </summary>
    public sealed class RangeAsync : IAsyncEnumerable<int>
    {
        public RangeAsync(int start, int count)
        {
            Start = start;
            Count = count;
        }

        /// <summary>
        ///     The value of the first integer in the sequence.
        /// </summary>
        public int Start { get; }
        /// <summary>
        ///     The number of sequential integers to generate.
        /// </summary>
        public int Count { get; }

        public async IAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            foreach (var item in Enumerable.Range(Start, Count))
            {
                await Task.Delay(1000, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                yield return item;
            }
        }
    }
}
