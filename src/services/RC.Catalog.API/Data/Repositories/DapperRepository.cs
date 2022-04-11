using Dapper;
using RC.Catalog.API.Data.Dapper;

namespace RC.Catalog.API.Data.Repositories
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
            SqlMapper.AddTypeHandler<T>(new CustomTypeHandler<T>());
        }

        public abstract void AddCustomTypeHandlersBaseCall();
    }
}
