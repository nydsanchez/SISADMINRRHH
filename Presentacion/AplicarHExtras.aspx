<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AplicarHExtras.aspx.cs" Inherits="NominaRRHH.Presentacion.AplicarHExtras" %>

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
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#aplicar" data-toggle="tab" aria-expanded="true">Aplicar</a></li>
                            <li class=""><a href="#rechazar" data-toggle="tab" aria-expanded="false">Revision</a></li>
                            <li class=""><a href="#pendiente" data-toggle="tab" aria-expanded="false">Pendiente</a></li>
                        </ul>
                        <div id="myTabContent" class="tab-content paddingTab">
                            <div class="tab-pane fade active in" id="aplicar">
                                <asp:Label class="control-label" for="focusedInput" ID="Label4" runat="server" Text="Aplicar HE aprobadas" Style="font-weight: 700"></asp:Label>

                                <div class="row">
                                    <div class="col-md-2" id="divChkDptoVac">
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="ChkEstatus" runat="server" Checked="false" OnCheckedChanged="ChkEstatus_CheckedChanged" AutoPostBack="true" />
                                                <strong>Aplicado</strong>
                                            </label>
                                        </div>
                                    </div>


                                    <div class="col-md-2">
                                        <asp:Label class="control-label" for="focusedInput" ID="Label3" runat="server" Text="Periodo" Style="font-weight: 700"></asp:Label>
                                        <asp:TextBox ID="txtPeriodo" class="form-control" placeholder="Digite un Periodo"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:Button ID="btnBuscar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Consultar" OnClick="btnBuscar_Click" />
                                    </div>
                                    <div class="col-md-2" runat="server" id="divbtn">
                                        <asp:Button ID="Button1" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Aplicar" OnClick="Button1_Click" />
                                    </div>

                                </div>
                                <div class="row" runat="server" id="divdp" visible="false">
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Ver Ingresos Tipo:</label>
                                        <asp:DropDownList class="form-control" ID="ddlIngreso" runat="server" OnSelectedIndexChanged="ddlIngreso_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <br />
                                <div class="row" runat="server" id="divrpt">
                                    <label class="control-label" for="focusedInput">Ingresos Aprobados por dia</label>
                                    <rsweb:ReportViewer ID="ReportViewer3" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                    </rsweb:ReportViewer>
                                </div>

                            </div>
                            <div class="tab-pane fade " id="rechazar">
                                <asp:Label class="control-label" for="focusedInput" ID="Label1" runat="server" Text="Enviar a Revision" Style="font-weight: 700"></asp:Label>
                                <br />
                                <asp:Panel ID="panelID" runat="server">
                                    <div class="row">

                                        <div class="col-md-12" runat="server" id="divFile">

                                            <div class="col-md-5">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cargar Achivo:  "></asp:Label>
                                                <asp:FileUpload ID="fileProtectedDz" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="Button6" Class="btn btn-success" runat="server" Text="Procesar" OnClick="Button6_Click" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="Button2" Class="btn btn-success" runat="server" Text="Guardar" OnClick="Button2_Click" Visible="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-8" runat="server" id="divGrid">
                                            <asp:GridView ID="gvING" class="table table-striped table-hover" runat="server">
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </asp:Panel>

                            </div>
                            <div class="tab-pane fade " id="pendiente">
                                <asp:Label class="control-label" for="focusedInput" ID="Label5" runat="server" Text="Mostrar Horas Extras Pendientes de Aprobacion" Style="font-weight: 700"></asp:Label>
                                <br />
                                <div class="row">
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Fecha Inicio</label>
                                        <div class="input-group input-append date" id="datePicker">
                                            <asp:TextBox ID="txtFechaIni" class="form-control datepicker"
                                                runat="server"></asp:TextBox>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Fecha Fin</label>
                                        <div class="input-group input-append date" id="datePicker2">
                                            <asp:TextBox ID="txtFechaFin" class="form-control datepicker"
                                                runat="server"></asp:TextBox>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
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
