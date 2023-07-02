using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiclesRenting.Data.Migrations
{
    public partial class SetCategoryIdToDefaultForEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("5845fbe2-cca9-4e4f-8fc2-a3a14b3fb415"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("b5bb78e2-862b-4e34-9481-3e930ee8d8d7"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("cec346e5-834b-46da-a378-992f4c27cef8"));

            migrationBuilder.DeleteData(
                table: "Jets",
                keyColumn: "Id",
                keyValue: new Guid("677f095d-8cb3-4731-9b52-6a6e679a089b"));

            migrationBuilder.DeleteData(
                table: "Jets",
                keyColumn: "Id",
                keyValue: new Guid("86b7b3ab-820f-4237-ab1d-73e83f8afd1c"));

            migrationBuilder.DeleteData(
                table: "Jets",
                keyColumn: "Id",
                keyValue: new Guid("e3f04801-73ec-49fc-ad16-ddd533a1ac0d"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("2934d582-0e8f-4068-91a9-5185073e2d53"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("8b5497c7-1c53-490b-92a3-74acf0d85870"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("c616faac-a700-44bd-ac51-83a0bf662cee"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("08832e29-74ca-4405-9e93-c7070de395da"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("2aff1642-c786-4cb7-87f3-2e1a6a6693c4"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("ff64d0a6-da6a-4330-9fe6-7b3c23bb604e"));

            migrationBuilder.DeleteData(
                table: "Yachts",
                keyColumn: "Id",
                keyValue: new Guid("146bcd6c-7269-4b0c-9067-c53ea0539d36"));

            migrationBuilder.DeleteData(
                table: "Yachts",
                keyColumn: "Id",
                keyValue: new Guid("7721c73b-d914-4baf-ba05-261bb81b14cc"));

            migrationBuilder.DeleteData(
                table: "Yachts",
                keyColumn: "Id",
                keyValue: new Guid("ddf76627-1b21-475e-b3f4-29e252d1bd50"));

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Yachts",
                type: "int",
                nullable: false,
                defaultValue: 5,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Scooters",
                type: "int",
                nullable: false,
                defaultValue: 3,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Motorcycles",
                type: "int",
                nullable: false,
                defaultValue: 2,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Jets",
                type: "int",
                nullable: false,
                defaultValue: 4,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AgentId", "Brand", "Color", "CreatedOn", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RegistrationNumber", "RenterId" },
                values: new object[,]
                {
                    { new Guid("16c47ced-2fd4-4ffc-8769-06e4e8059e8f"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Hyundai", "Black", new DateTime(2023, 7, 2, 12, 38, 47, 569, DateTimeKind.Utc).AddTicks(6369), "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/4/4b/Hyundai_Santa_Fe_%28TM%29_PHEV_FL_IMG_6648.jpg", "Santa fe", 40m, "CC1835AX", new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") },
                    { new Guid("40b74307-95ec-41d2-8ff1-c879006a171b"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Mercedes", "White", new DateTime(2023, 7, 2, 12, 38, 47, 569, DateTimeKind.Utc).AddTicks(6573), "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/5/55/Mercedes-Benz_W223_IMG_6663.jpg/1200px-Mercedes-Benz_W223_IMG_6663.jpg", "S-Class", 70m, "CB0832AP", null },
                    { new Guid("c02247bc-1488-4191-a47f-9d4aa1f6a26a"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Ford Mustang", "Gray", new DateTime(2023, 7, 2, 12, 38, 47, 569, DateTimeKind.Utc).AddTicks(6582), "Silistra, East", "https://upload.wikimedia.org/wikipedia/commons/f/f6/1967_Ford_Mustang_Shelby_GT-500_Eleanor.jpg", "GT500 Shelby", 200m, "CC0000CC", null }
                });

            migrationBuilder.InsertData(
                table: "Jets",
                columns: new[] { "Id", "AgentId", "Brand", "Color", "CreatedOn", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RenterId" },
                values: new object[,]
                {
                    { new Guid("18fff1a9-06cd-4b63-8c50-19f1332e093a"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Honda", "Red", new DateTime(2023, 7, 2, 15, 38, 47, 570, DateTimeKind.Local).AddTicks(4349), "Burgas", "https://getmyboat-user-images1.imgix.net/images/626ebcd126768/-processed.jpg", "PWC", 60m, null },
                    { new Guid("190cfb6a-7efa-411a-8bc2-847541cf1854"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Yamaha", "Black", new DateTime(2023, 7, 2, 15, 38, 47, 570, DateTimeKind.Local).AddTicks(4209), "Varna, Black Sea", "https://upload.wikimedia.org/wikipedia/commons/7/7d/2020_Yamaha_FX_SVHO_WaveRunner.jpg", "Wave Runner", 50m, new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") },
                    { new Guid("975e1497-71f3-4e98-acee-9371878ea77c"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Yamaha", "Blue", new DateTime(2023, 7, 2, 15, 38, 47, 570, DateTimeKind.Local).AddTicks(4364), "Balchik", "https://d1kqllve43agrl.cloudfront.net/imgs/Yamaha-superjet-701-bj-2008-17754.jpeg", "Superjet 701", 45m, null }
                });

            migrationBuilder.InsertData(
                table: "Motorcycles",
                columns: new[] { "Id", "AgentId", "Brand", "Color", "CreatedOn", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RegistrationNumber", "RenterId" },
                values: new object[,]
                {
                    { new Guid("48657c2e-adcf-461c-9c37-3af9d28d811b"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Suzuki", "Blue", new DateTime(2023, 7, 2, 12, 38, 47, 571, DateTimeKind.Utc).AddTicks(1298), "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/GSXR1000K5.jpg/640px-GSXR1000K5.jpg", "GSX-R1000K5", 75m, "CC1550AB", null },
                    { new Guid("8348303b-638e-4095-a431-9b17dfa109eb"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Honda", "Red", new DateTime(2023, 7, 2, 12, 38, 47, 571, DateTimeKind.Utc).AddTicks(1254), "Ruse, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/06/Honda750NR.jpg/1200px-Honda750NR.jpg", "NR-L", 30m, "CC3552OB", new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") },
                    { new Guid("afaee2d6-a456-4bbe-b607-d2ee4cbb7b0f"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Harley-Davidson", "Gray", new DateTime(2023, 7, 2, 12, 38, 47, 571, DateTimeKind.Utc).AddTicks(1303), "Silistra, West", "https://upload.wikimedia.org/wikipedia/commons/1/13/Harley_5-06.jpg", "VRSC", 110m, "CC1200AK", null }
                });

            migrationBuilder.InsertData(
                table: "Scooters",
                columns: new[] { "Id", "AgentId", "Brand", "CreatedOn", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RenterId" },
                values: new object[,]
                {
                    { new Guid("239e20a6-ed19-4537-97ce-9cecf8ca6181"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Xiomi", new DateTime(2023, 7, 2, 12, 38, 47, 571, DateTimeKind.Utc).AddTicks(8014), "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Xiaomi_M365.jpg/1200px-Xiaomi_M365.jpg", null, 12m, new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") },
                    { new Guid("81b1c074-cb07-4435-9ba4-b8b706c26494"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "E-scooter", new DateTime(2023, 7, 2, 12, 38, 47, 571, DateTimeKind.Utc).AddTicks(8057), "Silistra, North", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/29/Elektrische-tretroller.jpg/800px-Elektrische-tretroller.jpg", null, 7m, null },
                    { new Guid("90f56177-1cec-4056-acb2-f5dfe2015d36"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Inmotion", new DateTime(2023, 7, 2, 12, 38, 47, 571, DateTimeKind.Utc).AddTicks(8062), "Silistra, West", "https://cdn.shopify.com/s/files/1/0021/7389/4702/products/LeMotion-Web-1.jpg?v=1636106454", null, 9m, null }
                });

            migrationBuilder.InsertData(
                table: "Yachts",
                columns: new[] { "Id", "AgentId", "Brand", "Color", "CreatedOn", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RenterId" },
                values: new object[,]
                {
                    { new Guid("235b8d48-01ec-4091-9c87-0c5aea367e50"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Hanse", "Gray", new DateTime(2023, 7, 2, 12, 38, 47, 572, DateTimeKind.Utc).AddTicks(4998), "Varna", "https://img.yachtall.com/image-sale-boat/hanse-675-huge-203059a1l5rng9zti.jpg", "675", 180m, null },
                    { new Guid("78f07da5-775c-41cc-aa6f-2364a60dc5c6"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Bavaria", "White", new DateTime(2023, 7, 2, 12, 38, 47, 572, DateTimeKind.Utc).AddTicks(4949), "Varna", "https://images.boatsgroup.com/images/1/95/5/8289505_0_230720220750_1.jpg", "37 Sport", 200m, new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") },
                    { new Guid("e9f572e8-fcfd-498c-ae77-1426319c4e3e"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Bavaria", "White", new DateTime(2023, 7, 2, 12, 38, 47, 572, DateTimeKind.Utc).AddTicks(4990), "Burgas", "https://www.sailionian.com/wp-content/uploads/2020/05/c42-ex-01.jpg", "C42 Freedom", 220m, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("16c47ced-2fd4-4ffc-8769-06e4e8059e8f"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("40b74307-95ec-41d2-8ff1-c879006a171b"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("c02247bc-1488-4191-a47f-9d4aa1f6a26a"));

            migrationBuilder.DeleteData(
                table: "Jets",
                keyColumn: "Id",
                keyValue: new Guid("18fff1a9-06cd-4b63-8c50-19f1332e093a"));

            migrationBuilder.DeleteData(
                table: "Jets",
                keyColumn: "Id",
                keyValue: new Guid("190cfb6a-7efa-411a-8bc2-847541cf1854"));

            migrationBuilder.DeleteData(
                table: "Jets",
                keyColumn: "Id",
                keyValue: new Guid("975e1497-71f3-4e98-acee-9371878ea77c"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("48657c2e-adcf-461c-9c37-3af9d28d811b"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("8348303b-638e-4095-a431-9b17dfa109eb"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("afaee2d6-a456-4bbe-b607-d2ee4cbb7b0f"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("239e20a6-ed19-4537-97ce-9cecf8ca6181"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("81b1c074-cb07-4435-9ba4-b8b706c26494"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("90f56177-1cec-4056-acb2-f5dfe2015d36"));

            migrationBuilder.DeleteData(
                table: "Yachts",
                keyColumn: "Id",
                keyValue: new Guid("235b8d48-01ec-4091-9c87-0c5aea367e50"));

            migrationBuilder.DeleteData(
                table: "Yachts",
                keyColumn: "Id",
                keyValue: new Guid("78f07da5-775c-41cc-aa6f-2364a60dc5c6"));

            migrationBuilder.DeleteData(
                table: "Yachts",
                keyColumn: "Id",
                keyValue: new Guid("e9f572e8-fcfd-498c-ae77-1426319c4e3e"));

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Yachts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 5);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Scooters",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Motorcycles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Jets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 4);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Cars",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "Color", "CreatedOn", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RegistrationNumber", "RenterId" },
                values: new object[,]
                {
                    { new Guid("5845fbe2-cca9-4e4f-8fc2-a3a14b3fb415"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Hyundai", 1, "Black", new DateTime(2023, 7, 1, 20, 41, 35, 773, DateTimeKind.Utc).AddTicks(340), "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/4/4b/Hyundai_Santa_Fe_%28TM%29_PHEV_FL_IMG_6648.jpg", "Santa fe", 40m, "CC1835AX", new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") },
                    { new Guid("b5bb78e2-862b-4e34-9481-3e930ee8d8d7"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Mercedes", 1, "White", new DateTime(2023, 7, 1, 20, 41, 35, 773, DateTimeKind.Utc).AddTicks(533), "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/5/55/Mercedes-Benz_W223_IMG_6663.jpg/1200px-Mercedes-Benz_W223_IMG_6663.jpg", "S-Class", 70m, "CB0832AP", null },
                    { new Guid("cec346e5-834b-46da-a378-992f4c27cef8"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Ford Mustang", 1, "Gray", new DateTime(2023, 7, 1, 20, 41, 35, 773, DateTimeKind.Utc).AddTicks(538), "Silistra, East", "https://upload.wikimedia.org/wikipedia/commons/f/f6/1967_Ford_Mustang_Shelby_GT-500_Eleanor.jpg", "GT500 Shelby", 200m, "CC0000CC", null }
                });

            migrationBuilder.InsertData(
                table: "Jets",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "Color", "CreatedOn", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RenterId" },
                values: new object[,]
                {
                    { new Guid("677f095d-8cb3-4731-9b52-6a6e679a089b"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Yamaha", 4, "Black", new DateTime(2023, 7, 1, 23, 41, 35, 773, DateTimeKind.Local).AddTicks(5458), "Varna, Black Sea", "https://upload.wikimedia.org/wikipedia/commons/7/7d/2020_Yamaha_FX_SVHO_WaveRunner.jpg", "Wave Runner", 50m, new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") },
                    { new Guid("86b7b3ab-820f-4237-ab1d-73e83f8afd1c"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Yamaha", 4, "Blue", new DateTime(2023, 7, 1, 23, 41, 35, 773, DateTimeKind.Local).AddTicks(5583), "Balchik", "https://d1kqllve43agrl.cloudfront.net/imgs/Yamaha-superjet-701-bj-2008-17754.jpeg", "Superjet 701", 45m, null },
                    { new Guid("e3f04801-73ec-49fc-ad16-ddd533a1ac0d"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Honda", 4, "Red", new DateTime(2023, 7, 1, 23, 41, 35, 773, DateTimeKind.Local).AddTicks(5575), "Burgas", "https://getmyboat-user-images1.imgix.net/images/626ebcd126768/-processed.jpg", "PWC", 60m, null }
                });

            migrationBuilder.InsertData(
                table: "Motorcycles",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "Color", "CreatedOn", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RegistrationNumber", "RenterId" },
                values: new object[,]
                {
                    { new Guid("2934d582-0e8f-4068-91a9-5185073e2d53"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Honda", 2, "Red", new DateTime(2023, 7, 1, 20, 41, 35, 774, DateTimeKind.Utc).AddTicks(78), "Ruse, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/06/Honda750NR.jpg/1200px-Honda750NR.jpg", "NR-L", 30m, "CC3552OB", new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") },
                    { new Guid("8b5497c7-1c53-490b-92a3-74acf0d85870"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Harley-Davidson", 2, "Gray", new DateTime(2023, 7, 1, 20, 41, 35, 774, DateTimeKind.Utc).AddTicks(122), "Silistra, West", "https://upload.wikimedia.org/wikipedia/commons/1/13/Harley_5-06.jpg", "VRSC", 110m, "CC1200AK", null },
                    { new Guid("c616faac-a700-44bd-ac51-83a0bf662cee"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Suzuki", 2, "Blue", new DateTime(2023, 7, 1, 20, 41, 35, 774, DateTimeKind.Utc).AddTicks(117), "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/GSXR1000K5.jpg/640px-GSXR1000K5.jpg", "GSX-R1000K5", 75m, "CC1550AB", null }
                });

            migrationBuilder.InsertData(
                table: "Scooters",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "CreatedOn", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RenterId" },
                values: new object[,]
                {
                    { new Guid("08832e29-74ca-4405-9e93-c7070de395da"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Xiomi", 3, new DateTime(2023, 7, 1, 20, 41, 35, 774, DateTimeKind.Utc).AddTicks(4419), "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Xiaomi_M365.jpg/1200px-Xiaomi_M365.jpg", null, 12m, new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") },
                    { new Guid("2aff1642-c786-4cb7-87f3-2e1a6a6693c4"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Inmotion", 3, new DateTime(2023, 7, 1, 20, 41, 35, 774, DateTimeKind.Utc).AddTicks(4455), "Silistra, West", "https://cdn.shopify.com/s/files/1/0021/7389/4702/products/LeMotion-Web-1.jpg?v=1636106454", null, 9m, null },
                    { new Guid("ff64d0a6-da6a-4330-9fe6-7b3c23bb604e"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "E-scooter", 3, new DateTime(2023, 7, 1, 20, 41, 35, 774, DateTimeKind.Utc).AddTicks(4451), "Silistra, North", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/29/Elektrische-tretroller.jpg/800px-Elektrische-tretroller.jpg", null, 7m, null }
                });

            migrationBuilder.InsertData(
                table: "Yachts",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "Color", "CreatedOn", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RenterId" },
                values: new object[,]
                {
                    { new Guid("146bcd6c-7269-4b0c-9067-c53ea0539d36"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Bavaria", 5, "White", new DateTime(2023, 7, 1, 20, 41, 35, 774, DateTimeKind.Utc).AddTicks(8876), "Burgas", "https://www.sailionian.com/wp-content/uploads/2020/05/c42-ex-01.jpg", "C42 Freedom", 220m, null },
                    { new Guid("7721c73b-d914-4baf-ba05-261bb81b14cc"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Hanse", 5, "Gray", new DateTime(2023, 7, 1, 20, 41, 35, 774, DateTimeKind.Utc).AddTicks(8882), "Varna", "https://img.yachtall.com/image-sale-boat/hanse-675-huge-203059a1l5rng9zti.jpg", "675", 180m, null },
                    { new Guid("ddf76627-1b21-475e-b3f4-29e252d1bd50"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Bavaria", 5, "White", new DateTime(2023, 7, 1, 20, 41, 35, 774, DateTimeKind.Utc).AddTicks(8839), "Varna", "https://images.boatsgroup.com/images/1/95/5/8289505_0_230720220750_1.jpg", "37 Sport", 200m, new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") }
                });
        }
    }
}
