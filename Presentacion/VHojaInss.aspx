<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VHojaInss.aspx.cs" Inherits="NominaRRHH.VistaReportes.VHojaInss" %>

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
                            <label class="control-label" runat="server" for="focusedInput">Tipo de Reporte</label>
                            <asp:DropDownList class="form-control" ID="ddlTipo" runat="server">
                                <asp:ListItem Value="0">Hoja del Inss</asp:ListItem>
                                <asp:ListItem Value="1">Solicitud de Empleo</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Buscar</label>
                            <asp:TextBox ID="txtCodigo" class="form-control" placeholder="Digite un Codigo de Empleado"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                         <div class="col-md-3">
                            <asp:Button ID="btnProcesar" Class="btn btn-success" runat="server" Style="margin-top: 25px" Text="Aceptar" OnClick="btnProcesar_Click" />
                        </div>
                        <div class="col-md-3">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-8">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="" Font-Names="Verdana" Font-Size="8pt" AsyncRendering="False" SizeToReportContent="True" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                <LocalReport ReportEmbeddedResource="NominaRRHH.Reportes.HojaInss.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource3" Name="DataSet1" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="GetData" TypeName="CompanicsaRHDataSetTableAdapters.spMostrarEmpleadoTableAdapter"></asp:ObjectDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

