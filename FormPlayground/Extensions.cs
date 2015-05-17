using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace me.venj.Extensions
{
    // int的扩展，增加了Times方法。
    public static class IntExtension
    {
        public static void Times(this int me, Action action)
        {
            for (int i = 0; i < me; i++)
            {
                action();
            }
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

        // 增加了Map方法。
        public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<T> me, Func<T, TResult> f)
        {
            foreach (T t in me)
            {
                yield return f(t);
            }
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
