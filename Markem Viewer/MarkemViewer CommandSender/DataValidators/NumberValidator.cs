using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MarkemViewer_CommandSender
{
     static class NumberValidator
    {
        private static readonly Regex regex = new Regex("[^0-9]+");

        public static bool ValidateNumber(string textToValidate)
        {
            return !regex.IsMatch(textToValidate);
        }
    }
}
