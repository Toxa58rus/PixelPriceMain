using Newtonsoft.Json;
using System;
using Common.Models;

namespace Common.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object value)
        {
            try
            {
                return JsonConvert.SerializeObject(value);
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось преобразовать объект в json", e);
            }
        }

        public static string ToCommandresponseJson(this object value, string commandName)
        {
            try
            {
                var commandresponse = new CommandResponse
                {
                    CommandName = commandName,
                    Value = value
                };

                return JsonConvert.SerializeObject(commandresponse);
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось преобразовать объект в Commandresponse", e);
            }
        }

        public static T DeserializeToObject<T>(this string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception e)
            {
                throw new Exception($"Не удалось преобразовать объект в {typeof(T).FullName}", e);
            }
        }
    }
}
