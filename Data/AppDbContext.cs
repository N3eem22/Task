using Microsoft.Data.SqlClient;

namespace WinFormsApp1.Data;

public static class AppDbContext
{
    private static string? _connectionString;

    /// <summary>
    /// Connection string for SQL Server. Set before first use, or a default is used (LocalDB).
    /// Example: "Server=(localdb)\\MSSQLLocalDB;Database=ErpDb;Integrated Security=True;TrustServerCertificate=True;"
    /// </summary>
    public static string ConnectionString
    {
        get
        {
            if (_connectionString != null) return _connectionString;
            _connectionString = "Server=.;Database=ErpDb;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True;";
            return _connectionString;
        }
        set => _connectionString = value;
    }

    public static void EnsureCreated()
    {
        EnsureDatabaseExists();
        using var conn = new SqlConnection(ConnectionString);
        conn.Open();

        if (!TableExists(conn, "Customers"))
        {
            Execute(conn, """
                CREATE TABLE Customers (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Name NVARCHAR(200) NOT NULL,
                    Address NVARCHAR(500) NULL,
                    Phone NVARCHAR(50) NULL,
                    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
                );
                """);
        }
        if (!TableExists(conn, "Products"))
        {
            Execute(conn, """
                CREATE TABLE Products (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Name NVARCHAR(200) NOT NULL,
                    UnitPrice DECIMAL(18,2) NOT NULL DEFAULT 0,
                    Description NVARCHAR(500) NULL,
                    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
                );
                """);
        }
        if (!TableExists(conn, "Invoices"))
        {
            Execute(conn, """
                CREATE TABLE Invoices (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    InvoiceNumber NVARCHAR(50) NOT NULL UNIQUE,
                    CustomerId INT NOT NULL,
                    InvoiceDate DATE NOT NULL DEFAULT CAST(GETUTCDATE() AS DATE),
                    SubTotal DECIMAL(18,2) NOT NULL DEFAULT 0,
                    TaxPercent DECIMAL(5,2) NOT NULL DEFAULT 0,
                    TaxAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
                    Total DECIMAL(18,2) NOT NULL DEFAULT 0,
                    Notes NVARCHAR(1000) NULL,
                    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
                    CONSTRAINT FK_Invoices_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
                );
                CREATE INDEX IX_Invoices_CustomerId ON Invoices(CustomerId);
                CREATE INDEX IX_Invoices_InvoiceNumber ON Invoices(InvoiceNumber);
                """);
        }
        if (!TableExists(conn, "InvoiceLines"))
        {
            Execute(conn, """
                CREATE TABLE InvoiceLines (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    InvoiceId INT NOT NULL,
                    ProductId INT NOT NULL,
                    Quantity DECIMAL(18,2) NOT NULL DEFAULT 1,
                    UnitPrice DECIMAL(18,2) NOT NULL DEFAULT 0,
                    LineTotal DECIMAL(18,2) NOT NULL DEFAULT 0,
                    CONSTRAINT FK_InvoiceLines_Invoices FOREIGN KEY (InvoiceId) REFERENCES Invoices(Id) ON DELETE CASCADE,
                    CONSTRAINT FK_InvoiceLines_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
                );
                CREATE INDEX IX_InvoiceLines_InvoiceId ON InvoiceLines(InvoiceId);
                """);
        }

        SeedIfEmpty(conn);
    }

    /// <summary>
    /// Creates the database if it does not exist (uses master for the check).
    /// Only works when ConnectionString points to a local/default server (e.g. LocalDB).
    /// </summary>
    private static void EnsureDatabaseExists()
    {
        var builder = new SqlConnectionStringBuilder(ConnectionString);
        var database = builder.InitialCatalog;
        if (string.IsNullOrEmpty(database)) return;
        builder.InitialCatalog = "master";
        using var conn = new SqlConnection(builder.ConnectionString);
        conn.Open();
        using var cmd = new SqlCommand(
            "SELECT 1 FROM sys.databases WHERE name = @name", conn);
        cmd.Parameters.AddWithValue("@name", database);
        if (cmd.ExecuteScalar() != null) return;
        using var createDb = new SqlCommand($"CREATE DATABASE [{database}]", conn);
        createDb.ExecuteNonQuery();
    }

    private static bool TableExists(SqlConnection conn, string tableName)
    {
        using var cmd = new SqlCommand(
            "SELECT 1 FROM sys.tables WHERE name = @name", conn);
        cmd.Parameters.AddWithValue("@name", tableName);
        return cmd.ExecuteScalar() != null;
    }

    private static void Execute(SqlConnection conn, string sql)
    {
        using var cmd = new SqlCommand(sql, conn);
        cmd.ExecuteNonQuery();
    }

    private static void SeedIfEmpty(SqlConnection conn)
    {
        using var check = new SqlCommand("SELECT COUNT(*) FROM Customers", conn);
        if (Convert.ToInt32(check.ExecuteScalar()) > 0) return;

        Execute(conn, """
            SET IDENTITY_INSERT Customers ON;
            INSERT INTO Customers (Id, Name, Address, Phone) VALUES
            (1, N'Acme Corp', N'123 Main St', N'555-0100'),
            (2, N'Globex Inc', N'456 Oak Ave', N'555-0200'),
            (3, N'Initech', N'789 Tech Park', N'555-0300');
            SET IDENTITY_INSERT Customers OFF;
            """);
        Execute(conn, """
            SET IDENTITY_INSERT Products ON;
            INSERT INTO Products (Id, Name, UnitPrice, Description) VALUES
            (1, N'Widget A', 29.99, N'Standard widget'),
            (2, N'Widget B', 49.99, N'Premium widget'),
            (3, N'Gadget X', 99.99, N'Advanced gadget'),
            (4, N'Service Pack', 19.99, N'Support package');
            SET IDENTITY_INSERT Products OFF;
            """);
    }
}
