<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcesoPlanilla.aspx.cs" Inherits="NominaRRHH.Presentacion.ProcesoPlanilla" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/Styles.css" rel="stylesheet" />
    <link href="../Content/fileinput.css" rel="stylesheet" />
    <script src="../Scripts/fileinput.js"></script>
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
                <asp:MultiView ID="mvPlanillas" runat="server">
                    <asp:View ID="wvUsuario" runat="server">
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 45px; margin-left: 220px;">
                                <label class="control-label col-md-2" for="focusedInput">Tipo de Planillas</label>
                                <div class="col-md-6">
                                    <asp:RadioButtonList ID="RbTipoPeriodos" runat="server" RepeatDirection="Vertical" OnSelectedIndexChanged="RbTipoPeriodos_SelectedIndexChanged">
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
                    </asp:View>
                    <asp:View ID="vwPlanillaOrdinaria" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Ubicacion</label>
                                            <asp:DropDownList class="form-control" ID="ddlUbicacion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUbicacion_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Tipos De Planilla</label>
                                            <asp:DropDownList class="form-control" ID="ddlTiposPlanilla" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTiposPlanilla_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Periodo</label>
                                            <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                        </div>
                                      <%--  <div id="divsemana" runat="server" visible="false">
                                            <div class="col-md-2">
                                                <label class="control-label" for="focusedInput">Semana</label>
                                                <asp:DropDownList class="form-control" ID="ddlSemana" runat="server">
                                                    <asp:ListItem Value="1"> 1</asp:ListItem>
                                                    <asp:ListItem Value="2"> 2</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>
                                        <div class="row" id="procPlan" runat="server" visible="false">                                           
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnProcesarPlanilla" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Procesar Planilla" OnClick="btnProcesarPlanilla_Click" />
                                                </div>                                               
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnCerrarPeriodo" Class="btn btn-warning" Style="margin-top: 22px;" runat="server" Text="Cerrar Periodo" OnClick="btnCerrarPeriodo_Click" />
                                                </div>                                                                                       
                                        </div>
                                        </div>
                                    </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </asp:View>

                    <asp:View ID="vwPlanillaVacaciones" runat="server">
                        <div class="row">
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
                                    <div class="col-md-2" id="divChkDptoVac">
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="ChkDptoVac" runat="server" AutoPostBack="True" OnCheckedChanged="ChkDptoVac_CheckedChanged" />
                                                <strong>¿Procesar Por Empleado?</strong>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="ChkExcel" runat="server" AutoPostBack="True" OnCheckedChanged="ChkExcel_CheckedChanged" Checked="false" />
                                                <strong>¿Cargar excel con empleados?</strong>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Ubicacion</label>
                                        <asp:DropDownList class="form-control" ID="ddlUbicacionVac" runat="server" OnSelectedIndexChanged="ddlUbicacionVac_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label" for="focusedInput">Periodo</label>
                                        <asp:TextBox ID="txtPeriodoVacaciones" class="form-control" runat="server" autocomplete="off" ReadOnly="true"></asp:TextBox>
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
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Departamento</label>
                                            <asp:TextBox ID="TxtDeptoVac" class="form-control" runat="server" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Dias Vacaciones</label>
                                            <asp:TextBox ID="TxtSaldoVac" class="form-control" runat="server" autocomplete="off" onkeypress="return soloNumeros(event)" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="divexcel" runat="server" visible="false">
                                    <div class="col-md-12" style="margin-left: 50px; margin-top: 15px;">
                                        <div class="col-md-6" style="margin-left: 110px; margin-top: 15px;">
                                            <label class="control-label" for="focusedInput">Cargar Archivo de Empleados</label>
                                            <asp:FileUpload ID="FileVac" class="file" runat="server" />
                                        </div>
                                        <div class="col-md-2" style="margin-left: 15px; margin-top: 18px;">
                                            <asp:Button ID="BtnExcelEmp" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Cargar" OnClick="BtnExcelEmp_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2" style="margin-top: 26px;">
                                        <asp:Button ID="btnProcVacaciones" Class="btn btn-success" runat="server" Text="Procesar" OnClick="btnProcVacaciones_Click" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnRVac" Class="btn btn-info" Style="margin-top: 24px; margin-left: 10px;" runat="server" Text="Recalcular" OnClick="btnRVac_Click" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnCVac" Class="btn btn-warning" Style="margin-top: 22px;" runat="server" Text="Cerrar Periodo" OnClick="btnCVac_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vwPlanillaAguinaldo" runat="server">
                        <div class="row">
                            <div class="col-md-12" style="margin-left: 150px;">
                                <div class="col-md-2">
                                    <label class="control-label" for="focusedInput">Ubicacion</label>
                                    <asp:DropDownList class="form-control" ID="ddlUbicacionAguinaldo" runat="server" OnSelectedIndexChanged="ddlUbicacionAguinaldo_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <label class="control-label" for="focusedInput">Periodo</label>
                                    <asp:TextBox ID="txtPeriodoAguinaldo" class="form-control" runat="server" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-md-2" style="margin-top: 26px;">
                                    <asp:Button ID="btnProcAguinaldo" Class="btn btn-success" runat="server" Text="Procesar" OnClick="btnProcAguinaldo_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnRAgui" Class="btn btn-info" Style="margin-top: 24px; margin-left: 10px;" runat="server" Text="Recalcular" OnClick="btnRAgui_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnCAgui" Class="btn btn-warning" Style="margin-top: 22px;" runat="server" Text="Cerrar Periodo" OnClick="btnCAgui_Click" />
                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <h1>
                    <p style="margin-left: -118px; margin-top: 300px;">Procesando...</p>
                </h1>
                <div class="whirly-loader" style="margin-top: 108px;">
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script type="text/javascript">
        $("#MainContent_ChkPerSem").click(function () {
            if ($(this).is(':checked')) {
                $("#procPlanSemana").fadeIn();
                $("#procPlanPeriodo").fadeOut();
                $("#MainContent_ChkQuincenal").prop("checked", false);
            }
            else {
                $("#procPlanSemana").fadeOut();
            }
        });

        $("#MainContent_btnTipoPlanilla").click(function () {
            $("#MainContent_divCargArch").fadeIn();
        });
        $(function () {
            $('#datePicker').datepicker({
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
