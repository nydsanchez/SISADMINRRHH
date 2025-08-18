<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="periodos.aspx.cs" Inherits="NominaRRHH.Presentacion.periodos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                <div class="form-horizontal" id="divoptions" runat="server">
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 45px; margin-left: 220px;">
                            <label class="control-label col-md-2" for="focusedInput">Tipo de Periodos</label>
                            <div class="col-md-6">
                                <asp:RadioButtonList ID="RbTipoPeriodos" runat="server" RepeatDirection="Vertical">
                                    <asp:ListItem Selected="True" Value="1">Planilla</asp:ListItem>
                                    <asp:ListItem Value="2">Viaticos</asp:ListItem>
                                    <asp:ListItem Value="3">Vacaciones 1er Semestre</asp:ListItem>
                                    <asp:ListItem Value="4">Vacaciones 2do Semestre</asp:ListItem>
                                    <asp:ListItem Value="5">Aguinaldo</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnIniciar" Class="btn btn-info" Style="margin-left: 700px;" runat="server" Text="Iniciar" OnClick="btnIniciar_Click" />
                        </div>
                    </div>
                </div>
                <asp:MultiView ID="mvPeriodos" runat="server">
                    <asp:View ID="VwPeriodoOrdinario" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label class="control-label" for="focusedInput">Tipos De Planilla</label>
                                    <asp:DropDownList class="form-control" ID="ddlTiposPlanilla" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTiposPlanilla_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label class="control-label" for="focusedInput">Ubicacion</label>
                                    <asp:DropDownList class="form-control" ID="ddlUbicacion" runat="server">
                                    </asp:DropDownList>
                                </div>
                              <div class="col-md-1">
                                    <label class="control-label" for="focusedInput">Periodo</label>                                  
                                        <asp:TextBox ID="txtNo" class="form-control" ReadOnly="true"
                                            runat="server" autocomplete="off"></asp:TextBox>                                  
                                </div>
                                <div class="col-md-2">
                                    <label class="control-label" for="focusedInput">Factor Cierre</label>                                  
                                        <asp:TextBox ID="TxtFactor" class="form-control" onkeypress="return soloNumeros(event)"
                                            runat="server" autocomplete="off"></asp:TextBox>                                  
                                </div>
                                <div runat="server" id="divconsolidar" visible="false">
                                    <div class="col-md-2">
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="ChkConsolidar" runat="server" AutoPostBack="True" OnCheckedChanged="ChkConsolidar_CheckedChanged" Checked="false" />
                                                <strong>Consolidar</strong>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="col-md-2">
                                    <asp:Button ID="btnTipoPlanilla" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Aceptar" OnClick="btnTipoPlanilla_Click" />
                                </div>--%>
                            </div>
                        </div>
                    <%--</asp:View>
                    <asp:View ID="VWPeriodoCatorcenal" runat="server">--%>
            <div class="row" id="periodSemana1" runat="server" visible="false">
                
                <div class="col-md-12">
                    <h3>Periodo 1</h3>
                    <div class="col-md-2">
                        <label class="control-label" for="focusedInput">Mes</label>
                        <asp:DropDownList class="form-control" ID="ddlMesSem1" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label class="control-label" for="focusedInput">Desde</label>
                        <div class="input-group input-append date" id="datePicker">
                            <asp:TextBox ID="txtDesde1er" class="form-control datepicker"
                                runat="server"></asp:TextBox>
                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                    <div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Hasta</label>
                            <div class="input-group input-append date" id="datePicker1">
                                <asp:TextBox ID="txtHasta1er" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="row" id="periodSemana2" runat="server" visible="false">                                
                    <div class="col-md-12">
                         <h3>Periodo 2</h3>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Mes</label>
                            <asp:DropDownList class="form-control" ID="ddlMesSem2" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Desde</label>
                            <div class="input-group input-append date" id="datePicker2">
                                <asp:TextBox ID="txtDesde2da" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Hasta</label>
                            <div class="input-group input-append date" id="datePicker3">
                                <asp:TextBox ID="txtHasta2da" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                    </div>
                    
              
            </div>
                        <br />
                    <div class="col-md-2">
                        <asp:Button ID="btnAagregar" Class="btn btn-success" runat="server" Text="Guardar" OnClick="btnAagregar_Click" />
                    </div>
            </asp:View>
                   <%-- <asp:View ID="VWPeriodoQuincenal" runat="server">
                        <div class="row" id="periodo">
                         
                            <div class="col-md-12" style="margin-top: 45px; margin-left: 200px;">
                                <div class="col-md-2">
                                    <label class="control-label" for="focusedInput">Mes</label>
                                    <asp:DropDownList class="form-control" ID="ddlMesPerid" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label class="control-label" for="focusedInput">Desde</label>
                                    <div class="input-group input-append date" id="datePicker4">
                                        <asp:TextBox ID="txtFechaDesdPeriQ" class="form-control datepicker"
                                            runat="server"></asp:TextBox>
                                        <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <label class="control-label" for="focusedInput">Hasta</label>
                                    <div class="input-group input-append date" id="datePicker5">
                                        <asp:TextBox ID="txtFechaHastPeriQ" class="form-control datepicker"
                                            runat="server"></asp:TextBox>
                                        <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnGuardarPQ" Class="btn btn-success" Style="margin-top: 27px;" runat="server" Text="Guardar" OnClick="btnGuardarPQ_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:View>--%>
            <asp:View ID="vwPeriodoVacaciones" runat="server">
                <div class="row">
                    <div class="col-md-12" style="margin-left: 100px;">
                      
                        <div class="col-md-2">
                            <label class="control-label col-md-1" for="focusedInput">Ubicacion</label>
                            <asp:DropDownList class="form-control" ID="ddlUbicacionVacaciones" runat="server">
                            </asp:DropDownList>
                        </div>
                          <div class="col-md-1">
                            <label class="control-label" for="focusedInput">Periodo</label>
                            <asp:TextBox ID="txtNumeroPeriodoVacaciones" class="form-control" ReadOnly="true"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                                    <label class="control-label" for="focusedInput">Factor Cierre</label>                                  
                                        <asp:TextBox ID="TxtFactorVac" class="form-control" onkeypress="return soloNumeros(event)"
                                            runat="server" autocomplete="off"></asp:TextBox>                                  
                                </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Mes</label>
                            <asp:DropDownList class="form-control" ID="ddlMesVac" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Desde</label>
                            <div class="input-group input-append date" id="datePicker6">
                                <asp:TextBox ID="txtDesdeVac" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Hasta</label>
                            <div class="input-group input-append date" id="datePicker7">
                                <asp:TextBox ID="txtHastaVac" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnVacaciones" Class="btn btn-success" Style="margin-top: 27px;" runat="server" Text="Guardar" OnClick="btnVacaciones_Click" />
                        </div>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="vwPeriodoAguinaldo" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Ubicacion</label>
                            <asp:DropDownList class="form-control" ID="ddlUbicacionAguinaldo" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <label class="control-label" for="focusedInput">Periodo</label>
                            <asp:TextBox ID="txtPeriodoAguinaldo" class="form-control" ReadOnly="true"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                         <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Factor Cierre</label>
                            <asp:TextBox ID="TxtFactorAg" class="form-control"  onkeypress="return soloNumeros(event)"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Desde</label>
                            <div class="input-group input-append date" id="datePicker8">
                                <asp:TextBox ID="TxtDesdeAgui" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Hasta</label>
                            <div class="input-group input-append date" id="datePicker9">
                                <asp:TextBox ID="TxtHastaAgui" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnAguinaldo" Class="btn btn-success" Style="margin-top: 27px;" runat="server" Text="Guardar" OnClick="btnAguinaldo_Click" />
                        </div>
                    </div>
                </div>
            </asp:View>
            </asp:MultiView>
        </div>
    </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#datePicker,#datePicker1,#datePicker2,#datePicker3,#datePicker4').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });

            $('#datePicker5,#datePicker6,#datePicker7,#datePicker8').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });

            $('#datePicker9').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });
        $("#MainContent_ChkPerSem").click(function () {
            if ($(this).is(':checked')) {
                $("#periodSemana1").fadeIn();
                $("#periodSemana2").fadeIn();
                $("#periodo").fadeOut();
            }
            else {
                $("#periodSemana1").fadeOut();
                $("#periodSemana2").fadeOut();
            }
        });

        $("#MainContent_ChkPeri").click(function () {
            if ($(this).is(':checked')) {
                $("#periodo").fadeIn();
                $("#periodSemana1").fadeOut();
                $("#periodSemana2").fadeOut();
            }
            else {
                $("#periodo").fadeOut();
            }
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
