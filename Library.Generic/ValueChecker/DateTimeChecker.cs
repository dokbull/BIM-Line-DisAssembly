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
    public class DateTimeChecker
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

        /// <summary>
        /// 1~12월 사이의 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="checkingMonth"></param>
        /// <returns></returns>
        public static bool IsMonth(int checkingMonth)
        {
            return NumberRange(checkingMonth, 1, 12);
        }

        /// <summary>
        /// 1~12월 사이의 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="checkingMonth"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static bool IsMonth(string checkingMonth, out int month)
        {
            return NumberRange(checkingMonth, 1, 12, out month);
        }

        /// <summary>
        ///  1~12월 사이의 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="year">반환할 Date에 적용될 연도</param>
        /// <param name="checkingMonth"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsMonth(int year, string checkingMonth, out DateTime date)
        {
            date = new DateTime();

            if (IsMonth(checkingMonth, out int month))
            {
                date = new DateTime(year, month, 1);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 해당 연/월의 날자에 포함되는지 여부를 체크한다.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="checkingDay"></param>
        /// <returns></returns>
        public static bool IsDay(int year, int month, int checkingDay)
        {
            int lastDay = DateTime.DaysInMonth(year, month);

            return NumberRange(checkingDay, 1, lastDay);
        }

        /// <summary>
        /// 해당 연/월의 날자에 포함되는지 여부를 체크한다.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="checkingDay"></param>
        /// <param name="intDay"></param>
        /// <returns></returns>
        public static bool IsDay(int year, int month, string checkingDay, out int intDay)
        {
            int lastDay = DateTime.DaysInMonth(year, month);

            return NumberRange(checkingDay, 1, lastDay, out intDay);
        }

        /// <summary>
        /// 해당 연/월의 날자에 포함되는지 여부를 체크한다.
        /// </summary>
        /// <param name="year">반환할 Date에 적용될 연도</param>
        /// <param name="month">반환할 Date에 적용될 월</param>
        /// <param name="checkingDay"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsDay(int year, int month, int checkingDay, out DateTime date)
        {
            date = new DateTime();

            if (IsDay(year, month, checkingDay))
            {
                date = new DateTime(year, month, checkingDay);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 해당 연/월의 날자에 포함되는지 여부를 체크한다.
        /// </summary>
        /// <param name="year">반환할 Date에 적용될 연도</param>
        /// <param name="month">반환할 Date에 적용될 월</param>
        /// <param name="checkingDay"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsDay(int year, int month, string checkingDay, out DateTime date)
        {
            date = new DateTime();

            if (IsDay(year, month, checkingDay, out int day))
            {
                date = new DateTime(year, month, day);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 연/월/일을 표시하는 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="checkinMonth"></param>
        /// <param name="checkingDay"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsDate(int year, int checkinMonth, int checkingDay, out DateTime date)
        {
            date = new DateTime();

            if (IsMonth(checkinMonth) == false) return false;
            if (IsDay(year, checkinMonth, checkingDay) == false) return false;

            date = new DateTime(year, checkinMonth, checkingDay);
            return true;
        }

        /// <summary>
        /// 연/월/일을 표시하는 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="checkinMonth"></param>
        /// <param name="checkingDay"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsDate(int year, string checkinMonth, string checkingDay, out DateTime date)
        {
            date = new DateTime();

            if (IsMonth(checkinMonth, out int month) == false) return false;
            if (IsDay(year, month, checkingDay, out int day) == false) return false;

            date = new DateTime(year, month, day);
            return true;
        }

        /// <summary>
        /// 0~23 사이의 시간을 표시하는 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="checkingHour"></param>
        /// <returns></returns>
        public static bool IsHour(int checkingHour)
        {
            return NumberRange(checkingHour, 0, 23);
        }

        /// <summary>
        /// 0~23 사이의 시간을 표시하는 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="checkingHour"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static bool IsHour(string checkingHour, out int hour)
        {
            return NumberRange(checkingHour, 1, 12, out hour);
        }


        /// <summary>
        /// 0~23 사이의 시간을 표시하는 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="date">반환할 Date에 적용될 연/월/일 정보</param>
        /// <param name="checkingHour"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsHour(DateTime date, int checkingHour, out DateTime dateTime)
        {
            dateTime = new DateTime();

            if(IsHour(checkingHour))
            {
                dateTime = new DateTime(date.Year, date.Month, date.Day, checkingHour, 0, 0);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 0~23 사이의 시간을 표시하는 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="date">반환할 Date에 적용될 연/월/일 정보</param>
        /// <param name="checkingHour"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsHour(DateTime date, string checkingHour, out DateTime dateTime)
        {
            dateTime = new DateTime();

            if (IsHour(checkingHour, out int hour))
            {
                dateTime = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 0~59 사이의 분/초을 표시하는 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsMinOrSec(int data)
        {
            return NumberRange(data, 0, 59);
        }

        /// <summary>
        /// 0~59 사이의 분/초을 표시하는 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static bool IsMinOrSec(string data, out int time)
        {
            return NumberRange(data, 0, 59, out time);
        }

        /// <summary>
        /// 시간을 표시하는 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="checkingHour"></param>
        /// <param name="checkingMin"></param>
        /// <param name="checkingSec"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsTime(DateTime date, int checkingHour, int checkingMin, int checkingSec, out DateTime time)
        {
            time = new DateTime();

            if (IsMinOrSec(checkingMin) == false) return false;
            if (IsMinOrSec(checkingSec) == false) return false;

            time = new DateTime(date.Year, date.Month, date.Day, checkingHour, checkingMin, checkingSec);

            return true;
        }

        /// <summary>
        /// 시간을 표시하는 숫자인지 여부를 체크한다.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="checkingHour"></param>
        /// <param name="checkingMin"></param>
        /// <param name="checkingSec"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsTime(DateTime date, string checkingHour, string checkingMin, string checkingSec, out DateTime dateTime)
        {
            dateTime = new DateTime();

            if (IsMinOrSec(checkingHour, out int hour) == false) return false;
            if (IsMinOrSec(checkingMin, out int min) == false) return false;
            if (IsMinOrSec(checkingSec, out int sec) == false) return false;

            dateTime = new DateTime(date.Year, date.Month, date.Day, hour, min, sec);

            return true;
        }

    }
}
