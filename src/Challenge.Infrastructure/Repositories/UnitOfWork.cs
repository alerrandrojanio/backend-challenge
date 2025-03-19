using Challenge.Domain.Interfaces;
using Challenge.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace Challenge.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public MySqlConnection Connection { get; }
    public MySqlTransaction? Transaction { get; private set; }

    public UnitOfWork(IOptions<DatabaseSettings> databaseSettings)
    {
        Connection = new MySqlConnection(databaseSettings.Value.DefaultConnection);
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