using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Generic.ValueChecker
{
    public class NumberChecker
    {
        /// <summary>
        /// min 이상 ~ max 이하의 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="checkingData"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool NumberRange(int checkingData, int min, int max)
        {
            return (checkingData >= min || checkingData <= max);
        }

        /// <summary>
        /// min 이상 ~ max 이하의 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="checkingData"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="intData"></param>
        /// <returns></returns>
        public static bool NumberRange(string checkingData, int min, int max, out int intData)
        {
            if (int.TryParse(checkingData, out intData) == false) return false;

            return NumberRange(intData, min, max);
        }

    }
}
