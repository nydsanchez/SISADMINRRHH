<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteAusentismo.aspx.cs" Inherits="NominaRRHH.Presentacion.ReporteAusentismo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
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

                <br />
                <div class="row" visible="true" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-5">

                            <%--<asp:CheckBox ID="cbcodigo" runat="server" OnCheckedChanged="cbcodigo_CheckedChanged" TextAlign="Left" />--%>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div runat="server" class="col-md-2" id="dvparamcodigo">
                            <%-- <label class="control-label" for="focusedInput">Codigo</label>
                            <asp:TextBox ID="txtCodigo" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>--%>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Fecha Inicio</label>
                            <div class="input-group input-append date" id="datePicker">
                                <asp:TextBox ID="txtFecha" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" runat="server" id="calendarFin">
                            <label class="control-label" for="focusedInput">Fecha Fin</label>
                            <div class="input-group input-append date" id="datePicker2">
                                <asp:TextBox ID="TxtFecha2" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span runat="server" id="borcalendar" class="input-group-addon add-on borderCalendar"><span runat="server" id="calendar" class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <asp:Button ID="btnEditar" Visible="true" Class="btn btn-info" runat="server" Text="Consultar" Style="margin-top: 25px;" OnClick="btnEditar_Click" />

                        </div>
                    </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <div class="row">
                        <div class="col-md-12" ">

                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1100px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            </rsweb:ReportViewer>
                        </div>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
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

        function soloNumerosHora(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "0123456789:";
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

        $("#MainContent_btnCrear").click(function () {

            if ($("#MainContent_txtHoraE").val().length < 5) {
                alert("El formato de Hora Entrada no es correcto");
                return false;
            }

            if ($("#MainContent_txtHoraS").val().length < 5) {
                alert("El formato de Hora Salida no es correcto");
                return false;
            }
        });

        $("#MainContent_btnEditar").click(function () {

            if ($("#MainContent_txtHoraE").val().length < 5) {
                alert("El formato de Hora Entrada no es correcto");
                return false;
            }

            if ($("#MainContent_txtHoraS").val().length < 5) {
                alert("El formato de Hora Salida no es correcto");
                return false;
            }
        });

        $(function () {
            $('#datePicker').datepicker({
                format: 'dd/mm/yyyy'
            })
                .on('changeDate', function (e) {
                    $('.datepicker dropdown-menu').hide();
                });

            $('#datePicker2').datepicker({
                format: 'dd/mm/yyyy'
            })
                .on('changeDate', function (e) {
                    $('.datepicker2 dropdown-menu').hide();
                });

        });
    </script>
</asp:Content>
