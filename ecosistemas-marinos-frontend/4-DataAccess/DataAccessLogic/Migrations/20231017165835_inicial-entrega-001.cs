using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLogic.Migrations
{
    /// <inheritdoc />
    public partial class inicialentrega001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amenazas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradoDePeligro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenazas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosDeConservacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RangoConservacion_Minimo = table.Column<int>(type: "int", nullable: false),
                    RangoConservacion_Maximo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosDeConservacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEntidad = table.Column<int>(type: "int", nullable: false),
                    TipoEntidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoIso = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alias = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContraseniaCifrada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ecosistemas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false),
                    EstadoDeConservacionId = table.Column<int>(type: "int", nullable: false),
                    Ubicacion_Latitud = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ubicacion_Longitud = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecosistemas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ecosistemas_EstadosDeConservacion_EstadoDeConservacionId",
                        column: x => x.EstadoDeConservacionId,
                        principalTable: "EstadosDeConservacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AmenazaEcosistema",
                columns: table => new
                {
                    AmenazasId = table.Column<int>(type: "int", nullable: false),
                    EcosistemasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenazaEcosistema", x => new { x.AmenazasId, x.EcosistemasId });
                    table.ForeignKey(
                        name: "FK_AmenazaEcosistema_Amenazas_AmenazasId",
                        column: x => x.AmenazasId,
                        principalTable: "Amenazas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmenazaEcosistema_Ecosistemas_EcosistemasId",
                        column: x => x.EcosistemasId,
                        principalTable: "Ecosistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ecosistema_Pais",
                columns: table => new
                {
                    EcosistemasId = table.Column<int>(type: "int", nullable: false),
                    PaisesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecosistema_Pais", x => new { x.EcosistemasId, x.PaisesId });
                    table.ForeignKey(
                        name: "FK_Ecosistema_Pais_Ecosistemas_EcosistemasId",
                        column: x => x.EcosistemasId,
                        principalTable: "Ecosistemas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ecosistema_Pais_Paises_PaisesId",
                        column: x => x.PaisesId,
                        principalTable: "Paises",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Especies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCientifico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreVulgar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HabitaId = table.Column<int>(type: "int", nullable: true),
                    RangoPeso_Max = table.Column<int>(type: "int", nullable: false),
                    RangoPeso_Min = table.Column<int>(type: "int", nullable: false),
                    RangoLargo_Min = table.Column<int>(type: "int", nullable: false),
                    RangoLargo_Max = table.Column<int>(type: "int", nullable: false),
                    EstadoConservacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Especies_Ecosistemas_HabitaId",
                        column: x => x.HabitaId,
                        principalTable: "Ecosistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Especies_EstadosDeConservacion_EstadoConservacionId",
                        column: x => x.EstadoConservacionId,
                        principalTable: "EstadosDeConservacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImagenEcosistema",
                columns: table => new
                {
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EcosistemaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagenEcosistema", x => new { x.Nombre, x.EcosistemaId });
                    table.ForeignKey(
                        name: "FK_ImagenEcosistema_Ecosistemas_EcosistemaId",
                        column: x => x.EcosistemaId,
                        principalTable: "Ecosistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AmenazaEspecie",
                columns: table => new
                {
                    AmenazasId = table.Column<int>(type: "int", nullable: false),
                    EspeciesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenazaEspecie", x => new { x.AmenazasId, x.EspeciesId });
                    table.ForeignKey(
                        name: "FK_AmenazaEspecie_Amenazas_AmenazasId",
                        column: x => x.AmenazasId,
                        principalTable: "Amenazas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmenazaEspecie_Especies_EspeciesId",
                        column: x => x.EspeciesId,
                        principalTable: "Especies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ecosistema_Especie",
                columns: table => new
                {
                    EspeciesQuePuedenHabitarloId = table.Column<int>(type: "int", nullable: false),
                    PuedeHabitarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecosistema_Especie", x => new { x.EspeciesQuePuedenHabitarloId, x.PuedeHabitarId });
                    table.ForeignKey(
                        name: "FK_Ecosistema_Especie_Ecosistemas_PuedeHabitarId",
                        column: x => x.PuedeHabitarId,
                        principalTable: "Ecosistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ecosistema_Especie_Especies_EspeciesQuePuedenHabitarloId",
                        column: x => x.EspeciesQuePuedenHabitarloId,
                        principalTable: "Especies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImagenEspecie",
                columns: table => new
                {
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EspecieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagenEspecie", x => new { x.Nombre, x.EspecieId });
                    table.ForeignKey(
                        name: "FK_ImagenEspecie_Especies_EspecieId",
                        column: x => x.EspecieId,
                        principalTable: "Especies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmenazaEcosistema_EcosistemasId",
                table: "AmenazaEcosistema",
                column: "EcosistemasId");

            migrationBuilder.CreateIndex(
                name: "IX_AmenazaEspecie_EspeciesId",
                table: "AmenazaEspecie",
                column: "EspeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Ecosistema_Especie_PuedeHabitarId",
                table: "Ecosistema_Especie",
                column: "PuedeHabitarId");

            migrationBuilder.CreateIndex(
                name: "IX_Ecosistema_Pais_PaisesId",
                table: "Ecosistema_Pais",
                column: "PaisesId");

            migrationBuilder.CreateIndex(
                name: "IX_Ecosistemas_EstadoDeConservacionId",
                table: "Ecosistemas",
                column: "EstadoDeConservacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Especies_EstadoConservacionId",
                table: "Especies",
                column: "EstadoConservacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Especies_HabitaId",
                table: "Especies",
                column: "HabitaId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadosDeConservacion_Nombre",
                table: "EstadosDeConservacion",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagenEcosistema_EcosistemaId",
                table: "ImagenEcosistema",
                column: "EcosistemaId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagenEspecie_EspecieId",
                table: "ImagenEspecie",
                column: "EspecieId");

            migrationBuilder.CreateIndex(
                name: "IX_Paises_CodigoIso",
                table: "Paises",
                column: "CodigoIso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paises_Nombre",
                table: "Paises",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Alias",
                table: "Usuarios",
                column: "Alias",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmenazaEcosistema");

            migrationBuilder.DropTable(
                name: "AmenazaEspecie");

            migrationBuilder.DropTable(
                name: "Ecosistema_Especie");

            migrationBuilder.DropTable(
                name: "Ecosistema_Pais");

            migrationBuilder.DropTable(
                name: "ImagenEcosistema");

            migrationBuilder.DropTable(
                name: "ImagenEspecie");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Amenazas");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "Especies");

            migrationBuilder.DropTable(
                name: "Ecosistemas");

            migrationBuilder.DropTable(
                name: "EstadosDeConservacion");
        }
    }
}
