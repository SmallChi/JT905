using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.Extensions
{
    /// <summary>
    /// 
    /// ref http://adamsitnik.com/Span/#span-must-not-be-a-generic-type-argument
    /// ref http://adamsitnik.com/Span/
    /// ref:MessagePack.Formatters.DynamicObjectTypeFallbackFormatter
    /// </summary>
    public static class JT905MessagePackFormatterResolverExtensions
    {
        delegate void JT905SerializeMethod(object dynamicFormatter, ref JT905MessagePackWriter writer,object value, IJT905Config config);

        delegate dynamic JT905DeserializeMethod(object dynamicFormatter, ref JT905MessagePackReader reader, IJT905Config config);

        static readonly ConcurrentDictionary<Type, (object Value, JT905SerializeMethod SerializeMethod)> JT905Serializers = new ConcurrentDictionary<Type, (object Value, JT905SerializeMethod SerializeMethod)>();
        
        static readonly ConcurrentDictionary<Type, (object Value, JT905DeserializeMethod DeserializeMethod)> JT905Deserializes = new ConcurrentDictionary<Type, (object Value, JT905DeserializeMethod DeserializeMethod)>();
        /// <summary>
        /// JT905动态序列化
        /// </summary>
        /// <param name="objFormatter"></param>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public static void JT905DynamicSerialize(object objFormatter, ref JT905MessagePackWriter writer, object value, IJT905Config config)
        {
            Type type = value.GetType();
            var ti = type.GetTypeInfo();
          //  (object Value, JT905SerializeMethod SerializeMethod) formatterAndDelegate;
            if (!JT905Serializers.TryGetValue(type, out var formatterAndDelegate))
            {
                var t = type;
                {
                    var formatterType = typeof(IJT905MessagePackFormatter<>).MakeGenericType(t);
                    var param0 = Expression.Parameter(typeof(object), "formatter");
                    var param1 = Expression.Parameter(typeof(JT905MessagePackWriter).MakeByRefType(), "writer");
                    var param2 = Expression.Parameter(typeof(object), "value");
                    var param3 = Expression.Parameter(typeof(IJT905Config), "config");
                    var serializeMethodInfo = formatterType.GetRuntimeMethod("Serialize", new[] { typeof(JT905MessagePackWriter).MakeByRefType(), t, typeof(IJT905Config) });
                    var body = Expression.Call(
                        Expression.Convert(param0, formatterType),
                        serializeMethodInfo,
                        param1,
                        ti.IsValueType ? Expression.Unbox(param2, t) : Expression.Convert(param2, t),
                        param3);
                    var lambda = Expression.Lambda<JT905SerializeMethod>(body, param0, param1, param2, param3).Compile();
                    formatterAndDelegate = (objFormatter, lambda);
                }
                JT905Serializers.TryAdd(t, formatterAndDelegate);
            }
            formatterAndDelegate.SerializeMethod(formatterAndDelegate.Value, ref writer, value, config);
        }
        /// <summary>
        /// JT905动态反序列化
        /// </summary>
        /// <param name="objFormatter"></param>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static dynamic JT905DynamicDeserialize(object objFormatter, ref JT905MessagePackReader reader, IJT905Config config)
        {
            var type = objFormatter.GetType();
         //   (object Value, JT905DeserializeMethod DeserializeMethod) formatterAndDelegate;
            if (!JT905Deserializes.TryGetValue(type, out var formatterAndDelegate))
            {
                var t = type;
                {
                    var formatterType = typeof(IJT905MessagePackFormatter<>).MakeGenericType(t);
                    ParameterExpression param0 = Expression.Parameter(typeof(object), "formatter");
                    ParameterExpression param1 = Expression.Parameter(typeof(JT905MessagePackReader).MakeByRefType(), "reader");
                    ParameterExpression param2 = Expression.Parameter(typeof(IJT905Config), "config");
                    var deserializeMethodInfo = type.GetRuntimeMethod("Deserialize", new[] { typeof(JT905MessagePackReader).MakeByRefType(), typeof(IJT905Config) });
                    var body = Expression.Call(
                        Expression.Convert(param0, type),
                        deserializeMethodInfo,
                        param1,
                        param2
                        );
                    var lambda = Expression.Lambda<JT905DeserializeMethod>(body, param0, param1, param2).Compile();
                    formatterAndDelegate = (objFormatter, lambda);
                }
                JT905Deserializes.TryAdd(t, formatterAndDelegate);
            }
            return formatterAndDelegate.DeserializeMethod(formatterAndDelegate.Value,ref reader, config);
        }
    }
}
