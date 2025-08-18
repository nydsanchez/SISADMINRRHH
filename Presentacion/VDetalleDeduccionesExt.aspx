<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VDetalleDeduccionesExt.aspx.cs" Inherits="NominaRRHH.Presentacion.VDetalleDeduccionesExt" %>

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

                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                DETALLE DEDUCCIONES
                            </div>
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Mostrar:</label>
                                        <asp:RadioButtonList ID="rbl" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1">General</asp:ListItem>
                                            <asp:ListItem Value="2">Por empleado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Tipo Deduc.</label>
                                        <asp:DropDownList ID="ddlTipDeduc" runat="server" CssClass="form-control">
                                            <%--<asp:ListItem Value="27">CUOTAS</asp:ListItem>--%>
                                            <asp:ListItem Value="29">Cooperativa</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="col-md-1 marginChkActivo">
                                            <div class="checkbox">
                                                <label class="control-label" for="focusedInput">
                                                    <asp:CheckBox ID="ckpagopend" runat="server" />
                                                    <strong>Deduc. Pendientes</strong>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="plnemp" runat="server">
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Codigo Empleado</label>
                                            <asp:TextBox ID="txtcodigoEmp" class="form-control" placeholder="Digite Codigo"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="col-md-1 marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="cbD" runat="server" />
                                                        <strong>Mostrar Cuotas</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </asp:Panel>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">

                                        <asp:Button ID="btnProcesar" Class="btn btn-success" runat="server" Text="Generar Reporte" OnClick="btnProcesar_Click" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="row">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                    </rsweb:ReportViewer>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
