using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIModels.Migrations
{
    /// <inheritdoc />
    public partial class WebAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create or alter view [dbo].[EmployeesViews] as 
                select e.EmployeeId, e.Name as EmployeeName, e.Age, e.Position, c.Name as CompanyName, c.Address, c.Country 
                from Employees e inner join Companies c on c.CompanyId = e.CompanyId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view EmployeesViews'");
        }
    }
}
