<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VReportePlanillaVacaciones.aspx.cs" Inherits="NominaRRHH.Presentacion.VReportePlanillaVacaciones" %>

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
                <div class=" row">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <label class="control-label" for="focusedInput">Planilla de Vacaciones</label>
                        </div>
                    </div>
                    </div>
                <div class="form-group row">
                                <div class="col-md-12">
                                    <label class="col-sm-4 col-md-4 col-xs-4 control-label" for="focusedInput">Mostrar:</label>
                                    <div class="col-md-5 col-sm-5 col-xs-5">

                                        <asp:RadioButtonList ID="rbl" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbl_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1">Por Periodo</asp:ListItem>
                                            <asp:ListItem Value="2">Por Fecha</asp:ListItem>
                                            <asp:ListItem Value="3">Por empleado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                    <div class=" row">
                    <div class="col-md-12">
                        <div class="col-md-2" id="divperiodo" runat="server">
                            <label class="control-label" for="focusedInput">Periodo</label>
                            <asp:TextBox ID="txtPeriodo" class="form-control" placeholder="Digite un Periodo"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2" id="divcodigo" runat="server">
                            <label class="control-label" for="focusedInput">Codigo</label>
                            <asp:TextBox ID="txtcodigo" class="form-control" placeholder="Digite un Codigo"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                         <div id="divrango" runat="server">
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Fecha Inicio</label>
                                <div class="input-group input-append date" id="datePicker3">
                                    <asp:TextBox ID="txtFechaIni2" class="form-control datepicker"
                                        runat="server"></asp:TextBox>
                                    <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Fecha Fin</label>
                                <div class="input-group input-append date" id="datePicker4">
                                    <asp:TextBox ID="txtFechaFin2" class="form-control datepicker"
                                        runat="server"></asp:TextBox>
                                    <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                              
                            </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnProcesar" Class="btn btn-success" runat="server" Style="margin-top: 25px" Text="Procesar" OnClick="btnProcesar_Click" />
                        </div>
                    </div>
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12" style="margin-left: 256px;">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1000px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            </rsweb:ReportViewer>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#datePicker3,#datePicker4').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });
        $('.clockpicker').clockpicker();

        function soloNumeros(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "0123456789.";
            especiales = "8-37-39-46";

            tecla_especial = false
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }

            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }

    </script>
</asp:Content>


