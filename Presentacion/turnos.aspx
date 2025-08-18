<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="turnos.aspx.cs" Inherits="NominaRRHH.Presentacion.turnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/Styles.css" rel="stylesheet" />
    <link href="../Content/bootstrap-switch.css" rel="stylesheet" />
    <link href="../Content/bootstrap-clockpicker.css" rel="stylesheet" />
    <link href="../Content/clockpicker.css" rel="stylesheet" />
    <link href="../Content/standalone.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-switch.js"></script>
    <script src="../Scripts/bootstrap-clockpicker.js"></script>
    <script src="../Scripts/jquery-clockpicker.js"></script>
    <script src="../Scripts/clockpicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top">
        <div class="panel panel-info" style="width:1290px; margin-left: -40px;">
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
                    <div class="col-md-12" style="margin-left: 150px;">
                        <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Nombre Turno</label>
                            <asp:TextBox ID="txtNombTurno" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Minutos Comodin</label>
                            <asp:TextBox ID="txtMinComodin" class="form-control"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Total Horas Semana</label>
                            <asp:TextBox ID="txtTotalHoras" class="form-control"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnAgregar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                            <asp:Button class="btn btn-info btnagregarPermiso" ID="BtnEditar" runat="server" Text="Editar" Visible ="false" OnClick="BtnEditar_Click"/>
                            <asp:Button class="btn btn-danger btnagregarPermiso" ID="btnEliminar" runat="server" Text="Eliminar" Visible ="false" OnClick="btnEliminar_Click"/>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top: 70px;">
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <%--<label class="control-label" for="focusedInput" style="margin-left: 10px">Lunes</label>--%>
                            <asp:CheckBox ID="ChkLunes" Class="lunes" name="my-checkbox" runat="server" />
                            <label class="control-label" for="focusedInput" style="margin-left: 10px">Lunes</label>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div1">
                            <label class="control-label" for="focusedInput">Hora Entrada</label>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <asp:TextBox ID="txtHoraEntradaLunes" class="form-control"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon borderCalendar">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div2">
                            <label class="control-label" for="focusedInput">Hora Salida</label>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <asp:TextBox ID="txtHoraSalidaLunes" class="form-control"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon borderCalendar">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div3">
                            <label class="control-label" for="focusedInput">Horas Comida</label>
                            <asp:TextBox ID="txtHorComidaLunes" class="form-control"
                                onkeypress="return soloNumeros(event)" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12" style="margin-top: 30px;">
                        <div class="col-md-3">
                            <%-- <label style="margin-left: 10px">Martes</label>--%>
                            <asp:CheckBox ID="ChkMartes" Class="martes" name="my-checkbox" runat="server" />
                            <label style="margin-left: 10px">Martes</label>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div4">
                            <label class="control-label" for="focusedInput">Hora Entrada</label>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <asp:TextBox ID="txtHoraEntradaMartes" class="form-control"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon borderCalendar">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div5">
                            <label class="control-label" for="focusedInput">Hora Salida</label>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <asp:TextBox ID="txtHoraSalidaMartes" class="form-control"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon borderCalendar">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div6">
                            <label class="control-label" for="focusedInput">Horas Comida</label>
                            <asp:TextBox ID="txtHoraComidaMartes" class="form-control"
                                onkeypress="return soloNumeros(event)" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12" style="margin-top: 30px;">
                        <div class="col-md-3">
                            <asp:CheckBox ID="ChkMiercoles" Class="miercoles" name="my-checkbox" runat="server" />
                            <label style="margin-left: 10px">Miercoles</label>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div7">
                            <label class="control-label" for="focusedInput">Hora Entrada</label>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <asp:TextBox ID="txtHoraEntradaMiercoles" class="form-control"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon borderCalendar">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div8">
                            <label class="control-label" for="focusedInput">Hora Salida</label>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <asp:TextBox ID="txtHoraSalidaMiercoles" class="form-control"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon borderCalendar">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div9">
                            <label class="control-label" for="focusedInput">Horas Comida</label>
                            <asp:TextBox ID="txtHoraComidaMiercoles" class="form-control"
                                onkeypress="return soloNumeros(event)" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12" style="margin-top: 30px;">
                        <div class="col-md-3">
                            <asp:CheckBox ID="ChkJueves" Class="jueves" name="my-checkbox" runat="server" />
                            <label style="margin-left: 10px">Jueves</label>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div10">
                            <label class="control-label" for="focusedInput">Hora Entrada</label>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <asp:TextBox ID="txtHoraEntradaJueves" class="form-control"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon borderCalendar">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div11">
                            <label class="control-label" for="focusedInput">Hora Salida</label>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <asp:TextBox ID="txtHoraSalidaJueves" class="form-control"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon borderCalendar">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div12">
                            <label class="control-label" for="focusedInput">Horas Comida</label>
                            <asp:TextBox ID="txtHoraComidaJueves" class="form-control"
                                onkeypress="return soloNumeros(event)" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12" style="margin-top: 30px;">
                        <div class="col-md-3">
                            <asp:CheckBox ID="ChkViernes" Class="viernes" name="my-checkbox" runat="server" />
                            <label style="margin-left: 10px">Viernes</label>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div13">
                            <label class="control-label" for="focusedInput">Hora Entrada</label>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <asp:TextBox ID="txtHoraEntradaViernes" class="form-control"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon borderCalendar">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div14">
                            <label class="control-label" for="focusedInput">Hora Salida</label>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <asp:TextBox ID="txtHoraSalidaViernes" class="form-control"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon borderCalendar">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div15">
                            <label class="control-label" for="focusedInput">Horas Comida</label>
                            <asp:TextBox ID="txtHoraComidaViernes" class="form-control"
                                onkeypress="return soloNumeros(event)" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12" style="margin-top: 30px;">
                        <div class="col-md-3">
                            <asp:CheckBox ID="ChkSabado" Class="sabado" name="my-checkbox" runat="server" />
                            <label style="margin-left: 10px">Sabado</label>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div16">
                            <label class="control-label" for="focusedInput">Hora Entrada</label>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <asp:TextBox ID="txtHoraEntradaSabado" class="form-control"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon borderCalendar">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div17">
                            <label class="control-label" for="focusedInput">Hora Salida</label>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <asp:TextBox ID="txtHoraSalidaSabado" class="form-control"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon borderCalendar">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div18">
                            <label class="control-label" for="focusedInput">Horas Comida</label>
                            <asp:TextBox ID="txtHoraComidaSabado" class="form-control"
                                onkeypress="return soloNumeros(event)" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-12" style="margin-top: 30px;">
                        <div class="col-md-3">
                            <asp:CheckBox ID="ChkDomingo" Class="domingo" name="my-checkbox" runat="server" />
                            <label style="margin-left: 10px">Domingo</label>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div19">
                            <label class="control-label" for="focusedInput">Hora Entrada</label>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <asp:TextBox ID="txtHoraEntradaDomingo" class="form-control"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon borderCalendar">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div20">
                            <label class="control-label" for="focusedInput">Hora Salida</label>
                            <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                <asp:TextBox ID="txtHoraSalidaDomingo" class="form-control"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon borderCalendar">
                                    <span class="glyphicon glyphicon-time"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: -25px;" id="div21">
                            <label class="control-label" for="focusedInput">Horas Comida</label>
                            <asp:TextBox ID="txtHoraComidaDomingo" class="form-control"
                                onkeypress="return soloNumeros(event)" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="GVturnos" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="GVturnos_SelectedIndexChanged">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                    <ControlStyle Width="20px" />
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle Width="20px" />
                                </asp:CommandField>
                                <asp:BoundField DataField="nombre_turno" HeaderText="Turno">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="lunes_desde" HeaderText="Entrada Lunes">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="lunes_hasta" HeaderText="Salida Lunes">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="martes_desde" HeaderText="Entrada Martes">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="martes_hasta" HeaderText="Salida Martes">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="miercoles_desde" HeaderText="Entrada Miercoles">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="miercoles_hasta" HeaderText="Salida Miercoles">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="jueves_desde" HeaderText="Entrada Jueves">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="jueves_hasta" HeaderText="Salida Jueves">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="viernes_desde" HeaderText="Entrada Viernes">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="viernes_hasta" HeaderText="Salida Viernes">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sabado_desde" HeaderText="Entrada Sabado">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sabado_hasta" HeaderText="Salida Sabado">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="domingo_desde" HeaderText="Entrada Domingo">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="domingo_hasta" HeaderText="Salida Domingo">
                                    <ItemStyle Width="8px" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $("[name='ctl00$MainContent$ChkLunes']").bootstrapSwitch();
        $("[name='ctl00$MainContent$ChkMartes']").bootstrapSwitch();
        $("[name='ctl00$MainContent$ChkMiercoles']").bootstrapSwitch();
        $("[name='ctl00$MainContent$ChkJueves']").bootstrapSwitch();
        $("[name='ctl00$MainContent$ChkViernes']").bootstrapSwitch();
        $("[name='ctl00$MainContent$ChkSabado']").bootstrapSwitch();
        $("[name='ctl00$MainContent$ChkDomingo']").bootstrapSwitch();

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

        $(".lunes .bootstrap-switch .bootstrap-switch-handle-off").click(function () {
            $("#div1").fadeIn();
            $("#div2").fadeIn();
            $("#div3").fadeIn();

        });

        $(".martes .bootstrap-switch .bootstrap-switch-handle-off").click(function () {
            $("#div4").fadeIn();
            $("#div5").fadeIn();
            $("#div6").fadeIn();


        });

        $(".miercoles .bootstrap-switch .bootstrap-switch-handle-off").click(function () {
            $("#div7").fadeIn();
            $("#div8").fadeIn();
            $("#div9").fadeIn();
        });

        $(".jueves .bootstrap-switch .bootstrap-switch-handle-off").click(function () {
            $("#div10").fadeIn();
            $("#div11").fadeIn();
            $("#div12").fadeIn();
        });

        $(".viernes .bootstrap-switch .bootstrap-switch-handle-off").click(function () {
            $("#div13").fadeIn();
            $("#div14").fadeIn();
            $("#div15").fadeIn();
        });

        $(".sabado .bootstrap-switch .bootstrap-switch-handle-off").click(function () {
            $("#div16").fadeIn();
            $("#div17").fadeIn();
            $("#div18").fadeIn();
        });

        $(".domingo .bootstrap-switch .bootstrap-switch-handle-off").click(function () {
            $("#div19").fadeIn();
            $("#div20").fadeIn();
            $("#div21").fadeIn();
        });

        $(".lunes .bootstrap-switch .bootstrap-switch-handle-on").click(function () {
            $("#div1").fadeOut();
            $("#div2").fadeOut();
            $("#div3").fadeOut();
        });

        $(".martes .bootstrap-switch .bootstrap-switch-handle-on").click(function () {
            $("#div4").fadeOut();
            $("#div5").fadeOut();
            $("#div6").fadeOut();
        });

        $(".miercoles .bootstrap-switch .bootstrap-switch-handle-on").click(function () {
            $("#div7").fadeOut();
            $("#div8").fadeOut();
            $("#div9").fadeOut();
        });

        $(".jueves .bootstrap-switch .bootstrap-switch-handle-on").click(function () {
            $("#div10").fadeOut();
            $("#div11").fadeOut();
            $("#div12").fadeOut();
        });

        $(".viernes .bootstrap-switch .bootstrap-switch-handle-on").click(function () {
            $("#div13").fadeOut();
            $("#div14").fadeOut();
            $("#div15").fadeOut();
        });

        $(".sabado .bootstrap-switch .bootstrap-switch-handle-on").click(function () {
            $("#div16").fadeOut();
            $("#div17").fadeOut();
            $("#div18").fadeOut();
        });

        $(".domingo .bootstrap-switch .bootstrap-switch-handle-on").click(function () {
            $("#div19").fadeOut();
            $("#div20").fadeOut();
            $("#div21").fadeOut();
        });

    </script>
</asp:Content>


