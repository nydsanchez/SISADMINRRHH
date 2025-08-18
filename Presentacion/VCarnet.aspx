<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VCarnet.aspx.cs" Inherits="NominaRRHH.Presentacion.VCarnet" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
                    <div class="col-md-4">
                        <label class="control-label" for="focusedInput">Impresion de Carnet</label>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <%--<label class="control-label" for="focusedInput"></label>--%>
                            <asp:TextBox ID="TxtCarnet" class="form-control" placeholder="Digite un Codigo de Carnet"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnProcesar" Class="btn btn-success" runat="server" Text="Agregar" OnClick="btnProcesar_Click" />
                        </div>
                        <div class="col-md-2">
                            <asp:ListBox ID="LbtCodigos" class="form-control" runat="server" Width="150px"></asp:ListBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnMostrar" Visible="true" Class="btn btn-info" runat="server" Text="Aceptar" OnClick="btnMostrar_Click" />
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="CompanicsaRHDataSetTableAdapters.spPreplanillaIngTableAdapter"></asp:ObjectDataSource>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1000px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <LocalReport ReportEmbeddedResource="NominaRRHH.Reportes.Carnet.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" runat="server" Name="DataSet1" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData" TypeName="HRDataSet1TableAdapters.spCarnetTableAdapter"></asp:ObjectDataSource>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

