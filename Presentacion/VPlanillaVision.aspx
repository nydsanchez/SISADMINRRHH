<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VPlanillaVision.aspx.cs" Inherits="NominaRRHH.Presentacion.VPlanillaVision" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
                                            <label class="control-label" for="focusedInput">Ubicacion</label>
                                            <asp:DropDownList class="form-control" ID="ddlUbicacion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUbicacion_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Tipos De Planilla</label>
                                            <asp:DropDownList class="form-control" ID="ddlTiposPlanilla" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTiposPlanilla_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Periodo</label>
                                            <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                    <div class="col-md-3">
                            <asp:Button ID="btnProcesar" Class="btn btn-success" runat="server" Style="margin-top: 25px" Text="Aceptar" OnClick="btnProcesar_Click" />
                        </div>
                                        </div>
                                    </div>         
               
                <br />
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1039px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                <LocalReport ReportEmbeddedResource="NominaRRHH.Reportes.PlanillaVision.rdlc">
                                    <DataSources>
                                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                                    </DataSources>
                                </LocalReport>
                            </rsweb:ReportViewer>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="CompanicsaRHDataSetTableAdapters.spPlanillaVisionSelTableAdapter"></asp:ObjectDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
