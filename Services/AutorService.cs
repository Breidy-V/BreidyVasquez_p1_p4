using Dapper;
using Microsoft.Data.Sqlite;
using BreidyVasquez_p1_p4.Api.Models;

namespace BreidyVasquez_p1_p4.Api.Services;

public class AutorService
{
    private readonly string _connectionString;

    public AutorService(IWebHostEnvironment environment)
    {
        var databasePath = Path.Combine(
            environment.ContentRootPath,
            "autores.db"
        );

        _connectionString = $"Data Source={databasePath}";
    }

    private SqliteConnection CreateConnection =>
        new SqliteConnection(_connectionString);

    public async Task InitializeAsync()
    {
        const string query = """
            CREATE TABLE IF NOT EXISTS Autores (
                IdAutor INTEGER PRIMARY KEY AUTOINCREMENT,
                Nombres TEXT NOT NULL,
                Nacionalidad TEXT NOT NULL,
                FechaNacimiento TEXT NOT NULL,
                Sueldo REAL NOT NULL
            );
            """;

        using var connection = CreateConnection;

        await connection.ExecuteAsync(query);
    }

    public async Task<IEnumerable<Autor>> GetAllAsync()
    {
        const string query = """
            SELECT
                IdAutor,
                Nombres,
                Nacionalidad,
                FechaNacimiento,
                Sueldo
            FROM Autores;
            """;

        using var connection = CreateConnection;

        return await connection.QueryAsync<Autor>(query);
    }

    public async Task<Autor?> GetByIdAsync(int id)
    {
        const string query = """
            SELECT
                IdAutor,
                Nombres,
                Nacionalidad,
                FechaNacimiento,
                Sueldo
            FROM Autores
            WHERE IdAutor = @IdAutor;
            """;

        using var connection = CreateConnection;

        return await connection.QueryFirstOrDefaultAsync<Autor>(
            query,
            new { IdAutor = id }
        );
    }

    public async Task<int> CreateAsync(Autor autor)
    {
        const string query = """
            INSERT INTO Autores
                (Nombres, Nacionalidad, FechaNacimiento, Sueldo)
            VALUES
                (@Nombres, @Nacionalidad, @FechaNacimiento, @Sueldo);

            SELECT last_insert_rowid();
            """;

        using var connection = CreateConnection;

        return await connection.ExecuteScalarAsync<int>(
            query,
            autor
        );
    }

    public async Task<bool> UpdateAsync(int id, Autor autor)
    {
        const string query = """
            UPDATE Autores
            SET
                Nombres = @Nombres,
                Nacionalidad = @Nacionalidad,
                FechaNacimiento = @FechaNacimiento,
                Sueldo = @Sueldo
            WHERE IdAutor = @IdAutor;
            """;

        using var connection = CreateConnection;

        int rowsAffected = await connection.ExecuteAsync(
            query,
            new
            {
                IdAutor = id,
                autor.Nombres,
                autor.Nacionalidad,
                autor.FechaNacimiento,
                autor.Sueldo
            }
        );

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string query = """
            DELETE FROM Autores
            WHERE IdAutor = @IdAutor;
            """;

        using var connection = CreateConnection;

        int rowsAffected = await connection.ExecuteAsync(
            query,
            new { IdAutor = id }
        );

        return rowsAffected > 0;
    }
}