namespace ZTest.Tools;
public static class ClassHelper {
    /// <summary>
    /// 父-->子，无参转换
    /// </summary>
    /// <typeparam name="Parent">父类</typeparam>
    /// <typeparam name="Child">子类</typeparam>
    /// <param name="data">父类本身</param>
    /// <returns></returns>
    public static Child ChildConvert<Parent, Child>(this Parent data)
        where Parent : class, new()
        where Child : Parent, new() {
        Child returnvalue = new Child();
        System.Reflection.PropertyInfo[] properties = data.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        foreach (var item in properties) {
            if (!(item.CanRead && item.CanWrite)) continue;
            item.SetValue(returnvalue, item.GetValue(data));
        }
        return returnvalue;
    }
}
