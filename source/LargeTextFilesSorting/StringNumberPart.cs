/****************************************************************************
 * Copyleft (L) 2022 CENG - All Rights Not Reserved
 * You may use, distribute and modify this code.
 ****************************************************************************/

/**
 * @file StringNumberPart.cs
 * @author Utku Oruc
 * @date 25 January 2022
 *
 * @brief <b> define string number part </b>
 *
 * StringNumberPart.cs
 *
 * @see http://bilgisayar.mmf.erdogan.edu.tr/en/
 *
 */
using System.Diagnostics;

namespace LargeTextFilesSorting
{
    [DebuggerDisplay("String={StringPart}, Number={NumberPart}")]
    public struct StringNumberPart
    {
        public StringNumberPart(string stringPart, string numberPart)
        {
            StringPart = stringPart;
            NumberPart = numberPart;
        }

        public string StringPart;
        public string NumberPart;
    }
}