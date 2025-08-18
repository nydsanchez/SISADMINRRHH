<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncAsignarProteccion.aspx.cs" Inherits="NominaRRHH.Presentacion.IncAsignarProteccion" MaintainScrollPositionOnPostback="true" %>

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

                                <div class="row">
                                    <asp:Label class="control-label" for="focusedInput" ID="Label3" runat="server" Text="Protecciones Individuales" Style="font-weight: 700"></asp:Label>

                                    <div class="col-md-12 ">
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Codigo</label>
                                            <asp:TextBox ID="txtCodigo" class="form-control" autocomplete="off" TabIndex="4"
                                                runat="server" OnTextChanged="txtCodigo_TextChanged" AutoPostBack="true" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Nombre</label>
                                            <asp:TextBox ID="TxtNombreE" class="form-control"
                                                runat="server" autocomplete="off" ReadOnly="true" TabIndex="5"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Bono Asistencia</label>
                                            <asp:TextBox ID="txtAsistencia" class="form-control"
                                                runat="server" autocomplete="off" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Viatico</label>
                                            <asp:TextBox ID="txtViatico" class="form-control"
                                                runat="server" autocomplete="off" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Bono Calidad</label>
                                            <asp:TextBox ID="txtBonoCalidad" class="form-control"
                                                runat="server" autocomplete="off" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Porcentaje(1-100)</label>
                                            <asp:TextBox ID="TxtPorcentaje" class="form-control"
                                                runat="server" autocomplete="off" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Estilo</label>
                                            <asp:TextBox ID="TxtEstilo" class="form-control"
                                                runat="server" autocomplete="off" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnConsultar" Class="btn btn-info" Style="margin-top: 22px;" runat="server" Text="Buscar" OnClick="btnConsultar_Click" />
                                        </div>
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Semanas</label>
                                            <asp:TextBox ID="txtRepeticiones" class="form-control"
                                                runat="server" autocomplete="off" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="ChkRecurrente" runat="server" />
                                                        <strong>Recurrente</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="ChkActivo" runat="server" Checked="true" />
                                                        <strong>Activo</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnAgregarDed" Class="btn btn-info" Style="margin-top: 22px;" runat="server" Text="Aplicar" OnClick="btnAgregarDed_Click" />
                                        </div>
                                        
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-md-12 ">
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Periodo</label>
                                            <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" autocomplete="off" TabIndex="2" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="Button2" Class="btn btn-success" runat="server" Text="Ver" OnClick="Button2_Click" Style="margin-top: 22px;"/>


                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row" visible="false" runat="server" id="div1">
                                    <rsweb:ReportViewer ID="ReportViewer2" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                    </rsweb:ReportViewer>
                                </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">

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

