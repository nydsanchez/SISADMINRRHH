<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VDetalleMarcas.aspx.cs" Inherits="NominaRRHH.Presentacion.VDetalleMarcas" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Detalle de Marcas</label>
                            <%--</br>--%>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Periodo</label>
                            <asp:TextBox ID="txtperiodo" class="form-control" placeholder="Digite el Periodo"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Semana</label>
                            <asp:DropDownList class="form-control" ID="ddlsemana" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Depto 1</label>
                            <asp:DropDownList class="form-control" ID="ddldepto1" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Depto 2</label>
                            <asp:DropDownList class="form-control" ID="ddldepto2" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnMostrar" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Aceptar" OnClick="btnMostrar_Click" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-11">
                        <div>
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1110px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                <LocalReport ReportEmbeddedResource="NominaRRHH.Reportes.Preplanilla.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div>
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



