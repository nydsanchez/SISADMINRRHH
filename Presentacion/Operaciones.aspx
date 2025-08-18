<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Operaciones.aspx.cs" Inherits="NominaRRHH.Presentacion.Operaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                    <div>
                        <div class="col-md-3">
                            <asp:Label ID="Label" runat="server">Codigo</asp:Label>
                            <asp:TextBox ID="txtcodigo_operacion" class="form-control"
                                runat="server" autocomplete="off" tooltip="Codigo de Operacion"></asp:TextBox>
                        </div>  
                        <div class="col-md-3">
    <asp:Label ID="Label1" runat="server">Descripcion</asp:Label>
    <asp:TextBox ID="Txtdescripcion" class="form-control"
        runat="server" autocomplete="off" tooltip="Descripcion"></asp:TextBox>
</div>  
                        <div class="col-md-3" style="margin-top: 10px;">
                    <asp:CheckBox ID="chkActivoUp" runat="server" Text="Activo" Tooltip="Activo"/><br>
                </div>
                    </div>
                    </div>

                <div class="row">
                        <div class="col-md-2" style="margin-top: 10px;">
                            <asp:Button class="btn btn-success" ID="BtnAgregar" runat="server" Text="Agregar" OnClick="BtnAgregar_Click" />
                            <asp:Button class="btn btn-info" ID="btnEditar" Visible="false" runat="server" Text="Guardar" OnClick="btnEditar_Click" />
                        </div>
                </div>

                <div class="row">
                    <br />
                    <br />
                    <div class="col-md-16">
                        <asp:GridView ID="gvOperaciones" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" DataKeyNames="codigo_operacion" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvOperaciones_PageIndexChanging" OnSelectedIndexChanged="gvOperaciones_SelectedIndexChanged" PageSize="50" >
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                    <ControlStyle Width="5px" />
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Codigo">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtcodigo_operacion" class="form-control" ReadOnly="true" runat="server" Text='<% # Bind("codigo_operacion")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtdescripcion" class="form-control" ReadOnly="true" autocomplete="off" runat="server" Text='<% # Bind("descripcion")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="240px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Activo">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkActivo" runat="server" Checked='<% # Bind("activo")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings PageButtonCount="50" FirstPageText="&lt;&lt;" LastPageText="&gt;&gt;" NextPageText="-&gt;" PreviousPageText="&lt;-" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
       </div>
    </div>
</asp:Content>

