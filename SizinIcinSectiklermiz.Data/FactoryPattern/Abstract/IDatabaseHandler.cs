using System.Data;

namespace SizinIcinSectiklermiz.Data.FactoryPattern
{
    public interface IDatabaseHandler
    {
        void InsertDb(Models.Data data);
        void TruncateDb();
    }
}
