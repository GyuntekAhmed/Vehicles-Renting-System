using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiclesRenting.Data.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Cars" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Motorcycle" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Scooter" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "Color", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RegistrationNumber", "RenterId" },
                values: new object[,]
                {
                    { new Guid("26d29059-c4e9-4758-9d93-cff07d85ff25"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Mercedes", 1, "White", "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/5/55/Mercedes-Benz_W223_IMG_6663.jpg/1200px-Mercedes-Benz_W223_IMG_6663.jpg", "S-Class", 70m, "CB0832AP", null },
                    { new Guid("44196c29-7d1c-4dee-8d56-b39833062ada"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Ford Mustang", 1, "Gray", "Silistra, East", "https://upload.wikimedia.org/wikipedia/commons/f/f6/1967_Ford_Mustang_Shelby_GT-500_Eleanor.jpg", "GT500 Shelby", 200m, "CC0000CC", null },
                    { new Guid("aef33f88-dd3a-45ba-aa2b-2572e5cbab9e"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Hyundai", 1, "Black", "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/4/4b/Hyundai_Santa_Fe_%28TM%29_PHEV_FL_IMG_6648.jpg", "Santa fe", 40m, "CC1835AX", new Guid("3d2d9e6d-038b-4ff5-90b8-4eebc4c48426") }
                });

            migrationBuilder.InsertData(
                table: "Motorcycles",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "Color", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RegistrationNumber", "RenterId" },
                values: new object[,]
                {
                    { new Guid("3732d1b9-30f0-42e5-a2a1-c30d411f7cd1"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Honda", 2, "Red", "Ruse, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/06/Honda750NR.jpg/1200px-Honda750NR.jpg", "NR-L", 30m, "CC3552OB", new Guid("3d2d9e6d-038b-4ff5-90b8-4eebc4c48426") },
                    { new Guid("89c4f559-5494-460f-81a3-9398d2cd10d3"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Suzuki", 2, "Blue", "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/GSXR1000K5.jpg/640px-GSXR1000K5.jpg", "GSX-R1000K5", 75m, "CC1550AB", null },
                    { new Guid("96994854-db0d-403f-af85-da66bcde294b"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Harley-Davidson", 2, "Gray", "Silistra, West", "https://upload.wikimedia.org/wikipedia/commons/1/13/Harley_5-06.jpg", "VRSC", 110m, "CC1200AK", null }
                });

            migrationBuilder.InsertData(
                table: "Scooters",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "CurrentAddress", "ImageUrl", "PricePerDay", "RenterId" },
                values: new object[,]
                {
                    { new Guid("0c589107-6bed-4e05-b43c-5465140bfaa2"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Inmotion", 3, "Silistra, West", "https://cdn.shopify.com/s/files/1/0021/7389/4702/products/LeMotion-Web-1.jpg?v=1636106454", 9m, null },
                    { new Guid("63fe1ba5-b370-4550-b11d-fea51b7b8ff9"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "E-scooter", 3, "Silistra, North", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/29/Elektrische-tretroller.jpg/800px-Elektrische-tretroller.jpg", 7m, null },
                    { new Guid("c4763f6b-ab41-480c-8805-93ddc03cef42"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Xiomi", 3, "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Xiaomi_M365.jpg/1200px-Xiaomi_M365.jpg", 12m, new Guid("3d2d9e6d-038b-4ff5-90b8-4eebc4c48426") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("26d29059-c4e9-4758-9d93-cff07d85ff25"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("44196c29-7d1c-4dee-8d56-b39833062ada"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("aef33f88-dd3a-45ba-aa2b-2572e5cbab9e"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("3732d1b9-30f0-42e5-a2a1-c30d411f7cd1"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("89c4f559-5494-460f-81a3-9398d2cd10d3"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("96994854-db0d-403f-af85-da66bcde294b"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("0c589107-6bed-4e05-b43c-5465140bfaa2"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("63fe1ba5-b370-4550-b11d-fea51b7b8ff9"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("c4763f6b-ab41-480c-8805-93ddc03cef42"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
