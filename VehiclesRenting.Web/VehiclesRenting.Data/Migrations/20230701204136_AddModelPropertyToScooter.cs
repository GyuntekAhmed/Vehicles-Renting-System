using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehiclesRenting.Data.Migrations
{
    public partial class AddModelPropertyToScooter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("53ee2a22-6e5f-4610-9eab-1d3ffc30d08c"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("9464b95b-bd4d-4f5f-b9b6-a5a4f0c8c4a1"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("daea8025-3752-41b6-aa39-9a09d4f4e566"));

            migrationBuilder.DeleteData(
                table: "Jets",
                keyColumn: "Id",
                keyValue: new Guid("7a29aace-0c02-4aff-ba39-d722d6686243"));

            migrationBuilder.DeleteData(
                table: "Jets",
                keyColumn: "Id",
                keyValue: new Guid("c38cfa95-212f-4960-9fd7-0ad11ba9aee9"));

            migrationBuilder.DeleteData(
                table: "Jets",
                keyColumn: "Id",
                keyValue: new Guid("cbf2fb92-ed47-42a7-8b5c-6fb91f1c8650"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("3d286f57-c1f6-464a-8982-b4ee8b96eaa4"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("58940271-f37a-4158-997f-7f5f8f0a374a"));

            migrationBuilder.DeleteData(
                table: "Motorcycles",
                keyColumn: "Id",
                keyValue: new Guid("6c7799bb-d535-4845-bf19-2331660d1f20"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("a64ca7af-bd7e-4ead-8fbc-d03bf2a4ab88"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("d7ffa275-e610-4b64-98be-40aef5a91c6b"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("fcd31087-fdfe-456b-804b-e33e9de97ae4"));

            migrationBuilder.DeleteData(
                table: "Yachts",
                keyColumn: "Id",
                keyValue: new Guid("416f83e9-76f9-43df-92f7-97d624f7395b"));

            migrationBuilder.DeleteData(
                table: "Yachts",
                keyColumn: "Id",
                keyValue: new Guid("59f23bec-60bd-4b6d-9488-91d7832c8e5b"));

            migrationBuilder.DeleteData(
                table: "Yachts",
                keyColumn: "Id",
                keyValue: new Guid("c489cac7-85dc-42f5-b9f8-c88df1bfc474"));

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Scooters",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Scooters");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "Color", "CreatedOn", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RegistrationNumber", "RenterId" },
                values: new object[,]
                {
                    { new Guid("53ee2a22-6e5f-4610-9eab-1d3ffc30d08c"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Hyundai", 1, "Black", new DateTime(2023, 7, 1, 15, 9, 46, 986, DateTimeKind.Utc).AddTicks(6278), "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/4/4b/Hyundai_Santa_Fe_%28TM%29_PHEV_FL_IMG_6648.jpg", "Santa fe", 40m, "CC1835AX", new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") },
                    { new Guid("9464b95b-bd4d-4f5f-b9b6-a5a4f0c8c4a1"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Mercedes", 1, "White", new DateTime(2023, 7, 1, 15, 9, 46, 986, DateTimeKind.Utc).AddTicks(6360), "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/5/55/Mercedes-Benz_W223_IMG_6663.jpg/1200px-Mercedes-Benz_W223_IMG_6663.jpg", "S-Class", 70m, "CB0832AP", null },
                    { new Guid("daea8025-3752-41b6-aa39-9a09d4f4e566"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Ford Mustang", 1, "Gray", new DateTime(2023, 7, 1, 15, 9, 46, 986, DateTimeKind.Utc).AddTicks(6398), "Silistra, East", "https://upload.wikimedia.org/wikipedia/commons/f/f6/1967_Ford_Mustang_Shelby_GT-500_Eleanor.jpg", "GT500 Shelby", 200m, "CC0000CC", null }
                });

            migrationBuilder.InsertData(
                table: "Jets",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "Color", "CreatedOn", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RenterId" },
                values: new object[,]
                {
                    { new Guid("7a29aace-0c02-4aff-ba39-d722d6686243"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Yamaha", 4, "Blue", new DateTime(2023, 7, 1, 18, 9, 46, 987, DateTimeKind.Local).AddTicks(1355), "Balchik", "https://d1kqllve43agrl.cloudfront.net/imgs/Yamaha-superjet-701-bj-2008-17754.jpeg", "Superjet 701", 45m, null },
                    { new Guid("c38cfa95-212f-4960-9fd7-0ad11ba9aee9"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Yamaha", 4, "Black", new DateTime(2023, 7, 1, 18, 9, 46, 987, DateTimeKind.Local).AddTicks(1229), "Varna, Black Sea", "https://upload.wikimedia.org/wikipedia/commons/7/7d/2020_Yamaha_FX_SVHO_WaveRunner.jpg", "Wave Runner", 50m, new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") },
                    { new Guid("cbf2fb92-ed47-42a7-8b5c-6fb91f1c8650"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Honda", 4, "Red", new DateTime(2023, 7, 1, 18, 9, 46, 987, DateTimeKind.Local).AddTicks(1345), "Burgas", "https://getmyboat-user-images1.imgix.net/images/626ebcd126768/-processed.jpg", "PWC", 60m, null }
                });

            migrationBuilder.InsertData(
                table: "Motorcycles",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "Color", "CreatedOn", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RegistrationNumber", "RenterId" },
                values: new object[,]
                {
                    { new Guid("3d286f57-c1f6-464a-8982-b4ee8b96eaa4"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Suzuki", 2, "Blue", new DateTime(2023, 7, 1, 15, 9, 46, 987, DateTimeKind.Utc).AddTicks(5701), "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/GSXR1000K5.jpg/640px-GSXR1000K5.jpg", "GSX-R1000K5", 75m, "CC1550AB", null },
                    { new Guid("58940271-f37a-4158-997f-7f5f8f0a374a"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Honda", 2, "Red", new DateTime(2023, 7, 1, 15, 9, 46, 987, DateTimeKind.Utc).AddTicks(5670), "Ruse, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/06/Honda750NR.jpg/1200px-Honda750NR.jpg", "NR-L", 30m, "CC3552OB", new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") },
                    { new Guid("6c7799bb-d535-4845-bf19-2331660d1f20"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Harley-Davidson", 2, "Gray", new DateTime(2023, 7, 1, 15, 9, 46, 987, DateTimeKind.Utc).AddTicks(5706), "Silistra, West", "https://upload.wikimedia.org/wikipedia/commons/1/13/Harley_5-06.jpg", "VRSC", 110m, "CC1200AK", null }
                });

            migrationBuilder.InsertData(
                table: "Scooters",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "CreatedOn", "CurrentAddress", "ImageUrl", "PricePerDay", "RenterId" },
                values: new object[,]
                {
                    { new Guid("a64ca7af-bd7e-4ead-8fbc-d03bf2a4ab88"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Xiomi", 3, new DateTime(2023, 7, 1, 15, 9, 46, 987, DateTimeKind.Utc).AddTicks(9993), "Silistra, Center", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Xiaomi_M365.jpg/1200px-Xiaomi_M365.jpg", 12m, new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") },
                    { new Guid("d7ffa275-e610-4b64-98be-40aef5a91c6b"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "E-scooter", 3, new DateTime(2023, 7, 1, 15, 9, 46, 988, DateTimeKind.Utc).AddTicks(29), "Silistra, North", "https://upload.wikimedia.org/wikipedia/commons/thumb/2/29/Elektrische-tretroller.jpg/800px-Elektrische-tretroller.jpg", 7m, null },
                    { new Guid("fcd31087-fdfe-456b-804b-e33e9de97ae4"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Inmotion", 3, new DateTime(2023, 7, 1, 15, 9, 46, 988, DateTimeKind.Utc).AddTicks(34), "Silistra, West", "https://cdn.shopify.com/s/files/1/0021/7389/4702/products/LeMotion-Web-1.jpg?v=1636106454", 9m, null }
                });

            migrationBuilder.InsertData(
                table: "Yachts",
                columns: new[] { "Id", "AgentId", "Brand", "CategoryId", "Color", "CreatedOn", "CurrentAddress", "ImageUrl", "Model", "PricePerDay", "RenterId" },
                values: new object[,]
                {
                    { new Guid("416f83e9-76f9-43df-92f7-97d624f7395b"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Bavaria", 5, "White", new DateTime(2023, 7, 1, 15, 9, 46, 988, DateTimeKind.Utc).AddTicks(4356), "Burgas", "https://www.sailionian.com/wp-content/uploads/2020/05/c42-ex-01.jpg", "C42 Freedom", 220m, null },
                    { new Guid("59f23bec-60bd-4b6d-9488-91d7832c8e5b"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Hanse", 5, "Gray", new DateTime(2023, 7, 1, 15, 9, 46, 988, DateTimeKind.Utc).AddTicks(4361), "Varna", "https://img.yachtall.com/image-sale-boat/hanse-675-huge-203059a1l5rng9zti.jpg", "675", 180m, null },
                    { new Guid("c489cac7-85dc-42f5-b9f8-c88df1bfc474"), new Guid("8ed4eaa3-738c-49a4-9cf8-874903ded0bb"), "Bavaria", 5, "White", new DateTime(2023, 7, 1, 15, 9, 46, 988, DateTimeKind.Utc).AddTicks(4324), "Varna", "https://images.boatsgroup.com/images/1/95/5/8289505_0_230720220750_1.jpg", "37 Sport", 200m, new Guid("c42ef5d1-0c67-4dc2-9467-ec9947baa83f") }
                });
        }
    }
}
