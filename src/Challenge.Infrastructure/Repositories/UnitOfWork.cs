using Challenge.Domain.Interfaces;
using Challenge.Infrastructure.Configurations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Challenge.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public SqlConnection Connection { get; }
    
    public SqlTransaction? Transaction { get; private set; }
    
    private readonly DatabaseSettings _databaseSettings;

    public UnitOfWork(IOptions<DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings.Value;

        Connection = new SqlConnection(_databaseSettings.DefaultConnection);
    }

    public void BeginTransaction()
    {
        if (Connection.State != ConnectionState.Open)
            Connection.Open();

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
        Connection.Dispose();
    }
}