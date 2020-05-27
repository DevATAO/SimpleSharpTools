using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SimpleSharpTool.Core
{
    /// <summary>
    /// 时间日期处理
    /// </summary>
    public class DateTimeService
    {
        /// <summary>
        /// 获取星期几
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public string GetTodayDate(DateTime dateTime)
        {
            return dateTime.DayOfWeek.ToString();
        }

        /// <summary>
        /// 获取农历日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public string GetLunaCalendarDate(DateTime dateTime)
        {
            ChineseLunisolarCalendar calendar = new ChineseLunisolarCalendar();

            return $"{calendar.GetMonth(dateTime)}月初{calendar.GetDayOfMonth(dateTime)}";
        }

        /// <summary>
        /// 自定义格式
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public string FormatDate(DateTime dateTime)
        {
            /*
             * yyyy 年
             *
             * MM 月
             *
             * dd 日
             *
             * HH 时
             *
             * mm 分
             *
             * ss 秒
             *
             */

            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
