<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VCarta.aspx.cs" Inherits="NominaRRHH.Presentacion.VCarta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top">
        <div class="panel panel-info">
            <div class="panel-body" style="margin-top: 0px">
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
                        <div class="col-md-3" style="margin-left: 406px;">
                            <label class="control-label" for="focusedInput">Impresion de Contrato Laboral</label>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Buscar Empleado</label>
                            <asp:TextBox ID="TxtBuscar" class="form-control" placeholder="Digite el Codigo"
                                runat="server" autocomplete="off"></asp:TextBox>
                            <br />
                        </div>
                        <div>
                            <asp:Button ID="BtnBuscar" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Buscar" OnClick="BtnBuscar_Click" />
                        </div>

                    </div>
                    <asp:GridView ID="GVBenEmpleado" class="table table-striped table-hover" runat="server" CellPadding="4" GridLines="None" EmptyDataText="No hay Datos" DataKeyNames="Codigo"
                        AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="GVBenEmpleado_SelectedIndexChanged"
                        AutoGenerateColumns="False" ForeColor="#333333">
                        <AlternatingRowStyle Width="100px" BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="Codigo" HeaderText="Codigo" HtmlEncode="False"></asp:BoundField>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" HtmlEncode="False"></asp:BoundField>
                            <asp:BoundField DataField="Domicilio" HeaderText="Domicilio" />
                            <asp:BoundField DataField="Cedula" HeaderText="Cedula" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" />
                            <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
                            <asp:BoundField DataField="Salario" HeaderText="Salario" />
                            <asp:BoundField DataField="fecha_ingreso" HeaderText="Ingreso" />
                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Imp">
                                <ControlStyle Width="5px" />
                                <HeaderStyle Width="5px" />
                                <ItemStyle Width="5px" />
                            </asp:CommandField>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
