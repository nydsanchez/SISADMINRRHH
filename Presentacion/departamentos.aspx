<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="departamentos.aspx.cs" Inherits="NominaRRHH.Presentacion.departamentos" %>

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
                    <div >
                        <div class="col-md-3">
                            <asp:Label ID="LblDepartamento" runat="server">Departamento</asp:Label>
                            <asp:TextBox ID="txtdepartamento" class="form-control"
                                runat="server" autocomplete="off" tooltip="Nombre Departamento"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="Label1" runat="server">C. Costo</asp:Label>
                            <asp:TextBox ID="txtCentroCosto" class="form-control" Width="80px" ToolTip="Centro de Costo"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                        <asp:Label ID="Label2" runat="server">Email Jefe</asp:Label>
                        <asp:TextBox ID="txtMailJefeUp" class="form-control" Width="150px" ToolTip="Usado para envio de reportes por area"
                         runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
<asp:Label ID="Label3" runat="server">C Jefe</asp:Label>
<asp:TextBox ID="txtIdJefeUp" class="form-control" Width="80px" ToolTip="Codigo empleado del jefe de area"
 runat="server" autocomplete="off"></asp:TextBox>
</div>
                        <div class="col-md-1">
<asp:Label ID="Label4" runat="server">C Padre</asp:Label>
<asp:TextBox ID="txtCodigoPadreUp" class="form-control" Width="80px" ToolTip="Codigo del departamento padre"
runat="server" autocomplete="off"></asp:TextBox>
</div>
                        <div class="col-md-3" style="margin-top: 10px;">
    <asp:CheckBox ID="chkCostoDirUp" runat="server" Text="Costo Directo" Tooltip="Costo Directo"/><br>
    <asp:CheckBox ID="chkActivoUp" runat="server" Text="Activo" Tooltip="Activo"/><br>
    <asp:CheckBox ID="chkOmitirPlUp" runat="server" Text="Omitir en Planilla" Tooltip="No procesar en la planilla"/>
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
                        <asp:GridView ID="gvDepartamentos" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" DataKeyNames="codigo_depto" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvDepartamentos_PageIndexChanging" OnSelectedIndexChanged="gvDepartamentos_SelectedIndexChanged" PageSize="50" >
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                    <ControlStyle Width="5px" />
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="Codigo">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCodigo" class="form-control" ReadOnly="true" runat="server" Text='<% # Bind("codigo_depto")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNombre" class="form-control" ReadOnly="true" autocomplete="off" runat="server" Text='<% # Bind("nombre_depto")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="240px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Centro Costo">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCentro" class="form-control" ReadOnly="true" autocomplete="off" runat="server" Text='<% # Bind("centrocosto")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Codigo Padre">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtidpadre" class="form-control" ReadOnly="true" autocomplete="off" runat="server" Text='<% # Bind("idpadre")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px"/>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Email Jefe">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMailJefe" class="form-control" ReadOnly="true" autocomplete="off" runat="server" Text='<% # Bind("jefe")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="200px"/>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Codigo Jefe">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIdJefe" class="form-control" ReadOnly="true" autocomplete="off" runat="server" Text='<% # Bind("idjefe")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Omitir Planilla">
                                 <ItemTemplate>
                                 <asp:CheckBox ID="chkOmitirPlanilla" runat="server" Checked='<% # Bind("omitirpl")%>' />
                                 </ItemTemplate>
                                 <ItemStyle Width="80px" />
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Directo">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDir" runat="server" Checked='<% # Bind("costodirecto")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
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

