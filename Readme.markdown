Form Playground
===============

又是一个名字和内容对不上号的项目。本来只是用来测试C#的练习代码的，现在变成了保存C#扩展的源了。虽然目前只有几个方法，但是以后会慢慢加入更多。

方法：
-----

`int`扩展：

`void Times(Acthon action)`：多次执行action。

`IEmumerable<T>`扩展：

`void Each<T>(Action<T> action)`：对集合的每个对象执行action。（可能需要修改）

`IEnumerable<TResult> Map<T, TResult>(Func<T, TResult> f)`：将集合成员均通过f映射，并返回。

`TResult Reduct<T, TResult>(Func<TResult, T, TResult> f)`：将集合成员聚合为一个结果，并返回。

`Windows.Forms.Control`扩展：

`void UIThread(Action action)`：将action dispatch回主线程执行。
