<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VDetallePrestamo.aspx.cs" Inherits="NominaRRHH.Presentacion.VDetallePrestamo" %>

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
                <div class="form-group row">
                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                DETALLE DEDUCCIONES
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
                            <div class="form-group row">
                                <div class="col-md-12">
                                    <div class="form-group col-md-8 col-sm-8 col-xs-8">

                                        <label class="col-sm-4 col-md-4 col-xs-4 control-label" for="focusedInput">Tipo Deduc.</label>
                                        <div class="col-md-3 col-sm-3 col-xs-3">
                                            <asp:DropDownList ID="ddlTipDeduc" runat="server"></asp:DropDownList>
                                        </div>
                                         
                                    </div>
                                    <div class="col-md-4 col-sm-4 col-xs-4">
                                                <asp:CheckBox ID="ckpagopend" runat="server" Text="Deduc. Pendientes" />
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
                                            <div class="col-md-4 col-sm-4 col-xs-4">
                                                <asp:CheckBox ID="cbD" runat="server" Text="Mostrar Cuotas" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>
                            <div class="form-group row">
                                <div class="col-md-12">
                                    <div class="form-group col-sm-offset-4 col-xs-offset-4 col-md-offset-4 col-md-4 col-sm-4 col-xs-4">
                                        <div class="col-md-3 col-sm-3 col-xs-5">
                                            <asp:Button ID="btnProcesar" Class="btn btn-success" runat="server" Text="Generar Reporte" OnClick="btnProcesar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                CONSULTA DEVOLVENTE
                            </div>
                            <asp:Panel ID="plnRevolventes" runat="server">
                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <div class="form-group col-md-12 col-sm-12 col-xs-12">

                                            <label class="col-sm-4 col-md-4 col-xs-4 control-label" for="focusedInput">Tipo Visualizacion</label>
                                            <div class="col-md-6 col-sm-6 col-xs-6">
                                                <asp:RadioButtonList ID="rbtv" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbtv_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="1">General</asp:ListItem>
                                                    <asp:ListItem Value="2">Periodo</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                  <div class="form-group row">
                                <div class="col-md-12">
                                    <div class="form-group col-md-12 col-sm-12 col-xs-12">

                                        <label class="col-sm-4 col-md-4 col-xs-4 control-label" for="focusedInput">Tipo Deduccion</label>
                                        <div class="col-md-3 col-sm-3 col-xs-3">
                                            <asp:DropDownList ID="dlldD" runat="server"></asp:DropDownList>
                                        </div>

                                    </div>
                                </div>
                            </div>
                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <div class="form-group col-md-12 col-sm-12 col-xs-12">

                                            <label class="col-sm-4 col-md-4 col-xs-4 control-label" for="focusedInput">Periodo</label>
                                            <div class="col-md-4 col-sm-4 col-xs-4">
                                                <asp:TextBox ID="tbPeriodoIni" class="form-control" placeholder="Digite Periodo"
                                                    runat="server" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="form-group col-md-4 col-sm-4 col-xs-4">
                                        <div class="form-group  col-md-4 col-sm-46 col-xs-4">
                                            <asp:Button ID="btnprocesarD" Class="btn btn-success" runat="server" Text="Obtener Devolvente" OnClick="btnprocesarD_Click" />

                                        </div>
                                    </div>
                                    <div class="form-group col-md-8 col-sm-8 col-xs-8">
                                        <label class="col-sm-4 col-md-4 col-xs-4 control-label" for="focusedInput">Devolvente</label>
                                        <div class="col-md-10 col-sm-10 col-xs-10">
                                            <asp:Label ID="lblRevolvte" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>

                                </div>

                                <div class="form-group row">
                                    <div class="col-md-12">
                                    </div>
                                </div>
                            </asp:Panel>
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
