using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoLibreria.Migrations
{
    /// <inheritdoc />
    public partial class Libreria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    id_autor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fechanacimiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.id_autor);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    idcategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoria = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.idcategoria);
                });

            migrationBuilder.CreateTable(
                name: "Editoriales",
                columns: table => new
                {
                    id_editorial = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editoriales", x => x.id_editorial);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id_rol);
                });

            migrationBuilder.CreateTable(
                name: "Libro",
                columns: table => new
                {
                    id_libro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoriasidcategoria = table.Column<int>(type: "int", nullable: false),
                    id_categoria = table.Column<int>(type: "int", nullable: false),
                    Autoresid_autor = table.Column<int>(type: "int", nullable: false),
                    id_autor = table.Column<int>(type: "int", nullable: false),
                    Editorialesid_editorial = table.Column<int>(type: "int", nullable: false),
                    id_editorial = table.Column<int>(type: "int", nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    anio = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    url_libro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libro", x => x.id_libro);
                    table.ForeignKey(
                        name: "FK_Libro_Autores_Autoresid_autor",
                        column: x => x.Autoresid_autor,
                        principalTable: "Autores",
                        principalColumn: "id_autor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Libro_Categorias_Categoriasidcategoria",
                        column: x => x.Categoriasidcategoria,
                        principalTable: "Categorias",
                        principalColumn: "idcategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Libro_Editoriales_Editorialesid_editorial",
                        column: x => x.Editorialesid_editorial,
                        principalTable: "Editoriales",
                        principalColumn: "id_editorial",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rolesid_rol = table.Column<int>(type: "int", nullable: false),
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    nombre_usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_Rolesid_rol",
                        column: x => x.Rolesid_rol,
                        principalTable: "Roles",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    id_venta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuariosid_usuario = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    descuento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    iva = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fecha_de_venta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.id_venta);
                    table.ForeignKey(
                        name: "FK_Ventas_Usuarios_Usuariosid_usuario",
                        column: x => x.Usuariosid_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detalles",
                columns: table => new
                {
                    id_detalle_venta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ventasid_venta = table.Column<int>(type: "int", nullable: false),
                    id_venta = table.Column<int>(type: "int", nullable: false),
                    id_libro = table.Column<int>(type: "int", nullable: false),
                    Librosid_libro = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles", x => x.id_detalle_venta);
                    table.ForeignKey(
                        name: "FK_Detalles_Libro_Librosid_libro",
                        column: x => x.Librosid_libro,
                        principalTable: "Libro",
                        principalColumn: "id_libro",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Detalles_Ventas_Ventasid_venta",
                        column: x => x.Ventasid_venta,
                        principalTable: "Ventas",
                        principalColumn: "id_venta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_categoria",
                table: "Categorias",
                column: "categoria",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_Librosid_libro",
                table: "Detalles",
                column: "Librosid_libro");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_Ventasid_venta",
                table: "Detalles",
                column: "Ventasid_venta");

            migrationBuilder.CreateIndex(
                name: "IX_Libro_Autoresid_autor",
                table: "Libro",
                column: "Autoresid_autor");

            migrationBuilder.CreateIndex(
                name: "IX_Libro_Categoriasidcategoria",
                table: "Libro",
                column: "Categoriasidcategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Libro_Editorialesid_editorial",
                table: "Libro",
                column: "Editorialesid_editorial");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Rolesid_rol",
                table: "Usuarios",
                column: "Rolesid_rol");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_Usuariosid_usuario",
                table: "Ventas",
                column: "Usuariosid_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalles");

            migrationBuilder.DropTable(
                name: "Libro");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Editoriales");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
