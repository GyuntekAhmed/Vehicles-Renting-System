using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiclesRenting.Data.Migrations
{
    public partial class AddCreatedOncolumnToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Scooters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 25, 20, 27, 10, 894, DateTimeKind.Utc).AddTicks(194));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Motorcycles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 25, 20, 27, 10, 893, DateTimeKind.Utc).AddTicks(3527));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 25, 20, 27, 10, 892, DateTimeKind.Utc).AddTicks(5678));

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "Color", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RegistrationNumber", "RenterId" },
                values: new object[,]
                {
                    { new Guid("1df5bb39-9b46-405b-84ee-ef47bbae46de"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Hyundai", 1, "Black", "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/4/4b/Hyundai_Santa_Fe_%28TM%29_PHEV_FL_IMG_6648.jpg", "Santa fe", 40m, "CC1835AX", new Guid("3d2d9e6d-038b-4ff5-90b8-4eebc4c48426") },
                    { new Guid("2e3a6e04-25bb-462c-a832-f3823c107081"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Ford Mustang", 1, "Gray", "Silistra, East", "https://upload.wikimedia.org/wikipedia/commons/f/f6/1967_Ford_Mustang_Shelby_GT-500_Eleanor.jpg", "GT500 Shelby", 200m, "CC0000CC", null },
                    { new Guid("8159cf93-5835-4334-b523-5a6bdd37285d"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Mercedes", 1, "White", "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/5/55/Mercedes-Benz_W223_IMG_6663.jpg/1200px-Mercedes-Benz_W223_IMG_6663.jpg", "S-Class", 70m, "CB0832AP", null }
                });

            migrationBuilder.InsertData(
                table: "Motorcycles",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "Color", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RegistrationNumber", "RenterId" },
                values: new object[,]
                {
                    { new Guid("25177135-9450-4427-a45f-3af09b6286cf"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Suzuki", 2, "Blue", "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/GSXR1000K5.jpg/640px-GSXR1000K5.jpg", "GSX-R1000K5", 75m, "CC1550AB", null },
                    { new Guid("2aa01878-7eb9-462f-9683-792f254dc287"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Honda", 2, "Red", "Ruse, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/06/Honda750NR.jpg/1200px-Honda750NR.jpg", "NR-L", 30m, "CC3552OB", new Guid("3d2d9e6d-038b-4ff5-90b8-4eebc4c48426") },
                    { new Guid("867573a9-0f05-46ef-9913-8cc00b1b5b41"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Harley-Davidson", 2, "Gray", "Silistra, West", "https://upload.wikimedia.org/wikipedia/commons/1/13/Harley_5-06.jpg", "VRSC", 110m, "CC1200AK", null }
                });

            migrationBuilder.InsertData(
                table: "Scooters",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "CurrentAddress", "ImageUrl", "PricePerDay", "RenterId" },
                values: new object[,]
                {
                    { new Guid("43566f0c-0812-4100-8a99-3157813a7bf7"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "E-scooter", 3, "Silistra, North", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/29/Elektrische-tretroller.jpg/800px-Elektrische-tretroller.jpg", 7m, null },
                    { new Guid("664304e9-2e09-4bda-9a41-4c2bac441aec"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Xiomi", 3, "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Xiaomi_M365.jpg/1200px-Xiaomi_M365.jpg", 12m, new Guid("3d2d9e6d-038b-4ff5-90b8-4eebc4c48426") },
                    { new Guid("d1f46155-f33b-47d0-b7c7-9a248ef3167b"), new Guid("b18534f1-c32b-401f-b740-6035cb456174"), "Inmotion", 3, "Silistra, West", "https://cdn.shopify.com/s/files/1/0021/7389/4702/products/LeMotion-Web-1.jpg?v=1636106454", 9m, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("1df5bb39-9b46-405b-84ee-ef47bbae46de"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("2e3a6e04-25bb-462c-a832-f3823c107081"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("8159cf93-5835-4334-b523-5a6bdd37285d"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("25177135-9450-4427-a45f-3af09b6286cf"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("2aa01878-7eb9-462f-9683-792f254dc287"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("867573a9-0f05-46ef-9913-8cc00b1b5b41"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("43566f0c-0812-4100-8a99-3157813a7bf7"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("664304e9-2e09-4bda-9a41-4c2bac441aec"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("d1f46155-f33b-47d0-b7c7-9a248ef3167b"));

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Scooters");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Motorcycles");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Cars");

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
    }
}
