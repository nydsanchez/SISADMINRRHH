<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteINSSCatorcenalAPI.aspx.cs" Inherits="NominaRRHH.ReporteINSSCatorcenalAPI" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script> 
    <style>
       div#ltDatosHtml table tr td{
           padding: 1px 8px;
           border: 1px #797070 solid;
       }
    </style>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container mar-top">

        <%--<div class="panel panel-info">
          
            <div class="panel-body">--%>
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
                            <asp:Button ID="btnConsultar" Visible="true" Class="btn btn-info" runat="server" Text="Consultar" Style="margin-top: 25px;" OnClick="btnConsultar_Click" />
                        </div>

                  
                       <div class="col-md-1">
                           <label class="control-label" for="focusedInput" id="lblExportar" runat="server" visible="false">Exportar</label>
                            <div><asp:Button ID="btnExpTexto" Visible="False" Class="btn btn-info" runat="server" Text="Texto" OnClick="btnExpTexto_Click"/></div>
                        </div>
                      <div class="col-md-1">
                          <asp:Button ID="btnExpXml" Visible="False" Class="btn btn-info" runat="server" Text="XML" Style="margin-top: 25px;" OnClick="btnExpXml_Click"/> 
                         </div>
                      <div class="col-md-1"><div ID="btnExportExcel" Class="btn btn-info" Style="margin-top: 25px;">Excel </div></div>
                        <div class="col-md-3"></div>
                    </div>
                    </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server" >
                    </asp:ScriptManager>
                         
            </div>
     <div runat="server" ID="ltDatosHtml" style="margin-top: 30px;" ClientIDMode="Static"></div>
    
     

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
            //data:text/csv;charset=utf-8,
        });
        $("#btnExportExcel").click(function (e) {
            window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('#ltDatosHtml').html()));
            e.preventDefault();
        });        
    </script>
</asp:Content>
