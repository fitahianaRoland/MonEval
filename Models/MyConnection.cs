using Npgsql;
using System;
namespace dotnetEval.Models;
public class MyConnectionException : Exception
{
    public MyConnectionException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class MyConnection
{
    private string? connectionString;
    private NpgsqlConnection connection;

    public object? Connection { get; internal set; }

    public MyConnection()
    {
        connectionString = "Host=localhost;Port=5432;Database=construction;Username=postgres;Password=106fitahiana";
        connection = new NpgsqlConnection(connectionString);
    }

    public void Open()
    {
        if (connection.State != System.Data.ConnectionState.Open)
        {
            connection.Open();
        }
    }

    public void Close()
    {
        if (connection.State != System.Data.ConnectionState.Closed)
        {
            connection.Close();
        }
    }

    public NpgsqlDataReader Execute(string query)
    {
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        try
        {
            return command.ExecuteReader();
        }
        catch (NpgsqlException ex)
        {
            // Lancer votre exception personnalisée avec un message d'erreur et une exception interne.
            throw new MyConnectionException("Erreur lors de l'exécution de la requête.", ex);
        }
    }

    public void ExecuteInsert(string query)
    {
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (NpgsqlException ex)
        {
            // Lancer votre exception personnalisée avec un message d'erreur et une exception interne.
            throw new MyConnectionException("Erreur lors de l'insertion des données.", ex);
        }
    }

    public T? ExecuteScalar<T>(string sql){
        using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
        {
            object? result = command.ExecuteScalar();
            if(result != DBNull.Value && result != null){
                return (T)result;
            }
            else{
                return default(T);
            }
        }
    }
}
