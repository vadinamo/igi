using Newtonsoft.Json;

namespace WEB_053502_YUREV.Extensions;

public static class SessionsExtension
{
    public static void Set<T>(this ISession session, string key , T item)
    {
        var serializedItem = JsonConvert.SerializeObject(item);
        session.SetString(key, serializedItem);
    }

    public static T Get<T>(this ISession session, string key)
    {
        var item = session.GetString(key);
        return item == null
            ? Activator.CreateInstance<T>() // default(T)
            : JsonConvert.DeserializeObject<T>(item);
    }
}