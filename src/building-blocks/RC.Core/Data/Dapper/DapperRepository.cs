using Dapper;

namespace RC.Core.Data.Dapper
{
    public abstract class DapperRepository
    {
        public readonly int CommandTimeout = 300;

        public DapperRepository()
        {
            AddCustomTypeHandlersBaseCall();
        }

        protected virtual void AddCustomTypeHandler<T>()
        {
            SqlMapper.AddTypeHandler(new CustomTypeHandler<T>());
        }

        public abstract void AddCustomTypeHandlersBaseCall();
    }
}
