using Microsoft.Data.SqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.Data;

public static class CustomerRepository
{
    public static List<Customer> GetAll()
    {
        var list = new List<Customer>();
        using var conn = new SqlConnection(AppDbContext.ConnectionString);
        conn.Open();
        using var cmd = new SqlCommand("SELECT Id, Name, Address, Phone FROM Customers ORDER BY Name", conn);
        using var r = cmd.ExecuteReader();
        while (r.Read())
            list.Add(new Customer
            {
                Id = r.GetInt32(0),
                Name = r.GetString(1),
                Address = r.IsDBNull(2) ? null : r.GetString(2),
                Phone = r.IsDBNull(3) ? null : r.GetString(3)
            });
        return list;
    }

    public static Customer? GetById(int id)
    {
        using var conn = new SqlConnection(AppDbContext.ConnectionString);
        conn.Open();
        using var cmd = new SqlCommand("SELECT Id, Name, Address, Phone FROM Customers WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var r = cmd.ExecuteReader();
        if (!r.Read()) return null;
        return new Customer
        {
            Id = r.GetInt32(0),
            Name = r.GetString(1),
            Address = r.IsDBNull(2) ? null : r.GetString(2),
            Phone = r.IsDBNull(3) ? null : r.GetString(3)
        };
    }
}
