using System;
using System.Data;
using System.Data.SqlClient;

namespace MQWebApplication.DBhelpler;

public partial interface IQueryHelperSqlserver
{
    #region Methods

    string GetString();

    DataSet QueryDataSetSql(string sql);

    #endregion Methods
}

public partial class QueryHelperSqlserver : IQueryHelperSqlserver
{
    #region Fields

    private SqlServerDatabase _dbServerDatabase;

    #endregion Fields

    #region Constructors

    public QueryHelperSqlserver(SqlServerDatabase database)
    {
        _dbServerDatabase = database;
    }

    #endregion Constructors

    #region Methods

    public string GetString()
    {
        return "123123";
    }

    public DataSet QueryDataSetSql(string sql)
    {
        // var command = _dbServerDatabase.CreateCommand();
        // command.CommandText = sql;
        // var adapter = new SqlDataAdapter(command);
        // var ds = new DataSet();
        // adapter.Fill(ds);
        // return ds;
        // *** 关键改动 ***
        // 每次查询都创建一个新的连接和命令。
        // using 语句确保它们在使用后被正确关闭和释放，
        // 即使发生异常也是如此。
        using var connection = _dbServerDatabase.CreateAndOpenConnection();
        using var command = connection.CreateCommand();

        command.CommandText = sql;
        var adapter = new SqlDataAdapter(command);
        var ds = new DataSet();
        adapter.Fill(ds);
        return ds;
    }

    #endregion Methods
}

// MQWebApplication/DBhelpler/SqlServerDatabase.cs

/// <summary>
/// 这个类现在只负责持有连接字符串。
/// </summary>
public class SqlServerDatabase
{
    public string ConnectionString { get; }

    public SqlServerDatabase(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }
        ConnectionString = connectionString;
    }

    /// <summary>
    /// 提供一个工厂方法来创建和打开一个新的连接。
    /// 这可以确保每次操作都使用新的连接。
    /// </summary>
    public SqlConnection CreateAndOpenConnection()
    {
        var connection = new SqlConnection(ConnectionString);
        connection.Open();
        return connection;
    }
}



// public class SqlServerDatabase : IDisposable
// {
//     #region Fields

//     private SqlConnection _connection;

//     #endregion Fields

//     #region Constructors

//     public SqlServerDatabase(string connectionString)
//     {
//         _connection = new SqlConnection(connectionString);
//         _connection.Open();
//     }

//     #endregion Constructors

//     #region Methods

//     public SqlCommand CreateCommand()
//     {
//         var cmd = _connection.CreateCommand();
//         return cmd;
//     }

//     public void Dispose()
//     {
//         _connection.Close();
//         _connection.Dispose();
//     }

//     #endregion Methods
// }

//public class Startup
//{
//    #region Methods

// public void ConfigureServices(IServiceCollection services) {
// services.AddSingleton<SqlServerDatabase>(new SqlServerDatabase("Your Connection String"));
// services.AddTransient<QueryHelper>(); }

//    #endregion Methods
//}