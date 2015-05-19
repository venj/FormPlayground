using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace me.venj.Extensions
{
    // int的扩展
    public static class IntExtension
    {
        //增加了Times方法。
        public static void Times(this int me, Action action)
        {
            for (int i = 0; i < me; i++)
            {
                action();
            }
        }

        //增加了To方法。
        public static IEnumerable<int> To(this int me, int end, bool open = false)
        {
            int incrementer = me;
            if (me < end)
            {
                while (true)
                {
                    if (open == false && incrementer > end) break;
                    if (open && incrementer >= end) break;
                    yield return incrementer++;
                }
            }
            else
            {
                while(true)
                {
                    if (open == false && incrementer < end) break;
                    if (open && incrementer <= end) break;
                    yield return incrementer--;
                }
            }
        }

        //增加了Seconds，Minutes，Hours，Days方法。
        public static TimeSpan Seconds(this int me)
        {
            return TimeSpan.FromSeconds(me);
        }
        public static TimeSpan Minites(this int me)
        {
            return TimeSpan.FromMinutes(me);
        }
        public static TimeSpan Hours(this int me)
        {
            return TimeSpan.FromHours(me);
        }
        public static TimeSpan Days(this int me)
        {
            return TimeSpan.FromDays(me);
        }
    }

    // TimeSpan扩展
    public static class TimeSpanExtension
    {
        //增加了From方法。
        public static DateTime From(this TimeSpan me, DateTime start)
        {
            return start.Add(me);
        }

        //增加了FromNow方法。
        public static DateTime FromNow(this TimeSpan me)
        {
            return From(me, DateTime.Now);
        }
    }

    // IEnumerable接口的扩展。
    public static class EnumerableInterfaceExtension
    {
        // 增加了Each方法。
        public static void Each<T>(this IEnumerable<T> me, Action<T> action)
        {
            foreach (T t in me)
            {
                action(t);
            }
        }

        // 增加了Map方法。（这个方法与标准库的Select方法功能一致。）
        public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<T> me, Func<T, TResult> f)
        {
            foreach (T t in me)
            {
                yield return f(t);
            }
        }

        // 增加Find方法。需要改进。
        // Caveat: 如果T为值类型，返回default(T)，可能并非正确结果。
        public static T Find<T>(this IEnumerable<T> me, Predicate<T> p)
        {
            T result = default(T);
            foreach (T t in me)
            {
                if (p(t))
                {
                    result = t;
                    break;
                }
            }

            return result;
        }

        // 增加了Filter方法
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> me, Predicate<T> p)
        {
            foreach (T t in me)
            {
                if (p(t))
                {
                    yield return t;
                }
            }
        }

        // 增加了Reject方法
        public static IEnumerable<T> Reject<T>(this IEnumerable<T> me, Predicate<T> p)
        {
            foreach (T t in me)
            {
                if (!p(t))
                {
                    yield return t;
                }
            }
        }

        // 增加了Every方法
        public static bool Every<T>(this IEnumerable<T> me, Predicate<T> p)
        {
            bool result = true;

            foreach (T t in me)
            {
                if (!p(t))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        // 增加了Some方法
        public static bool Some<T>(this IEnumerable<T> me, Predicate<T> p)
        {
            bool result = false;

            foreach (T t in me)
            {
                if (p(t))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        // 增加了Reduce方法。
        public static TResult Reduce<T, TResult>(this IEnumerable<T> me, TResult seed, Func<TResult, T, TResult> f)
        {
            var result = seed;
            foreach (T t in me)
            {
                result = f(result, t);
            }
            return result;
        }
    }

    // Windows Form Control类的扩展。在主线程上调用action。
    public static class ControlExtensions
    {
        public static void UIThread(this Control me, Action code)
        {
            if (me.InvokeRequired)
            {
                me.BeginInvoke(code);
            }
            else
            {
                code.Invoke();
            }
        }
    }
}
