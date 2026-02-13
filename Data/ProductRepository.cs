using Microsoft.Data.SqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.Data;

public static class ProductRepository
{
    public static List<Product> GetAll()
    {
        var list = new List<Product>();
        using var conn = new SqlConnection(AppDbContext.ConnectionString);
        conn.Open();
        using var cmd = new SqlCommand("SELECT Id, Name, UnitPrice, Description FROM Products ORDER BY Name", conn);
        using var r = cmd.ExecuteReader();
        while (r.Read())
            list.Add(new Product
            {
                Id = r.GetInt32(0),
                Name = r.GetString(1),
                UnitPrice = r.GetDecimal(2),
                Description = r.IsDBNull(3) ? null : r.GetString(3)
            });
        return list;
    }

    public static Product? GetById(int id)
    {
        using var conn = new SqlConnection(AppDbContext.ConnectionString);
        conn.Open();
        using var cmd = new SqlCommand("SELECT Id, Name, UnitPrice, Description FROM Products WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var r = cmd.ExecuteReader();
        if (!r.Read()) return null;
        return new Product
        {
            Id = r.GetInt32(0),
            Name = r.GetString(1),
            UnitPrice = r.GetDecimal(2),
            Description = r.IsDBNull(3) ? null : r.GetString(3)
        };
    }
}
