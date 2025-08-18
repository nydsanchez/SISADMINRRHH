<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleMarcas.aspx.cs" Inherits="NominaRRHH.Presentacion.DetalleMarcas" %>

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
                <div class="row">
                    <div class="col-md-12" style="margin-left: 100px;">
                        <div class="col-md-4">
                            <label class="control-label" for="focusedInput">Tipos De Planilla</label>
                            <asp:DropDownList class="form-control" ID="ddlTiposPlanilla" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTiposPlanilla_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <label class="control-label" for="focusedInput">Periodo</label>
                            <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-1" id="divSemana" runat="server" visible ="false">
                            <label class="control-label" for="focusedInput">Semana</label>
                            <asp:DropDownList class="form-control" Style="width: 67px;" ID="ddlSemana" runat="server">
                                <asp:ListItem Value="1"> 1</asp:ListItem>
                                <asp:ListItem Value="2"> 2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Codigo Empleado</label>
                            <asp:TextBox ID="txtCodEmp" autofocus="true" class="form-control" placeholder="Digite Codigo"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnBuscar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row" visible="false" id="editMarcas" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Codigo</label>
                            <asp:TextBox ID="txtCodigo" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
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
                            <label class="control-label" for="focusedInput">Hora Entrada</label>
                            <asp:TextBox ID="txtHoraE" class="form-control"
                                runat="server" onkeypress="return soloNumerosHora(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Hora Salida</label>
                            <asp:TextBox ID="txtHoraS" class="form-control"
                                runat="server" onkeypress="return soloNumerosHora(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnEditar" Visible="false" Class="btn btn-info" runat="server" Text="Editar" Style="margin-top: 25px;" OnClick="btnEditar_Click" />
                            <asp:Button ID="btnCrear" Visible="false" Class="btn btn-info" runat="server" Text="Crear" Style="margin-top: 25px;" OnClick="btnCrear_Click" />
                        </div>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="GVDetNomEmpl" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="GVDetNomEmpl_SelectedIndexChanged">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                    <ControlStyle Width="5px" />
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:CommandField>
                                <asp:BoundField DataField="nombrecompleto" HeaderText="Nombre">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Codigo" HeaderText="Codigo">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horae" HeaderText="Hora Entrada">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horas" HeaderText="Hora Salida">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="HT" HeaderText="Horas Trabajadas">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
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

