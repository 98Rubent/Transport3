using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Transport.Data.Migrations
{
    public partial class Completo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Cliente",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Nacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Masculino = table.Column<bool>(type: "bit", nullable: false),
                    DPI = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    NIT = table.Column<int>(type: "int", nullable: false),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroPedidos = table.Column<int>(type: "int", nullable: false),
                    Inversion = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Cliente", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "T_Departamento",
                columns: table => new
                {
                    DepartamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Departamento", x => x.DepartamentoId);
                });

            migrationBuilder.CreateTable(
                name: "T_Producto",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Existencias = table.Column<int>(type: "int", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CostoUnidad = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Producto", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "T_TipoLugar",
                columns: table => new
                {
                    TipoLugarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lugar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TipoLugar", x => x.TipoLugarID);
                });

            migrationBuilder.CreateTable(
                name: "T_TipoVehiculo",
                columns: table => new
                {
                    TipoVehiculoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_TipoVehiculo", x => x.TipoVehiculoID);
                });

            migrationBuilder.CreateTable(
                name: "T_Planta",
                columns: table => new
                {
                    PlantaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Procesamiento = table.Column<bool>(type: "bit", nullable: false),
                    DepartamentoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Planta", x => x.PlantaId);
                    table.ForeignKey(
                        name: "FK_T_Planta_T_Departamento_DepartamentoID",
                        column: x => x.DepartamentoID,
                        principalTable: "T_Departamento",
                        principalColumn: "DepartamentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Directorio",
                columns: table => new
                {
                    DirectorioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Indicaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Contacto = table.Column<int>(type: "int", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoLugarID = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Directorio", x => x.DirectorioId);
                    table.ForeignKey(
                        name: "FK_T_Directorio_T_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "T_Producto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_Directorio_T_TipoLugar_TipoLugarID",
                        column: x => x.TipoLugarID,
                        principalTable: "T_TipoLugar",
                        principalColumn: "TipoLugarID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_ProductoAsignado",
                columns: table => new
                {
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    PlantaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_ProductoAsignado", x => new { x.ProductoID, x.PlantaID });
                    table.ForeignKey(
                        name: "FK_T_ProductoAsignado_T_Planta_PlantaID",
                        column: x => x.PlantaID,
                        principalTable: "T_Planta",
                        principalColumn: "PlantaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_ProductoAsignado_T_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "T_Producto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Vehiculo",
                columns: table => new
                {
                    VehiculoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Modelo = table.Column<int>(type: "int", nullable: false),
                    Linea = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Mantenimiento = table.Column<bool>(type: "bit", nullable: false),
                    Ruta = table.Column<bool>(type: "bit", nullable: false),
                    TipoVehiculoID = table.Column<int>(type: "int", nullable: false),
                    PlantaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Vehiculo", x => x.VehiculoId);
                    table.ForeignKey(
                        name: "FK_T_Vehiculo_T_Planta_PlantaId",
                        column: x => x.PlantaId,
                        principalTable: "T_Planta",
                        principalColumn: "PlantaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_Vehiculo_T_TipoVehiculo_TipoVehiculoID",
                        column: x => x.TipoVehiculoID,
                        principalTable: "T_TipoVehiculo",
                        principalColumn: "TipoVehiculoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_SolicitudTransporte",
                columns: table => new
                {
                    SolicitudTransporteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Receptor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contacto = table.Column<int>(type: "int", nullable: false),
                    Pagado = table.Column<bool>(type: "bit", nullable: false),
                    Total = table.Column<decimal>(type: "money", nullable: false),
                    DestinoID = table.Column<int>(type: "int", nullable: false),
                    ProductoID = table.Column<int>(type: "int", nullable: false),
                    ClienteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SolicitudTransporte", x => x.SolicitudTransporteID);
                    table.ForeignKey(
                        name: "FK_T_SolicitudTransporte_T_Cliente_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "T_Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_SolicitudTransporte_T_Directorio_DestinoID",
                        column: x => x.DestinoID,
                        principalTable: "T_Directorio",
                        principalColumn: "DirectorioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_SolicitudTransporte_T_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "T_Producto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Directorio_ProductoId",
                table: "T_Directorio",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Directorio_TipoLugarID",
                table: "T_Directorio",
                column: "TipoLugarID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Planta_DepartamentoID",
                table: "T_Planta",
                column: "DepartamentoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_ProductoAsignado_PlantaID",
                table: "T_ProductoAsignado",
                column: "PlantaID");

            migrationBuilder.CreateIndex(
                name: "IX_T_SolicitudTransporte_ClienteID",
                table: "T_SolicitudTransporte",
                column: "ClienteID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_SolicitudTransporte_DestinoID",
                table: "T_SolicitudTransporte",
                column: "DestinoID");

            migrationBuilder.CreateIndex(
                name: "IX_T_SolicitudTransporte_ProductoID",
                table: "T_SolicitudTransporte",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_T_Vehiculo_PlantaId",
                table: "T_Vehiculo",
                column: "PlantaId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Vehiculo_TipoVehiculoID",
                table: "T_Vehiculo",
                column: "TipoVehiculoID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_ProductoAsignado");

            migrationBuilder.DropTable(
                name: "T_SolicitudTransporte");

            migrationBuilder.DropTable(
                name: "T_Vehiculo");

            migrationBuilder.DropTable(
                name: "T_Cliente");

            migrationBuilder.DropTable(
                name: "T_Directorio");

            migrationBuilder.DropTable(
                name: "T_Planta");

            migrationBuilder.DropTable(
                name: "T_TipoVehiculo");

            migrationBuilder.DropTable(
                name: "T_Producto");

            migrationBuilder.DropTable(
                name: "T_TipoLugar");

            migrationBuilder.DropTable(
                name: "T_Departamento");
        }
    }
}
