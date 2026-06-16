using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace eVote360Pro.Core.Application.Helpers
{
    public static class SessionHelper
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            var bytes = Encoding.UTF8.GetBytes(json);

            session.Set(key, bytes);
        }

        public static T? Get<T>(this ISession session, string key)
        {
            if (!session.TryGetValue(key, out var bytes))
                return default;

            var json = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}