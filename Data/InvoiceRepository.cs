using Microsoft.Data.SqlClient;
using WinFormsApp1.Models;

namespace WinFormsApp1.Data;

public static class InvoiceRepository
{
    public static string GetNextInvoiceNumber()
    {
        using var conn = new SqlConnection(AppDbContext.ConnectionString);
        conn.Open();
        using var cmd = new SqlCommand(
            "SELECT ISNULL(MAX(TRY_CAST(SUBSTRING(InvoiceNumber, 5, 50) AS INT)), 0) + 1 FROM Invoices WHERE InvoiceNumber LIKE 'INV-%'", conn);
        var next = Convert.ToInt32(cmd.ExecuteScalar());
        return $"INV-{next:D5}";
    }

    public static List<Invoice> GetAll()
    {
        var list = new List<Invoice>();
        using var conn = new SqlConnection(AppDbContext.ConnectionString);
        conn.Open();
        using var cmd = new SqlCommand("""
            SELECT i.Id, i.InvoiceNumber, i.CustomerId, i.InvoiceDate, i.SubTotal, i.TaxPercent, i.TaxAmount, i.Total, i.CreatedAt, i.Notes, c.Name
            FROM Invoices i
            LEFT JOIN Customers c ON c.Id = i.CustomerId
            ORDER BY i.CreatedAt DESC
            """, conn);
        using var r = cmd.ExecuteReader();
        while (r.Read())
            list.Add(new Invoice
            {
                Id = r.GetInt32(0),
                InvoiceNumber = r.GetString(1),
                CustomerId = r.GetInt32(2),
                InvoiceDate = r.GetDateTime(3),
                SubTotal = r.GetDecimal(4),
                TaxPercent = r.GetDecimal(5),
                TaxAmount = r.GetDecimal(6),
                Total = r.GetDecimal(7),
                CreatedAt = r.GetDateTime(8),
                Notes = r.IsDBNull(9) ? null : r.GetString(9),
                CustomerName = r.IsDBNull(10) ? "" : r.GetString(10)
            });
        return list;
    }

