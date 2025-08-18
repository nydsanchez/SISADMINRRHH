<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VPlanillaIncentivos.aspx.cs" Inherits="NominaRRHH.VPlanillaIncentivos" %>

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
                        <label class="control-label" for="focusedInput">Informes de Planilla</label>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label class="control-label" for="focusedInput" ID="Label1" runat="server" Text="Periodo" Style="font-weight: 700"></asp:Label>
                            <asp:TextBox ID="txtPeriodo" class="form-control" placeholder="Digite un Periodo"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <%--<label class="control-label" runat="server"  for="focusedInput">Semana</label>--%>
                            <asp:Label class="control-label" for="focusedInput" ID="lblSemana" runat="server" Text="Semana" Style="font-weight: 700"></asp:Label>
                            <asp:DropDownList class="form-control" ID="ddlTipo" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>Consolidado</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-3">
                            <asp:Button ID="btnProcesar" Class="btn btn-success" runat="server" Style="margin-top: 25px; width: 104px;" Text="Aceptar" OnClick="btnProcesar_Click" />
                        </div>
                    </div>
                </div>
                <br />
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <div class="row">
                    <div class="col-md-12">
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="CompanicsaRHDataSetTableAdapters.spPreplanillaIngTableAdapter"></asp:ObjectDataSource>
                        <rsweb:reportviewer id="ReportViewer1" runat="server" width="1122px" font-names="Verdana" font-size="8pt" waitmessagefont-names="Verdana" waitmessagefont-size="14pt" processingmode="Remote">
                        </rsweb:reportviewer>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
