using Microsoft.Data.SqlClient;

namespace Challenge.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    
    void Commit();
    
    void Rollback();

    public SqlConnection Connection { get; }

    public SqlTransaction? Transaction { get; }
}
