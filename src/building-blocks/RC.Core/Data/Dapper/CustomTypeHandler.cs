using Dapper;
using System.Data;
using System.Text.Json;

namespace RC.Core.Data.Dapper
{
    public class CustomTypeHandler<T> : SqlMapper.TypeHandler<T>
    {
        public override T Parse(object value)
        {
            return JsonSerializer.Deserialize<T>(value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, T value)
        {
            parameter.Value = value.ToString();
        }
    }
}
