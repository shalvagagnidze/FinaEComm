using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class Added_FacetsFacetValuesToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DisplayType = table.Column<int>(type: "integer", nullable: false),
                    IsCustom = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryFacet",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacetsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFacet", x => new { x.CategoriesId, x.FacetsId });
                    table.ForeignKey(
                        name: "FK_CategoryFacet_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryFacet_Facets_FacetsId",
                        column: x => x.FacetsId,
                        principalTable: "Facets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacetValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    FacetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacetValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacetValues_Facets_FacetId",
                        column: x => x.FacetId,
                        principalTable: "Facets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFacetValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacetValueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFacetValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFacetValues_FacetValues_FacetValueId",
                        column: x => x.FacetValueId,
                        principalTable: "FacetValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductFacetValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFacet_FacetsId",
                table: "CategoryFacet",
                column: "FacetsId");

            migrationBuilder.CreateIndex(
                name: "IX_FacetValues_FacetId",
                table: "FacetValues",
                column: "FacetId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFacetValues_FacetValueId",
                table: "ProductFacetValues",
                column: "FacetValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFacetValues_ProductId",
                table: "ProductFacetValues",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryFacet");

            migrationBuilder.DropTable(
                name: "ProductFacetValues");

            migrationBuilder.DropTable(
                name: "FacetValues");

            migrationBuilder.DropTable(
                name: "Facets");
        }
    }
}
