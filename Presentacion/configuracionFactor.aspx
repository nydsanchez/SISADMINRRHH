<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="configuracionFactor.aspx.cs" Inherits="NominaRRHH.Presentacion.configuracionFactor" %>

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
                    <div class="col-md-12" style="margin-left: 222px;">
                        <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Factor Hora</label>
                            <asp:TextBox ID="txtFactor" class="form-control"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <div class="col-md-1 marginChkActivo">
                                <div class="checkbox">
                                    <label class="control-label" for="focusedInput">
                                        <asp:CheckBox ID="ChkActivo" runat="server" />
                                        <strong>Activo</strong>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Button class="btn btn-success btnagregarPermiso" ID="BtnAgregar" runat="server" Text="Editar" OnClick="BtnAgregar_Click" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="GVfactorHora" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" OnSelectedIndexChanged="GVfactorHora_SelectedIndexChanged">
                            <AlternatingRowStyle Width="578px" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                    <ControlStyle Width="5px" />
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:CommandField>
                                <asp:BoundField DataField="id_Factor" HeaderText="ID">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="factor" HeaderText="Factores">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Activo">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAct" runat="server" Checked='<% # Bind("activo")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

