<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportPlanillaRevision.aspx.cs" Inherits="NominaRRHH.Presentacion.ReportPlanillaRevision" %>

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
                <div class="col-md-12">
                    <div class="col-md-2">
                        <asp:Label class="control-label" for="focusedInput" ID="Label1" runat="server" Text="Periodo" Style="font-weight: 700"></asp:Label>
                        <asp:TextBox ID="txtperiodo" class="form-control" placeholder="Digite un Periodo"
                            runat="server" autocomplete="off" OnTextChanged="txtperiodo_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <%--<label class="control-label" runat="server"  for="focusedInput">Semana</label>--%>
                        <asp:Label class="control-label" for="focusedInput" ID="lblSemana" runat="server" Text="Semana" Style="font-weight: 700"></asp:Label>
                        <asp:DropDownList class="form-control" ID="ddlTipo" runat="server">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                        </asp:DropDownList>
                    </div>


                </div>
                <div class="row">

                    <div class="col-xs-4">
                        <label class="control-label" for="focusedInput">Tipo</label>
                        <%--<asp:DropDownList class="form-control" ID="ddltipor" runat="server" OnSelectedIndexChanged="ddltipor_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="1">Con Incentivo</asp:ListItem>
                            <asp:ListItem Value="2">Sin Incentivo</asp:ListItem>
                            <%--  <asp:ListItem Value="3">General</asp:ListItem>--%>
                        <%--</asp:DropDownList>--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12">


                        <div class="col-xs-9">
                            <asp:Panel ID="pnlcheck" runat="server">
                                <div class="checkbox">
                                    <label>

                                        <asp:RadioButtonList ID="rbllistImpresion" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbllistImpresion_SelectedIndexChanged" AutoPostBack="True" TextAlign="Left">
                                            <asp:ListItem Value="1">Por Módulo</asp:ListItem>
                                            <asp:ListItem Value="2">Por Rango</asp:ListItem>
                                            <asp:ListItem Value="3" Selected="True">TODOS</asp:ListItem>
                                        </asp:RadioButtonList>

                                    </label>
                                </div>
                            </asp:Panel>
                        </div>



                        <br />
                        <div>
                        </div>

                    </div>


                </div>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="col-xs-3">
                            <asp:Panel ID="pnlModulo" runat="server">
                                <label class="control-label" for="focusedInput">Módulos</label>
                                <asp:DropDownList class="form-control" ID="ddlProceso" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </asp:Panel>
                        </div>


                        <div class="col-md-6" style="margin-left: 110px; margin-top: 15px;">
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-4">
                        <asp:Panel ID="pnlCodigo" runat="server">
                            <label class="control-label" for="TxtCodigoE">Del Módulo: </label>
                            <asp:TextBox ID="txtmoduloini" class="form-control" placeholder="Digite el Módulo"
                                runat="server" autocomplete="off"></asp:TextBox>
                            <label class="control-label" for="TxtCodigoE">Al Módulo: </label>
                            <asp:TextBox ID="txtmodulohasta" class="form-control" placeholder="Digite el Módulo"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12" style="margin-top: 26px;">
                        <asp:Button ID="btnAceptar" Class="btn btn-success" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
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

