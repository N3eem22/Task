# Sales Invoice – Desktop ERP

A simple **Sales Invoice** screen for a Windows Forms ERP-style application. It lets you create, edit, and delete invoices; choose a customer; add multiple line items with quantities; and see subtotal, tax, and total calculated automatically.

## Features

- **Create** a new invoice (invoice number is generated automatically, e.g. `INV-00001`)
- **Select a customer** (required; dropdown populated from the database)
- **Add multiple items** via “Add Item” (product + quantity); unit price and line total are calculated
- **Enter quantities** per line (decimal supported)
- **Automatic totals**: Subtotal, tax %, tax amount, and total update as you add/remove lines or change tax %
- **Save, edit, and delete** invoices from the main list

## Requirements Met

- Invoice number is **generated automatically** (format `INV-00001`, incrementing)
- **Customer selection is required** before saving
- **At least one line item** is required before saving
- **Totals** are calculated from line totals and tax %

## How to Run

### Prerequisites

- **.NET 8 SDK**  
  - Check: `dotnet --version` (should be 8.x).  
  - Install from: https://dotnet.microsoft.com/download/dotnet/8.0

### Run the application

1. Open the solution/project folder in your IDE or a terminal.
2. Restore and run:

   ```bash
   dotnet restore
   dotnet run
   ```

3. The main form opens with the **Sales Invoices** list (initially empty or with existing data).
4. Use **New Invoice** to open the Sales Invoice form, pick a customer, add at least one item, then **Save**.
5. Use **Edit** or **Delete** on the selected row to modify or remove an invoice.

### Database (SQL Server / MSSQL)

The app uses **Microsoft SQL Server** (MSSQL). By default it connects to **LocalDB** with database name `ErpDb`:

- **Default connection string**: `Server=(localdb)\\MSSQLLocalDB;Database=ErpDb;Integrated Security=True;TrustServerCertificate=True;`

**First-time setup:**

1. Ensure SQL Server or **LocalDB** is installed (e.g. with Visual Studio or [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads)).
2. Create the database and tables either:
   - **Option A**: Run **`Scripts/CreateDatabase.sql`** in SQL Server Management Studio (SSMS) or Azure Data Studio. Connect to `(localdb)\MSSQLLocalDB`, create a new database `ErpDb` if needed, then run the script in that database.
   - **Option B**: Let the app create tables automatically: create an empty database named `ErpDb` (e.g. in SSMS: right-click Databases → New Database → name: `ErpDb`). On first run, the app will create the tables and seed sample data if empty.

To use a different server or database, set the connection string before the first database call (e.g. in `Program.cs`):

```csharp
WinFormsApp1.Data.AppDbContext.ConnectionString = "Server=.;Database=YourDb;Integrated Security=True;TrustServerCertificate=True;";
```

## Database Script

The schema and optional seed data are in:

- **`Scripts/CreateDatabase.sql`** (T-SQL for SQL Server)

Run this script in SSMS or sqlcmd against your SQL Server instance. Create the database `ErpDb` first (or change the script/database name), then execute the script in that database. The application also creates the same schema automatically on startup if the tables do not exist.

### Tables

- **Customers** – Id, Name, Address, Phone  
- **Products** – Id, Name, UnitPrice, Description  
- **Invoices** – Id, InvoiceNumber, CustomerId, InvoiceDate, SubTotal, TaxPercent, TaxAmount, Total, Notes  
- **InvoiceLines** – Id, InvoiceId, ProductId, Quantity, UnitPrice, LineTotal  

Sample customers and products are inserted on first run when the database is empty.

## Project Structure (source code)

- **Program.cs** – Entry point; starts the main form.
- **Form1** – Main screen: invoice list (grid) + New / Edit / Delete.
- **SalesInvoiceForm** – Create/Edit invoice: customer, date, notes, line items, totals, Save/Cancel.
- **AddLineDialog** – Dialog to pick a product and quantity for a new line.
- **Models/** – `Customer`, `Product`, `Invoice`, `InvoiceLine`.
- **Data/** – `AppDbContext` (connection + schema creation), `CustomerRepository`, `ProductRepository`, `InvoiceRepository`.
- **Scripts/CreateDatabase.sql** – SQL Server schema and optional seed script.

## Technology

- **.NET 8**, **Windows Forms**
- **Microsoft SQL Server (MSSQL)** via **Microsoft.Data.SqlClient**
- Default: **LocalDB** with database `ErpDb`; connection string is configurable.
