using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;

namespace PostgRESTSharp.Shared
{
    [Order(2)]
    public class RangeLimitHeaderTransformer : IRequestHeaderTransformer
    {
        private const string rangeUnitHeaderKey = "Range-Unit";
        private const string rangeHeaderKey = "Range";
        private const string rangeValuePrefix = "items=";

        public int CountLimit { get; set; }

        public RangeLimitHeaderTransformer()
        {
            CountLimit = 100;
        }

        public void Transform(Request incomingRequestToProcess, IList<KeyValuePair<string, IEnumerable<string>>> postgRestHeadersToAddTo)
        {
            var existingRangeHeader = postgRestHeadersToAddTo.FirstOrDefault(a => a.Key.Equals(rangeHeaderKey, StringComparison.OrdinalIgnoreCase));
            if (existingRangeHeader.Key == null)
            {
                SetRangeHeaderDefaults(postgRestHeadersToAddTo, existingRangeHeader);
                return;
            }
            var rangeString = existingRangeHeader.Value.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(rangeString))
            {
                SetRangeHeaderDefaults(postgRestHeadersToAddTo, existingRangeHeader);
                return;
            }
            if (rangeString.StartsWith(rangeValuePrefix, StringComparison.OrdinalIgnoreCase))
            {
                rangeString = rangeString.Remove(0, rangeValuePrefix.Length);
            }
            var indexOfDash = rangeString.LastIndexOf("-");
            if (indexOfDash < 0)
            {
                SetRangeHeaderDefaults(postgRestHeadersToAddTo, existingRangeHeader);
                return;
            }
            var startIndexString = rangeString.Substring(0, indexOfDash);
            int startIndex;
            if (!int.TryParse(startIndexString, out startIndex))
            {
                SetRangeHeaderDefaults(postgRestHeadersToAddTo, existingRangeHeader);
                return;
            }
            if (startIndex < 0)
            {
                SetRangeHeaderDefaults(postgRestHeadersToAddTo, existingRangeHeader);
                return;
            }
            var maxEndIndex = GetIndexForCountLimit(startIndex);
            var endIndex = maxEndIndex;
            if (rangeString.Length - 1 != indexOfDash)
            {
                var endIndexString = rangeString.Substring(indexOfDash + 1);
                if (!int.TryParse(endIndexString, out endIndex))
                {
                    endIndex = maxEndIndex;
                }
            }
            if (endIndex > maxEndIndex)
            {
                endIndex = maxEndIndex;
            }
            SetRangeHeaders(postgRestHeadersToAddTo, existingRangeHeader, startIndex, endIndex);
        }

        private void SetRangeHeaderDefaults(IList<KeyValuePair<string, IEnumerable<string>>> postgRestHeadersToAddTo, KeyValuePair<string, IEnumerable<string>> existingRangeHeader)
        {
            SetRangeHeaders(postgRestHeadersToAddTo, existingRangeHeader, 0, GetCountLimitAsIndex());
        }

        private void SetRangeHeaders(IList<KeyValuePair<string, IEnumerable<string>>> postgRestHeadersToAddTo, KeyValuePair<string, IEnumerable<string>> existingRangeHeader, int startIndex, int endIndex)
        {
            if (existingRangeHeader.Key != null)
            {
                postgRestHeadersToAddTo.Remove(existingRangeHeader);
            }
            AddRangeHeader(postgRestHeadersToAddTo, startIndex, endIndex);
            AddRangeUnitHeaderIfNotPresent(postgRestHeadersToAddTo);
        }

        private int GetIndexForCountLimit(int startIndex)
        {
            return startIndex + GetCountLimitAsIndex();
        }

        private int GetCountLimitAsIndex()
        {
            return CountLimit - 1;
        }

        private static void AddRangeHeader(IList<KeyValuePair<string, IEnumerable<string>>> postgRestHeadersToAddTo, int startIndex, int endIndex)
        {
            postgRestHeadersToAddTo.Add(new KeyValuePair<string, IEnumerable<string>>(rangeHeaderKey, new[] { startIndex + "-" + endIndex }));
        }

        private void AddRangeUnitHeaderIfNotPresent(IList<KeyValuePair<string, IEnumerable<string>>> postgRestHeadersToAddTo)
        {
            var existingRangeUnitHeader = postgRestHeadersToAddTo
                .FirstOrDefault(a => a.Key.Equals(rangeUnitHeaderKey, StringComparison.OrdinalIgnoreCase));

            if (existingRangeUnitHeader.Key == null)
            {
                postgRestHeadersToAddTo.Add(new KeyValuePair<string, IEnumerable<string>>(rangeUnitHeaderKey, new[] { "items" }));
            }
        }
    }
}
