<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleDDEmp.aspx.cs" Inherits="NominaRRHH.Presentacion.DetalleDDEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="lblAlert" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="LblSuccess" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-12" style="margin-left: 146px;">
                         <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Tipos De Planilla</label>
                            <asp:DropDownList class="form-control" ID="ddlTiposPlanilla" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTiposPlanilla_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <label class="control-label" for="focusedInput">Periodo</label>
                            <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-1"  id="divSemana" runat="server" visible ="false">
                            <label class="control-label" for="focusedInput">Semana</label>
                            <asp:DropDownList class="form-control" Style="width: 67px;" ID="ddlSemana" runat="server">
                                <asp:ListItem Value="1"> 1</asp:ListItem>
                                <asp:ListItem Value="2"> 2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Codigo</label>
                            <asp:TextBox ID="txtCodigo" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnBuscar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                                <div class="col-md-12 ">
                                          <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Codigo</label>
                                            <asp:TextBox ID="txtcodigoAsig" class="form-control"
                                                runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Nombre</label>
                                            <asp:TextBox ID="TxtNombreE" class="form-control"
                                                runat="server" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                        </div>
                                                             
                                </div>
                            </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="GVDetNomEmpl" class="table table-striped table-hover" runat="server" Style="width:100%;"
                            AutoGenerateColumns="False" DataKeyNames="id,idDeduccion"  CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="GVDetNomEmpl_SelectedIndexChanged" OnRowCommand="GVDetNomEmpl_RowCommand">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                    <ControlStyle Width="5px" />
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:CommandField>
                                  <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtId" Style="width: 50px;" class="form-control" autocomplete="off" ReadOnly="true" runat="server" Text='<% # Bind("id")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="2px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IdDeduccion">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIdDeduccion" Style="width: 50px;" class="form-control" autocomplete="off" ReadOnly="true" runat="server" Text='<% # Bind("idDeduccion")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="2px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="deduccionNombre" HeaderText="Concepto">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotal" class="form-control" autocomplete="off" ReadOnly="true" runat="server" Text='<% # Bind("valor")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="2px" />
                                </asp:TemplateField>
                                <asp:ButtonField ButtonType="Button" Text="Editar" CommandName="editar">
                                    <ControlStyle CssClass="btn btn-success btn-xs" />
                                    <ItemStyle Width="16px" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Button"  CommandName="eliminar" Text="Eliminar">
                                    <ControlStyle CssClass="btn btn-danger btn-xs" />
                                    <ItemStyle Width="16px" />
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function soloNumeros(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "0123456789.";
            especiales = "8-37-39-46";

            tecla_especial = false
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }

            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }
    </script>
</asp:Content>
