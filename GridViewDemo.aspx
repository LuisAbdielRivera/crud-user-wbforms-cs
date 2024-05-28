<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridViewDemo.aspx.cs" Inherits="WebApplication2.GridViewDemo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sistema de Usuarios</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            padding: 20px;
        }
        .form-container {
            background-color: #fff;
            padding: 20px;
        }
        .grid-container {
            margin-top: 20px;
        }
        .btn-custom {
            margin: 2px;
        }
        .btn-edit {
            background-color: #28a745;
            color: white;
        }
        .btn-delete {
            background-color: #dc3545;
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="form-container">
                <div class="form-group">
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" Placeholder="Nombre"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" Placeholder="Apellidos"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Correo Electrónico"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" Placeholder="Salario"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:DropDownList ID="ddlDireccion" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="form-group text-center">
                    <asp:Button ID="btnAdd" runat="server" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAdd_Click"/>
                    <asp:Button ID="btnUpdate" runat="server" Text="Actualizar" CssClass="btn btn-primary" Visible="False" OnClick="btnUpdate_Click"/>
                    <asp:Button ID="btnClear" runat="server" Text="Limpiar" CssClass="btn btn-primary" OnClick="btnClear_Click"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-primary" Visible="False" OnClick="btnCancel_Click"/>
                </div>
            </div>
            <div class="grid-container">
                <asp:GridView ID="userData" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" DataKeyNames="EmployeeID">
                    <Columns>
                        <asp:BoundField DataField="EmployeeID" HeaderText="No. Empleado" ReadOnly="True" />
                        <asp:BoundField DataField="FirstName" HeaderText="Nombre" />
                        <asp:BoundField DataField="LastName" HeaderText="Apellidos" />
                        <asp:BoundField DataField="Email" HeaderText="Correo Electrónico" />
                        <asp:BoundField DataField="Salary" HeaderText="Salario" />
                        <asp:BoundField DataField="DepartamentoNombre" HeaderText="Departamento" />
                        <asp:BoundField DataField="DireccionNombre" HeaderText="Dirección" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" Text="Editar" runat="server" OnClick="OnGetU" CssClass="btn btn-custom btn-edit"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" Text="Eliminar" runat="server" OnClick="OnGetD" CssClass="btn btn-custom btn-delete"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
