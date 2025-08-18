<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeclaracionHExtras.aspx.cs" Inherits="NominaRRHH.Presentacion.DeclaracionHExtras" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
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
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label class="control-label" for="focusedInput" ID="Label5" runat="server" Text="Declaracion de Horas Extras" Style="font-weight: 700"></asp:Label>
                                <br />
                                <div class="row">
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Fecha Ini</label>
                                        <div class="input-group input-append date" id="datePicker">
                                            <asp:TextBox ID="txtFechaIni" class="form-control datepicker"
                                                runat="server"></asp:TextBox>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                     <div class="col-md-2" runat="server" visible="false" id="divffin">
                                <label class="control-label" for="focusedInput">Fecha Fin</label>
                                <div class="input-group input-append date" id="datePicker2">
                                    <asp:TextBox ID="txtFechaFin" class="form-control datepicker"
                                        runat="server"></asp:TextBox>
                                    <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                                  <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Agrupar?</label>
                                       <asp:CheckBox runat="server" ID="ckagrupar" AutoPostBack="true" Checked="true" OnCheckedChanged="ckagrupar_CheckedChanged"/>
                                    </div>
                                     <div class="col-md-3" runat="server" id="divtipos" visible="false">
                                        <label class="control-label" for="focusedInput">Ver Ingresos Tipo:</label>
                                        <asp:DropDownList class="form-control" ID="ddlIngreso" runat="server" >
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3" runat="server" id="divgrupos" visible="true">
                                        <label class="control-label" for="focusedInput">Ver Ingresos Grupo:</label>
                                        <asp:DropDownList class="form-control" ID="ddlGrupo" runat="server" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged" AutoPostBack="true" >
                                            <asp:ListItem Value="HE">Horas Extras</asp:ListItem>
                                            <asp:ListItem Value="Feriado">Horas Feriado</asp:ListItem>
                                            <asp:ListItem Value="NA">Errores Imputables</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="Button3" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Buscar" OnClick="Button3_Click"/>
                                    </div>


                                </div>


                                <br />
                                <div class="row" runat="server" id="div3">

                                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                    </rsweb:ReportViewer>
                                </div>
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
        $(function () {
            $('#datePicker,#datePicker2').datepicker({
                format: 'dd/mm/yyyy'
            })
                .on('changeDate', function (e) {
                    $('.datepicker dropdown-menu').hide();
                });



        });
    </script>
</asp:Content>
