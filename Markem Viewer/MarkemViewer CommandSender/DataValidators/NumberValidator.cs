using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MarkemViewer_CommandSender
{
     public static class NumberValidator
    {
        public static bool ValidatePositiveNumber(this string textToValidate) 
        {
            if ((textToValidate.Count() > 1 && textToValidate[0].Equals('0')) || string.IsNullOrEmpty(textToValidate))
                return false;

            return textToValidate.All(char.IsNumber);



        }

    }
}
