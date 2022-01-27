/****************************************************************************
 * Copyleft (L) 2022 CENG - All Rights Not Reserved
 * You may use, distribute and modify this code.
 ****************************************************************************/

/**
 * @file StringNumberPartComparator.cs
 * @author Utku Oruc
 * @date 25 January 2022
 *
 * @brief <b> compare number part </b>
 *
 * StringNumberPartComparator.cs
 *
 * @see http://bilgisayar.mmf.erdogan.edu.tr/en/
 *
 */
using System.Collections.Generic;

namespace LargeTextFilesSorting
{
    public sealed class StringNumberPartComparator : IComparer<StringNumberPart>
    {
        public int Compare(StringNumberPart x, StringNumberPart y)
        {
            var pairComparision = string.CompareOrdinal(x.StringPart, y.StringPart);
            if (pairComparision == 0)
            {
                pairComparision = string.CompareOrdinal(x.NumberPart, y.NumberPart);
            }
            return pairComparision;
        }
    }
}