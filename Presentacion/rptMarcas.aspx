<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rptMarcas.aspx.cs" Inherits="NominaRRHH.Presentacion.rptMarcas" %>

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
                    <div class="col-md-12" style="margin-left: 95px;">
                        <div class="col-md-1">
                            <label class="control-label" for="focusedInput">Periodo</label>
                            <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Semana</label>
                            <asp:DropDownList class="form-control" ID="ddlSemana" runat="server">
                                <asp:ListItem Value="1"> 1</asp:ListItem>
                                <asp:ListItem Value="2"> 2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                         <div class="col-md-2">
                            <asp:Button ID="btnProcesarMarcas" Class="btn btn-success" Style="margin-top: 22px; margin-left: 10px;" runat="server" Text="Procesar" OnClick="btnProcesarMarcas_Click"/>
                        </div>
                    </div>
                </div><br /><br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="GvMarcasMenoresTurno" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GvMarcasMenoresTurno_PageIndexChanging">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                <asp:BoundField DataField="codigo_empleado" HeaderText="Codigo">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombrecompleto" HeaderText="Nombre">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="departamento" HeaderText="Departamento">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="periodo" HeaderText="Periodo">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="semana" HeaderText="Semana">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horasT" HeaderText="Horas">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                         <div class="col-md-2">
                            <asp:Button ID="btnExportar" Visible="false" Class="btn btn-success" Style="margin-top: 22px; margin-left: 10px;" runat="server" Text="Exportar Excel"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

