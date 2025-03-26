using Challenge.Domain.Interfaces;
using Challenge.Infrastructure.Configurations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Challenge.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseSettings _databaseSettings;
    private SqlConnection? _connection;

    public SqlConnection Connection
    {
        get
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_databaseSettings.DefaultConnection);
                _connection.Open();
            }
            return _connection;
        }
    }

    public SqlTransaction? Transaction { get; private set; }

    public UnitOfWork(IOptions<DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings.Value;
    }

    public void BeginTransaction()
    {
        Transaction = Connection.BeginTransaction();
    }

    public void Commit()
    {
        Transaction?.Commit();
        Dispose();
    }

    public void Rollback()
    {
        Transaction?.Rollback();
        Dispose();
    }

    public void Dispose()
    {
        Transaction?.Dispose();
    }
}