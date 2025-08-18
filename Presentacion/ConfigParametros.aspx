<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfigParametros.aspx.cs" Inherits="NominaRRHH.Presentacion.ConfigParametros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.8.2.js"></script>
    <link href="../Content/Styles.css" rel="stylesheet" />
    <link href="../Content/bootstrap-switch.css" rel="stylesheet" />
    <link href="../Content/bootstrap-clockpicker.css" rel="stylesheet" />
    <link href="../Content/clockpicker.css" rel="stylesheet" />
    <link href="../Content/standalone.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-switch.js"></script>
    <script src="../Scripts/bootstrap-clockpicker.js"></script>
    <script src="../Scripts/jquery-clockpicker.js"></script>
    <script src="../Scripts/clockpicker.js"></script>
    <script src="../Scripts/jquery.maskedinput.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container mar-top">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <label class="control-label" for="focusedInput">CONFIGURACION DE PARAMETROS</label>
                    </div>
                </div>
                <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="LblSuccess" runat="server"></asp:Label>
                </div>
                <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="lblAlert" runat="server"></asp:Label>
                </div>
                <div class="form-group row">
                    <div class="col-md-4">
                        <asp:ImageButton ID="ImageButton3" runat="server" Height="40px" ImageUrl="~/Images/agregar.png" ToolTip="NUEVO GRUPO" Width="40px" OnClick="ImageButton1_Click" />
                    </div>
                </div>
                <asp:Panel ID="plnEditarT" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Nombre Grupo</label>
                                <asp:TextBox ID="txtNombreGrupo" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Descripción</label>
                                <asp:TextBox ID="txtDescripcionGrupo" class="form-control"
                                    runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnAgregar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Guardar" OnClick="btnAgregar_Click" />
                                <asp:Button class="btn btn-danger btnagregarPermiso" ID="btnEliminar" Text="Cancelar" runat="server" OnClick="btnEliminar_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <div class="form-group row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvGrupo" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="True" AllowSorting="True" DataKeyNames="idgrupo" HorizontalAlign="Center">
                            <Columns>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre Grupo" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblDetalleG" runat="server" OnClick="lblDetalleG_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Ver Parametros</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblEditarGrupo" runat="server" OnClick="lblEditarGrupo_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Editar</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblEliminarG" runat="server" OnClick="lblEliminarG_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  OnClientClick="return confirm('¿Desea Eliminar el Grupo?, esta acción tambien eliminara sus Parámetros');"   >Eliminar</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataRowStyle BorderColor="Blue" ForeColor="Black" />
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <HeaderStyle BackColor="#128f76" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#000065" />
                        </asp:GridView>

                    </div>
                </div>
                <asp:Panel ID="plnDetalle" runat="server">
                    <div class="form-group row">
                        <div class="col-md-4">
                            <asp:Label ID="lblDetalle" runat="server" class="control-label" for="focusedInput" Text="Label"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-4">
                            <asp:ImageButton ID="ImageButton1" runat="server" Height="40px" ImageUrl="~/Images/agregar.png" ToolTip="NUEVO PARAMETRO" Width="40px" OnClick="ImageButton2_Click" />
                            <asp:Button ID="btbOcultar" runat="server" Text="Cerrar Detalle de Parametros" OnClick="btbOcultar_Click" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-8">
                            <asp:GridView ID="gvdetalle" class="table table-striped table-hover" runat="server"
                                AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                                BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" DataKeyNames="idgrupo,idParametro">
                                <Columns>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre Parametro" />
                                     <asp:BoundField DataField="valor" HeaderText="Valor" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblEditarPa" runat="server" OnClick="lblEditarPa_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Editar</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblEliminarP" runat="server" OnClick="lblEliminarP_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %> "  OnClientClick="return confirm('¿Desea Eliminar el Parametro?');">Eliminar</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle BorderColor="Blue" ForeColor="Black" />
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <HeaderStyle BackColor="#128f76" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#000065" />
                            </asp:GridView>

                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="plnDetalleEditar" runat="server">
                    <div class="row">
                        <div class="col-md-12">

                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Nombre Parametro</label>
                                <asp:TextBox ID="txtNombreParametro" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                             <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Valor Parametro</label>
                                <asp:TextBox ID="txtvalorP" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Descripción</label>
                                <asp:TextBox ID="txtDescripcionP" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnGuardarDetalle" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Guardar" OnClick="btnGuardarDetalle_Click" />
                                <asp:Button class="btn btn-danger btnagregarPermiso" ID="btnEliminarDetalle" runat="server" Text="Cancelar" OnClick="btnEliminarDetalle_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Content>


