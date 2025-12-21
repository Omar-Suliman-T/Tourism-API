using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tourist.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class SeedsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Norifications_AspNetUsers_UserId",
                table: "Norifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Norifications",
                table: "Norifications");

            migrationBuilder.RenameTable(
                name: "Norifications",
                newName: "Notifications");

            migrationBuilder.RenameIndex(
                name: "IX_Norifications_UserId",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "NotificationId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", "aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1", "Admin", "ADMIN" },
                    { "22222222-2222-2222-2222-222222222222", "bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa", 0, "dddddddd-1111-1111-1111-dddddddddddd", "saifalkomi@gmail.com", true, false, null, "SAIFALKOMI@GMAIL.COM", "SAIF KOMI", "AQAAAAIAAYagAAAAEDHhLq+Xep0cKJz7xXoA+yVJpVn+7L+5pXZ3RYw0nQ6fS4M4G6tGZ7kE8fVwV3Wp0w==", "+972592131946", true, "cccccccc-1111-1111-1111-cccccccccccc", false, "Saif Komi" },
                    { "bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb", 0, "ffffffff-2222-2222-2222-ffffffffffff", "omarsit20004031@gmail.com", true, false, null, "OMARSIT20004031@GMAIL.COM", "OMAR SULIMAN", "AQAAAAIAAYagAAAAEG5ccOAsgYYXQ9ndQ8YN6Ckv3GCdkkcDlMdDO4k47hcYfU/QZjnzXxZMqRdQ7Gz6Jw==", "+962798461282", true, "eeeeeeee-2222-2222-2222-eeeeeeeeeeee", false, "Omar Suliman" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Permission", "CanManageUsers", "11111111-1111-1111-1111-111111111111" },
                    { 2, "Permission", "CanBookTrips", "22222222-2222-2222-2222-222222222222" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "FullName", "Saif Komi", "aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa" },
                    { 2, "FullName", "Omar Suliman", "bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", "aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa" },
                    { "22222222-2222-2222-2222-222222222222", "bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "11111111-1111-1111-1111-111111111111", "aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "22222222-2222-2222-2222-222222222222", "bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Norifications");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "Norifications",
                newName: "IX_Norifications_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Norifications",
                table: "Norifications",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Norifications_AspNetUsers_UserId",
                table: "Norifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
