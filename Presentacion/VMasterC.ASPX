<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VMasterC.aspx.cs" Inherits="NominaRRHH.Presentacion.VMasterC" %>

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
                    <asp:Label ID="lblAlert" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="LblSuccess" runat="server" Visible="false"></asp:Label>
                </div>

                <div class="form-group row">
                    <div class="col-md-12">
                        <div class="form-group col-md-12 col-sm-12 col-xs-12">


                            <div class="col-md-4 col-sm-4 col-xs-4">
                                <asp:Button ID="btnProcesar" Class="btn btn-success" runat="server" Text="GENERAR REPORTE" OnClick="btnProcesar_Click" />
                            </div>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="618px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                    </rsweb:ReportViewer>
                </div>
                <div class="row">
                </div>

            </div>
        </div>
    </div>

</asp:Content>
