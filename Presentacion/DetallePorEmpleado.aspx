<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallePorEmpleado.aspx.cs" Inherits="NominaRRHH.Presentacion.DetallePorEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top">
        <div class="panel panel-info" style="width: 1220px;">
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
                    <div class="col-md-12" style="margin-left: 170px;">
                         <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Tipos De Planilla</label>
                            <asp:DropDownList class="form-control" ID="ddlTiposPlanilla" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTiposPlanilla_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Codigo Empleado</label>
                            <asp:TextBox ID="txtCodigo" class="form-control" placeholder="Digite Codigo"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <label class="control-label" for="focusedInput">Periodo</label>
                            <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-1" id="divSemana" runat="server" visible ="false">
                            <label class="control-label" for="focusedInput">Semana</label>
                            <asp:DropDownList class="form-control" Style="width: 67px;" ID="ddlSemana" runat="server">
                                <asp:ListItem Value="1"> 1</asp:ListItem>
                                <asp:ListItem Value="2"> 2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnBuscar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Horas T</label>
                            <asp:TextBox ID="txtHorast" ReadOnly="true" class="form-control"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Salario</label>
                            <asp:TextBox ID="txtSalario"  class="form-control"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">HE</label>
                            <asp:TextBox ID="txtHe" class="form-control"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">ValorHE</label>
                            <asp:TextBox ID="txtValorHe" class="form-control"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Inss</label>
                            <asp:TextBox ID="txtInss" ReadOnly="true" class="form-control"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">IR</label>
                            <asp:TextBox ID="txtIr" ReadOnly="true" class="form-control"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div><br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Total Ingresos</label>
                            <asp:TextBox ID="txtTotIngresos" ReadOnly="true" class="form-control"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Total Egresos</label>
                            <asp:TextBox ID="txtTotEgresos" ReadOnly="true" class="form-control"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Neto</label>
                            <asp:TextBox ID="txtNeto" ReadOnly="true" class="form-control"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnGuardar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12" runat="server" id="gridIngresos" visible="false">
                        <h2>Ingresos</h2>
                        <div class="col-md-12">
                            <asp:GridView ID="GVDetalleIngrs" class="table table-striped table-hover" runat="server"
                                AutoGenerateColumns="False" DataKeyNames="idDevengado" CellPadding="2" GridLines="Vertical"
                                BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                                AllowPaging="True" AllowSorting="True" OnRowCommand="GVDetalleIngrs_RowCommand" OnSelectedIndexChanged="GVDetalleIngrs_SelectedIndexChanged">
                                <AlternatingRowStyle Width="100px" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                        <ControlStyle Width="5px" />
                                        <HeaderStyle Width="5px" />
                                        <ItemStyle Width="5px" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtId" Style="width: 50px;" class="form-control" autocomplete="off" ReadOnly="true" runat="server" Text='<% # Bind("idDevengado")%>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="2px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="devengadoNombre" HeaderText="Devengado">
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
                                    <asp:ButtonField ButtonType="Button" CommandName="eliminar" Text="Eliminar">
                                        <ControlStyle CssClass="btn btn-danger btn-xs" />
                                        <ItemStyle Width="16px" />
                                    </asp:ButtonField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <br />
                    <br />
                    <div class="col-md-12" runat="server" id="gridEgresos" visible="false">
                        <h2>Deducciones</h2>
                        <div class="col-md-12">
                            <asp:GridView ID="GVDetNomEmpl" class="table table-striped table-hover" runat="server"
                                AutoGenerateColumns="False" DataKeyNames="idDeduccion" CellPadding="2" GridLines="Vertical"
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
                                            <asp:TextBox ID="txtId" Style="width: 50px;" class="form-control" autocomplete="off" ReadOnly="true" runat="server" Text='<% # Bind("idDeduccion")%>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="2px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="deduccionNombre" HeaderText="Deduccion">
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
                                    <asp:ButtonField ButtonType="Button" CommandName="eliminar" Text="Eliminar">
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
    </div>
</asp:Content>

