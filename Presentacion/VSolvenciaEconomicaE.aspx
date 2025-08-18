<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VSolvenciaEconomicaE.aspx.cs" Inherits="NominaRRHH.Presentacion.VSolvenciaEconomicaE" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container mar-top">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="lblAlert" runat="server"></asp:Label>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <label class="control-label" for="focusedInput">DETALLE DE SOLVENCIA ECONOMICA</label>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-12">
                        <label class="col-sm-4 col-md-4 col-xs-4 control-label" for="focusedInput">Mostrar:</label>
                        <div class="col-md-5 col-sm-5 col-xs-5">

                            <asp:RadioButtonList ID="rbl" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="1">General</asp:ListItem>
                                <asp:ListItem Value="2">Por empleado</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
             
                <asp:Panel ID="plnemp" runat="server">
                    <div class="form-group row">
                        <div class="col-md-12">
                            <div class="form-group col-md-12 col-sm-12 col-xs-12">

                                <label class="col-sm-4 col-md-4 col-xs-4 control-label" for="focusedInput">Codigo Empleado</label>

                                <div class="col-md-4 col-sm-4 col-xs-4">

                                    <asp:TextBox ID="txtcodigoEmp" class="form-control" placeholder="Digite Codigo"
                                        runat="server" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>
                </asp:Panel>
                <div class="form-group row">
                    <div class="col-md-12">
                        <div class="form-group col-md-12 col-sm-12 col-xs-12">
                            <div class="col-md-3 col-sm-3 col-xs-5">
                                <asp:Button ID="btnProcesar" Class="btn btn-success" runat="server" Text="Generar Reporte" OnClick="btnProcesar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <%--<div class="row">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1084px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                    </rsweb:ReportViewer>
                </div>--%>
                  <div class="row">
                    <div class="col-md-12">
                        <div>
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1122px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                <LocalReport ReportEmbeddedResource="NominaRRHH.Reportes.SolvenciaE.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="CompanicsaRHDataSetTableAdapters.SolvenciaTableAdapter"></asp:ObjectDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
