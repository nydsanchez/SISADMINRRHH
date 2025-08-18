<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IngresoxVacacionesxFecha.aspx.cs" Inherits="NominaRRHH.Presentacion.IngresoxVacacionesxFecha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <link href="../Content/fileinput.css" rel="stylesheet" />
    <script src="../Scripts/fileinput.js"></script>
    <link href="../Content/bootstrap-clockpicker.css" rel="stylesheet" />
    <link href="../Content/clockpicker.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    <script src="../Scripts/bootstrap-clockpicker.js"></script>
    <script src="../Scripts/jquery-clockpicker.js"></script>
    <script src="../Scripts/clockpicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="row">
                    <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server" visible="false">
                        <button type="button" class="close" data-dismiss="alert">×</button>
                        <asp:Label ID="lblAlert" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server" visible="false">
                        <button type="button" class="close" data-dismiss="alert">×</button>
                        <asp:Label ID="LblSuccess" runat="server" Visible="false"></asp:Label>
                    </div>

                    <div class="col-md-12">
                        <div class="alert alert-dismissible alert-warning" id="alertValida1" runat="server" visible="false">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <asp:Label ID="lblAlert1" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="alert alert-dismissible alert-success" id="alertSucces1" runat="server" visible="false">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <asp:Label ID="LblSuccess1" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Ubicacion</label>
                                <asp:DropDownList class="form-control" ID="ddlUbicacionVac" runat="server">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2" runat="server">
                                <label class="control-label" for="focusedInput">Fecha Pago</label>
                                <div class="input-group input-append date" id="datePicker3">
                                    <asp:TextBox ID="txtFecAut" class="form-control datepicker" ReadOnly="true"
                                        runat="server"></asp:TextBox>
                                    <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div id="empleado" runat="server">
                                <div class="col-md-2">
                                    <label class="control-label" for="focusedInput">Codigo Empleado</label>
                                    <asp:TextBox ID="txtcodigoEmpleadoVacaciones" class="form-control" runat="server" autocomplete="off" onkeypress="return soloNumeros(event)" OnTextChanged="txtcodigoEmpleadoVacaciones_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label class="control-label" for="focusedInput">Nombre</label>
                                    <asp:TextBox ID="TxtNombreEmpVac" class="form-control" runat="server" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="control-label" for="focusedInput">Departamento</label>
                                    <asp:TextBox ID="TxtDeptoVac" class="form-control" runat="server" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="control-label" for="focusedInput">Dias Vacaciones</label>
                                    <asp:TextBox ID="TxtSaldoVac" class="form-control" runat="server" autocomplete="off" onkeypress="return soloNumeros(event)" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2" style="margin-top: 26px;">
                                <asp:Button ID="btnProcVacaciones" Class="btn btn-success" runat="server" Text="Procesar" OnClick="btnProcVacaciones_Click" />
                            </div>

                        </div>
                    </div>

                  
                </div>


            </div>
        </div>

    </div>
    <script type="text/javascript">
        $(function () {
            $('#datePicker3,#datePicker4,#datePicker1').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
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