    public static Invoice? GetById(int id)
    {
        using var conn = new SqlConnection(AppDbContext.ConnectionString);
        conn.Open();
        using var cmd = new SqlCommand("""
            SELECT Id, InvoiceNumber, CustomerId, InvoiceDate, SubTotal, TaxPercent, TaxAmount, Total, CreatedAt, Notes
            FROM Invoices WHERE Id = @id
            """, conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var r = cmd.ExecuteReader();
        if (!r.Read()) return null;
        var inv = new Invoice
        {
            Id = r.GetInt32(0),
            InvoiceNumber = r.GetString(1),
            CustomerId = r.GetInt32(2),
            InvoiceDate = r.GetDateTime(3),
            SubTotal = r.GetDecimal(4),
            TaxPercent = r.GetDecimal(5),
            TaxAmount = r.GetDecimal(6),
            Total = r.GetDecimal(7),
            CreatedAt = r.GetDateTime(8),
            Notes = r.IsDBNull(9) ? null : r.GetString(9)
        };
        var cust = CustomerRepository.GetById(inv.CustomerId);
        inv.CustomerName = cust?.Name ?? "";
        inv.Lines = GetLinesByInvoiceId(conn, id);
        return inv;
    }

    private static List<InvoiceLine> GetLinesByInvoiceId(SqlConnection conn, int invoiceId)
    {
        var list = new List<InvoiceLine>();
        using var cmd = new SqlCommand("""
            SELECT l.Id, l.InvoiceId, l.ProductId, p.Name, l.UnitPrice, l.Quantity, l.LineTotal
            FROM InvoiceLines l
            JOIN Products p ON p.Id = l.ProductId
            WHERE l.InvoiceId = @id
            """, conn);
        cmd.Parameters.AddWithValue("@id", invoiceId);
        using var r = cmd.ExecuteReader();
        while (r.Read())
            list.Add(new InvoiceLine
            {
                Id = r.GetInt32(0),
                InvoiceId = r.GetInt32(1),
                ProductId = r.GetInt32(2),
                ProductName = r.GetString(3),
                UnitPrice = r.GetDecimal(4),
                Quantity = r.GetDecimal(5),
                LineTotal = r.GetDecimal(6)
            });
        return list;
    }

    public static int Insert(Invoice invoice)
    {
        using var conn = new SqlConnection(AppDbContext.ConnectionString);
        conn.Open();
        using var trans = conn.BeginTransaction();
        try
        {
            using var cmd = new SqlCommand("""
                INSERT INTO Invoices (InvoiceNumber, CustomerId, InvoiceDate, SubTotal, TaxPercent, TaxAmount, Total, Notes)
                VALUES (@num, @cid, @date, @sub, @taxPct, @taxAmt, @total, @notes);
                SELECT CAST(SCOPE_IDENTITY() AS INT);
                """, conn, trans);
            cmd.Parameters.AddWithValue("@num", invoice.InvoiceNumber);
            cmd.Parameters.AddWithValue("@cid", invoice.CustomerId);
            cmd.Parameters.AddWithValue("@date", invoice.InvoiceDate);
            cmd.Parameters.AddWithValue("@sub", invoice.SubTotal);
            cmd.Parameters.AddWithValue("@taxPct", invoice.TaxPercent);
            cmd.Parameters.AddWithValue("@taxAmt", invoice.TaxAmount);
            cmd.Parameters.AddWithValue("@total", invoice.Total);
            cmd.Parameters.AddWithValue("@notes", (object?)invoice.Notes ?? DBNull.Value);
            var id = Convert.ToInt32(cmd.ExecuteScalar());
            foreach (var line in invoice.Lines)
            {
                using var lineCmd = new SqlCommand("""
                    INSERT INTO InvoiceLines (InvoiceId, ProductId, Quantity, UnitPrice, LineTotal)
                    VALUES (@invId, @pid, @qty, @up, @lt)
                    """, conn, trans);
                lineCmd.Parameters.AddWithValue("@invId", id);
                lineCmd.Parameters.AddWithValue("@pid", line.ProductId);
                lineCmd.Parameters.AddWithValue("@qty", line.Quantity);
                lineCmd.Parameters.AddWithValue("@up", line.UnitPrice);
                lineCmd.Parameters.AddWithValue("@lt", line.LineTotal);
                lineCmd.ExecuteNonQuery();
            }
            trans.Commit();
            return id;
        }
        catch
        {
            trans.Rollback();
            throw;
        }
    }

    public static void Update(Invoice invoice)
    {
        using var conn = new SqlConnection(AppDbContext.ConnectionString);
        conn.Open();
        using var trans = conn.BeginTransaction();
        try
        {
            using var cmd = new SqlCommand("""
                UPDATE Invoices SET CustomerId=@cid, InvoiceDate=@date, SubTotal=@sub, TaxPercent=@taxPct, TaxAmount=@taxAmt, Total=@total, Notes=@notes
                WHERE Id=@id
                """, conn, trans);
            cmd.Parameters.AddWithValue("@id", invoice.Id);
            cmd.Parameters.AddWithValue("@cid", invoice.CustomerId);
            cmd.Parameters.AddWithValue("@date", invoice.InvoiceDate);
            cmd.Parameters.AddWithValue("@sub", invoice.SubTotal);
            cmd.Parameters.AddWithValue("@taxPct", invoice.TaxPercent);
            cmd.Parameters.AddWithValue("@taxAmt", invoice.TaxAmount);
            cmd.Parameters.AddWithValue("@total", invoice.Total);
            cmd.Parameters.AddWithValue("@notes", (object?)invoice.Notes ?? DBNull.Value);
            cmd.ExecuteNonQuery();
            using var delLines = new SqlCommand("DELETE FROM InvoiceLines WHERE InvoiceId = @id", conn, trans);
            delLines.Parameters.AddWithValue("@id", invoice.Id);
            delLines.ExecuteNonQuery();
            foreach (var line in invoice.Lines)
            {
                using var lineCmd = new SqlCommand("""
                    INSERT INTO InvoiceLines (InvoiceId, ProductId, Quantity, UnitPrice, LineTotal)
                    VALUES (@invId, @pid, @qty, @up, @lt)
                    """, conn, trans);
                lineCmd.Parameters.AddWithValue("@invId", invoice.Id);
                lineCmd.Parameters.AddWithValue("@pid", line.ProductId);
                lineCmd.Parameters.AddWithValue("@qty", line.Quantity);
                lineCmd.Parameters.AddWithValue("@up", line.UnitPrice);
                lineCmd.Parameters.AddWithValue("@lt", line.LineTotal);
                lineCmd.ExecuteNonQuery();
            }
            trans.Commit();
        }
        catch
        {
            trans.Rollback();
            throw;
        }
    }

    public static void Delete(int id)
    {
        using var conn = new SqlConnection(AppDbContext.ConnectionString);
        conn.Open();
        using var cmd = new SqlCommand("DELETE FROM Invoices WHERE Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }
}
