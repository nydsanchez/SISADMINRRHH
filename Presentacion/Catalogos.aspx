<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Catalogos.aspx.cs" Inherits="NominaRRHH.Presentacion.Catalogos" %>

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

                    <div class="col-md-12">
                        
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Grupo</label>
                            <asp:DropDownList class="form-control" ID="ddlGrupo" runat="server">
                                                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Descripcion</label>
                            <asp:TextBox ID="txtDescrp" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                                                                                                        <asp:Button ID="btnAgregar" Class="btn btn-info" Style="margin-top: 22px;" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />

                    </div>
                </div>

                <br />
                 <div class="row">

                    <div class="col-md-12">
                        
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Filtrar por Grupo</label>
                            <asp:DropDownList class="form-control" ID="ddlfiltro" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfiltro_SelectedIndexChanged">
                                                            </asp:DropDownList>
                        </div>
                        
                    </div>
                </div>

                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="Gvgrupos" class="table table-striped table-hover" runat="server" Width="100%"
                            AutoGenerateColumns="False" DataKeyNames="id_grupo,descripcion" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" OnRowDataBound="Gvgrupos_RowDataBound"
                            AllowPaging="True" AllowSorting="True" OnRowCommand="Gvgrupos_RowCommand" OnPageIndexChanging="Gvgrupos_PageIndexChanging">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                
                                <asp:TemplateField HeaderText="Grupo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrupo" runat="server" Text='<%# Eval("id_grupo") %>' Visible="false" />
                                        <asp:DropDownList runat="server" ID="ddlGrupoGv">
                                           
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" />
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Descripcion">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDesc" class="form-control" autocomplete="off" runat="server" Text='<% # Bind("descripcion")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btneditar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="editar" runat="server" Text="Editar" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Eliminar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btneliminar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="eliminar" runat="server" Text="Eliminar" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16px" />
                                                </asp:TemplateField>

                                <%--                                                <asp:ButtonField ButtonType="Button" Text="Editar" CommandName="editar">
                                                    <ControlStyle CssClass="btn btn-success btn-xs" />
                                                    <ItemStyle Width="16px" />
                                                </asp:ButtonField>
                                                <asp:ButtonField ButtonType="Button" CommandName="eliminar" Text="Eliminar">
                                                    <ControlStyle CssClass="btn btn-danger btn-xs" />
                                                    <ItemStyle Width="16px" />
                                                </asp:ButtonField>--%>
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
