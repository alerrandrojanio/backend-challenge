using MySql.Data.MySqlClient;

namespace Challenge.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    
    void Commit();
    
    void Rollback();
    
    MySqlConnection Connection { get; }
    
    MySqlTransaction? Transaction { get; }
}
