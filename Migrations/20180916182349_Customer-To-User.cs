using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace XBitApi.Migrations
{
    public partial class CustomerToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_UserInformations_UserInformationId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_FarmMembers_Customers_CustomerId",
                table: "FarmMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_MiningFarms_Customers_CustomerId",
                table: "MiningFarms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_UserInformationId",
                table: "Users",
                newName: "IX_Users_UserInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_AddressId",
                table: "Users",
                newName: "IX_Users_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FarmMembers_Users_CustomerId",
                table: "FarmMembers",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MiningFarms_Users_CustomerId",
                table: "MiningFarms",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserInformations_UserInformationId",
                table: "Users",
                column: "UserInformationId",
                principalTable: "UserInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FarmMembers_Users_CustomerId",
                table: "FarmMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_MiningFarms_Users_CustomerId",
                table: "MiningFarms");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_AddressId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserInformations_UserInformationId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserInformationId",
                table: "Customers",
                newName: "IX_Customers_UserInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_AddressId",
                table: "Customers",
                newName: "IX_Customers_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_UserInformations_UserInformationId",
                table: "Customers",
                column: "UserInformationId",
                principalTable: "UserInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FarmMembers_Customers_CustomerId",
                table: "FarmMembers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MiningFarms_Customers_CustomerId",
                table: "MiningFarms",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
