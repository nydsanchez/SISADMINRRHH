<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Empleados.aspx.cs" Inherits="NominaRRHH.Presentacion.Empleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
    <link href="../Content/bootstrap-clockpicker.css" rel="stylesheet" />
    <link href="../Content/clockpicker.css" rel="stylesheet" />
    <link href="../Content/standalone.css" rel="stylesheet" />
    <link href="../Content/Styles.css" rel="stylesheet" />
    <link href="../Content/fileinput.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    <script src="../Scripts/bootstrap-clockpicker.js"></script>
    <script src="../Scripts/jquery-clockpicker.js"></script>
    <script src="../Scripts/clockpicker.js"></script>
    <script src="../Scripts/fileinput.js"></script>
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </cc1:ToolkitScriptManager>
    <div class="container mar-top">
        <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server" visible="false">
            <button type="button" class="close" data-dismiss="alert">×</button>
            <asp:Label ID="lblAlert" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server" visible="false">
            <button type="button" class="close" data-dismiss="alert">×</button>
            <asp:Label ID="LblSuccess" runat="server" Visible="false"></asp:Label>
        </div>
        <fieldset class="marginFields">
            <legend>Catalogo Empleados</legend>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label class="control-label" for="focusedInput">Codigo Empleado</label>
                        <asp:TextBox ID="txtCodEmp" autofocus="true" class="form-control" placeholder="Digite Codigo"
                            runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label" for="focusedInput">Nombre</label>
                        <asp:TextBox ID="txtBuscarNombre" class="form-control"
                            runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="col-md-1" style="margin-left: 50px;">
                        <asp:ImageButton ID="btnBuscar" Class="btnSearch" ImageUrl="~/Images/lupa3.png" runat="server" OnClick="btnBuscar_Click" />
                    </div>
                    <div class="col-md-3" style="margin-left: 90px;">
                        <div class="form-group">
                            <div id="Imagen" runat="server">
                                <asp:Image ID="Image1" class="sizeFoto" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#Infor" data-toggle="tab" aria-expanded="true">Informacion</a></li>
                        <li class=""><a href="#DiasLab" data-toggle="tab" aria-expanded="false">Datos Laborales</a></li>
                        <li class=""><a href="#DatosPers" data-toggle="tab" aria-expanded="false">Datos Personales</a></li>
                        <li class=""><a href="#Fam" data-toggle="tab" aria-expanded="false">Familia</a></li>
                        <li class=""><a href="#HistoLab" data-toggle="tab" aria-expanded="false">Historial Laboral</a></li>
                        <li class=""><a href="#Refer" data-toggle="tab" aria-expanded="false">Referencias</a></li>
                        <li class=""><a href="#SaludEstud" data-toggle="tab" aria-expanded="false">Salud y Estudios</a></li>
                        <li class=""><a href="#HistoEgres" data-toggle="tab" aria-expanded="false">Historial Egresos</a></li>
                        <li class=""><a href="#HistoSalario" data-toggle="tab" aria-expanded="false">Historial Salarios</a></li>
                    </ul>
                    <div id="myTabContent" class="tab-content paddingTab">
                        <div class="tab-pane fade active in" id="Infor">
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Primer Nombre *</label>
                                        <asp:TextBox ID="txt1erNombre" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Segundo Nombre</label>
                                        <asp:TextBox ID="txt2doNombre" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Primer Apellido *</label>
                                        <asp:TextBox ID="txt1erApellido" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Segundo Apellido</label>
                                        <asp:TextBox ID="txt2doApellido" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>

                                    <div class="col-md-1">
                                        <label class="control-label" for="focusedInput">Sexo</label>
                                        <asp:DropDownList class="form-control" Style="width: 71px;" ID="ddlSexo" runat="server">
                                            <asp:ListItem Value="M"> </asp:ListItem>
                                            <asp:ListItem Value="F"> </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Fecha Nacimiento *</label>
                                        <div class="input-group input-append date" id="datePicker">
                                            <asp:TextBox ID="txtFechaNac" class="form-control datepicker" onkeypress="return soloNumeros(event)" autocomplete="off"
                                                runat="server" ></asp:TextBox>
                                              <cc1:MaskedEditExtender ID="msk_txtDate" runat="server" ClearMaskOnLostFocus="False"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtFechaNac">
                                                </cc1:MaskedEditExtender>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Num Cedula *</label>
                                        <asp:TextBox ID="txtCedula" class="form-control" ClientIDMode="Static"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                        <%-- <cc1:MaskedEditExtender TargetControlID="txtCedula" Mask="999-999999-9999L" OnInvalidCssClass="MaskedEditError"
                                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
                                            MaskType="Number" InputDirection="LeftToRight" AcceptNegative="None" DisplayMoney="None"
                                            ErrorTooltipEnabled="True" runat="server" ID="mskD" />--%>                                        <%-- ClearMaskOnLostFocus="False" --%>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Emitida</label>
                                        <div class="input-group input-append date" id="datePicker2">
                                            <asp:TextBox ID="txtEmite" class="form-control datepicker" onkeypress="return soloNumeros(event)" autocomplete="off"
                                                runat="server"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" ClearMaskOnLostFocus="False"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtEmite" MaskType="Date">
                                                </cc1:MaskedEditExtender>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Vence</label>
                                        <div class="input-group input-append date" id="datePicker3">
                                            <asp:TextBox ID="txtVence" class="form-control datepicker" onkeypress="return soloNumeros(event)" autocomplete="off"
                                                runat="server" ></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" ClearMaskOnLostFocus="False"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtVence" MaskType="Date">
                                                </cc1:MaskedEditExtender>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">No Inss</label>
                                        <asp:TextBox ID="txtNumInss" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Pais</label>
                                        <asp:DropDownList class="form-control" ID="ddlPais" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Departamento</label>
                                        <asp:DropDownList class="form-control" ID="ddlDepartamento" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Municipio</label>
                                        <asp:DropDownList class="form-control" ID="ddlMunicipio" runat="server">
                                        </asp:DropDownList>
                                    </div>


                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Ubicacion</label>
                                        <asp:DropDownList class="form-control" ID="ddlUbicacion" runat="server">
                                        </asp:DropDownList>
                                    </div>


                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Proceso</label>
                                        <asp:DropDownList class="form-control" ID="ddlProceso" runat="server" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Cargo</label>
                                        <asp:DropDownList class="form-control" ID="ddlCargo" runat="server" OnSelectedIndexChanged="ddlCargo_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Operacion</label>
                                        <asp:DropDownList class="form-control" ID="ddlOperacion" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Nivel Academico</label>
                                        <asp:DropDownList class="form-control" ID="ddlNivelAcademico" runat="server">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Estado</label>
                                        <asp:DropDownList class="form-control" ID="ddlEstadoEmpl" runat="server" Visible="false">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="TxtEstado" autocomplete="off" ReadOnly="true" class="form-control"
                                                runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Primer Ingreso *</label>
                                        <div class="input-group input-append date" id="datePicker1">
                                            <asp:TextBox ID="txt1erIngreso" class="form-control datepicker" onkeypress="return soloNumeros(event)" autocomplete="off"
                                                runat="server"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" ClearMaskOnLostFocus="False"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txt1erIngreso" MaskType="Date">
                                                </cc1:MaskedEditExtender>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Fecha Ingreso *</label>
                                        <div class="input-group input-append date" id="datePicker4">
                                            <asp:TextBox ID="txtFechaIngreso" class="form-control datepicker" onkeypress="return soloNumeros(event)" autocomplete="off"
                                                runat="server"></asp:TextBox>
                                             <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" ClearMaskOnLostFocus="False"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtFechaIngreso" MaskType="Date">
                                                </cc1:MaskedEditExtender>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-md-2">                                       
                                        <div class="input-group input-append date" id="chkBoxMFI">
                                            
                                            <asp:CheckBox ID="CheckBoxMFI" runat="server" Text="Modificar Fecha Ingreso" />
                                            
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Fecha Egreso</label>
                                        <div class="input-group input-append date" id="datePicker5">
                                            <asp:TextBox ID="txtFechaEgreso" class="form-control datepicker" onkeypress="return soloNumeros(event)" autocomplete="off"
                                                runat="server"></asp:TextBox>
                                              <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" ClearMaskOnLostFocus="False"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtFechaEgreso" MaskType="Date">
                                                </cc1:MaskedEditExtender>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Jefe Inmediato</label>
                                        <asp:TextBox ID="txtJefeInmed" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Cargar Foto</label>
                                        <asp:FileUpload ID="file" class="file" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <label class="control-label" for="focusedInput">Observacion</label>
                                    <asp:TextBox ID="txtObservEmpl" class="form-control"
                                        runat="server" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="DiasLab">
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Salario Moneda</label>
                                        <asp:DropDownList class="form-control" ID="ddlMoneda" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMoneda_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Salario</label>
                                        <asp:TextBox ID="txtSalario" class="form-control" onkeypress="return soloNumeros(event)" autocomplete="off"
                                            runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Cuenta Contable</label>
                                        <asp:TextBox ID="txtCuentaContable" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Subsidio Diario</label>
                                        <asp:TextBox ID="txtSubsidio" class="form-control"
                                            onkeypress="return soloNumeros(event)" runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Cuenta Bancaria</label>
                                        <asp:TextBox ID="txtCuentaBanc" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Incentivo Fijo</label>
                                        <asp:TextBox ID="txtIncentivo" class="form-control"
                                            onkeypress="return soloNumeros(event)" runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Turno</label>
                                        <asp:DropDownList class="form-control" ID="ddlTurno" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Cuenta Wallmart</label>
                                        <asp:TextBox ID="TxtCuentaMall" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Tipo Contrato</label>
                                        <asp:DropDownList class="form-control" ID="ddlTipoContrato" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Tipo Salario</label>
                                        <asp:DropDownList class="form-control" ID="ddlTipoSalario" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Liquidacion</label>
                                        <asp:DropDownList class="form-control" ID="ddlLiquidado" runat="server">
                                            <asp:ListItem Value="2">NO </asp:ListItem>
                                            <asp:ListItem Value="1">SI </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-2 marginChkActivo">
                                            <div class="checkbox">
                                                <label>
                                                    <asp:CheckBox ID="ChkMarca" runat="server" OnCheckedChanged="ChkMarca_CheckedChanged" AutoPostBack="true" />
                                                    Empleado Marca?
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2 marginChkActivo">
                                            <div class="checkbox">
                                                <label>
                                                    <asp:CheckBox ID="chkflexitime" runat="server" OnCheckedChanged="chkflexitime_CheckedChanged" AutoPostBack="true"/>
                                                    FlexiTime?
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2 marginChkActivo chkExtra">
                                            <div class="checkbox">
                                                <label>
                                                    <asp:CheckBox ID="ChkExtras" runat="server" />
                                                    Gana extras?
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2 marginChkActivo">
                                            <div class="checkbox">
                                                <label>
                                                    <asp:CheckBox ID="chkpagovac" runat="server" />
                                                    Pago Vacaciones?
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2 marginChkActivo">
                                            <div class="checkbox">
                                                <label>
                                                    <asp:CheckBox ID="chkCredito" runat="server" />
                                                    Aplica Credito?
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2 marginChkActivo">
                                            <div class="checkbox">
                                                <label>
                                                    <asp:CheckBox ID="chkMultiTarea" runat="server" />
                                                    Multi-Tarea?
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="DatosPers">
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Telefono</label>
                                        <asp:TextBox ID="txtTelf" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Celular</label>
                                        <asp:TextBox ID="txtCel" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Correo</label>
                                        <asp:TextBox ID="txtCorreo" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Estado Civil</label>
                                        <asp:DropDownList class="form-control" ID="ddlEstadoCivil" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    
                                </div>
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Casa</label>
                                        <asp:DropDownList class="form-control" ID="ddlTipoCasa" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Vive Con</label>
                                        <asp:TextBox ID="txtVive" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <%-- <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Embargo Bancario</label>
                                        <asp:TextBox ID="txtEmbBanc" class="form-control"
                                            onkeypress="return soloNumeros(event)" runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Embargo Por Pension</label>
                                        <asp:TextBox ID="txtEmbPens" class="form-control"
                                            onkeypress="return soloNumeros(event)" runat="server" autocomplete="off"></asp:TextBox>
                                    </div>--%>
                                    
                                </div>
                        
                            </div>
                            <br>
                             <fieldset>
                                <legend>Domicilio</legend>
                                <div class="row">
                                              <div class="col-md-12 paddingContentTabs">
                                      <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Departamento</label>
                                        <asp:DropDownList class="form-control" ID="ddlDomiciDepto" runat="server" OnSelectedIndexChanged="ddlDomiciDepto_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                      <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Municipio</label>
                                        <asp:DropDownList class="form-control" ID="ddlDomiciMunicip" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                      </div>
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-12">
                                        <label class="control-label" for="focusedInput">Direccion *</label>
                                        <asp:TextBox ID="txtDireccion" class="form-control" Rows="10" runat="server" />
                                    </div>
                                </div>
                                    </div>
                                 </fieldset>
                            <fieldset>
                                <legend>En Caso de Emergencia</legend>
                                <div class="row">
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Nombre</label>
                                            <asp:TextBox ID="txtNombEmerg" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Telefono</label>
                                            <asp:TextBox ID="txtTelEmerg" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Parentesco</label>
                                            <asp:TextBox ID="txtParentescEmerg" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-12">
                                            <label class="control-label" for="focusedInput">Direccion</label>
                                            <asp:TextBox ID="txtDireccEmerg" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </fieldset>
                        </div>
                        <div class="tab-pane fade" id="Fam">
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Nombre Padre</label>
                                        <asp:TextBox ID="txtNombPad" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Numero Cedula</label>
                                        <asp:TextBox ID="txtNumCedulPadre" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label" for="focusedInput">Vive</label>
                                        <asp:DropDownList class="form-control" ID="ddlVivPad" runat="server">
                                            <asp:ListItem Value="SI">SI </asp:ListItem>
                                            <asp:ListItem Value="NO">NO </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Nombre Madre</label>
                                        <asp:TextBox ID="txtNombMad" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Numero Cedula</label>
                                        <asp:TextBox ID="txtNumCedulMadre" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label" for="focusedInput">Vive</label>
                                        <asp:DropDownList class="form-control" ID="ddlVivMad" runat="server">
                                            <asp:ListItem Value="SI">SI </asp:ListItem>
                                            <asp:ListItem Value="NO">NO </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Nombre Espos@</label>
                                        <asp:TextBox ID="txtNombEspos" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Numero Cedula</label>
                                        <asp:TextBox ID="txtNumCedulEsps" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <fieldset>
                                <legend>Hijos</legend>
                                <div class="row">
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-5">
                                            <label class="control-label" for="focusedInput">Nombre</label>
                                            <asp:TextBox ID="txtNombH1" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Lugar Nacimiento</label>
                                            <asp:TextBox ID="txtLugNacH1" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Fecha</label>
                                            <div class="input-group input-append date" id="datePicker6">
                                                <asp:TextBox ID="txtFechaNacH1" class="form-control datepicker"
                                                    runat="server"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" ClearMaskOnLostFocus="False"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtFechaNacH1" MaskType="Date">
                                                </cc1:MaskedEditExtender>
                                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Sexo</label>
                                            <asp:DropDownList class="form-control" ID="ddlSexoH1" runat="server">
                                                <asp:ListItem Value="M">M </asp:ListItem>
                                                <asp:ListItem Value="F">F </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-5">
                                            <label class="control-label" for="focusedInput">Nombre</label>
                                            <asp:TextBox ID="txtNombH2" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Lugar Nacimiento</label>
                                            <asp:TextBox ID="txtLugNacH2" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Fecha</label>
                                            <div class="input-group input-append date" id="datePicker7">
                                                <asp:TextBox ID="txtFechaNacH2" class="form-control datepicker"
                                                    runat="server"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" ClearMaskOnLostFocus="False"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtFechaNacH2" MaskType="Date">
                                                </cc1:MaskedEditExtender>
                                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Sexo</label>
                                            <asp:DropDownList class="form-control" ID="ddlSexoH2" runat="server">
                                                <asp:ListItem Value="M">M </asp:ListItem>
                                                <asp:ListItem Value="F">F </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-5">
                                            <label class="control-label" for="focusedInput">Nombre</label>
                                            <asp:TextBox ID="txtNombH3" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Lugar Nacimiento</label>
                                            <asp:TextBox ID="txtLugNacH3" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Fecha</label>
                                            <div class="input-group input-append date" id="datePicker8">
                                                <asp:TextBox ID="txtFechaNacH3" class="form-control datepicker"
                                                    runat="server"></asp:TextBox>
                                                 <cc1:MaskedEditExtender ID="MaskedEditExtender8" runat="server" ClearMaskOnLostFocus="False"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtFechaNacH3" MaskType="Date">
                                                </cc1:MaskedEditExtender>
                                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Sexo</label>
                                            <asp:DropDownList class="form-control" ID="ddlSexoH3" runat="server">
                                                <asp:ListItem Value="M">M </asp:ListItem>
                                                <asp:ListItem Value="F">F </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-5">
                                            <label class="control-label" for="focusedInput">Nombre</label>
                                            <asp:TextBox ID="txtNombH4" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Lugar Nacimiento</label>
                                            <asp:TextBox ID="txtLugNacH4" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Fecha</label>
                                            <div class="input-group input-append date" id="datePicker9">
                                                <asp:TextBox ID="txtFechaNacH4" class="form-control datepicker"
                                                    runat="server"></asp:TextBox>
                                                  <cc1:MaskedEditExtender ID="MaskedEditExtender9" runat="server" ClearMaskOnLostFocus="False"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtFechaNacH4" MaskType="Date">
                                                </cc1:MaskedEditExtender>
                                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Sexo</label>
                                            <asp:DropDownList class="form-control" ID="ddlSexoH4" runat="server">
                                                <asp:ListItem Value="M">M </asp:ListItem>
                                                <asp:ListItem Value="F">F </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </fieldset>
                        </div>
                        <div class="tab-pane fade" id="HistoLab">
                            <fieldset>
                                <legend>Referencia Laboral #1</legend>
                                <div class="row">
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Empresa Anterior</label>
                                            <asp:TextBox ID="txtEmpAnterior" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Verificado</label>
                                            <asp:DropDownList class="form-control" ID="ddlVerfEmAnt1" runat="server">
                                                <asp:ListItem Value="Si">SI </asp:ListItem>
                                                <asp:ListItem Value="No">NO </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Observaciones</label>
                                            <asp:TextBox ID="txtObservEmpAant1" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Ultimo Salario</label>
                                            <asp:TextBox ID="txtUltSalRef1" class="form-control"
                                                onkeypress="return soloNumeros(event)" runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Verificado</label>
                                            <asp:DropDownList class="form-control" ID="ddlVerfUltSalAnt1" runat="server">
                                                <asp:ListItem Value="Si">SI </asp:ListItem>
                                                <asp:ListItem Value="No">NO </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Observaciones</label>
                                            <asp:TextBox ID="txtObservSalAnt1" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Cargo</label>
                                            <asp:TextBox ID="txtCargUltRef1" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Verificado</label>
                                            <asp:DropDownList class="form-control" ID="ddlVerfUltCargRef1" runat="server">
                                                <asp:ListItem Value="1">SI </asp:ListItem>
                                                <asp:ListItem Value="2">NO </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Observaciones</label>
                                            <asp:TextBox ID="txtObservCargAnt1" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Motivo Salida</label>
                                            <asp:TextBox ID="txtMotvSalidRef1" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Verificado</label>
                                            <asp:DropDownList class="form-control" ID="ddlVerMtvSal1" runat="server">
                                                <asp:ListItem Value="Si">SI </asp:ListItem>
                                                <asp:ListItem Value="No">NO </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Observaciones</label>
                                            <asp:TextBox ID="txtObservMotSal1" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Fecha Ingreso</label>
                                            <div class="input-group input-append date" id="datePicker11">
                                                <asp:TextBox ID="txtFechIngresRef1" class="form-control datepicker"
                                                    runat="server"></asp:TextBox>
                                                  <cc1:MaskedEditExtender ID="MaskedEditExtender10" runat="server" ClearMaskOnLostFocus="False"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtFechIngresRef1" MaskType="Date">
                                                </cc1:MaskedEditExtender>
                                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Verificado</label>
                                            <asp:DropDownList class="form-control" ID="ddlVerFechIng1" runat="server">
                                                <asp:ListItem Value="Si">SI </asp:ListItem>
                                                <asp:ListItem Value="No">NO </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Observaciones</label>
                                            <asp:TextBox ID="txtObservFecIng1" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Fecha Egreso</label>
                                            <div class="input-group input-append date" id="datePicker12">
                                                <asp:TextBox ID="txtFechEgres1" class="form-control datepicker"
                                                    runat="server"></asp:TextBox>
                                                  <cc1:MaskedEditExtender ID="MaskedEditExtender11" runat="server" ClearMaskOnLostFocus="False"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtFechEgres1" MaskType="Date">
                                                </cc1:MaskedEditExtender>
                                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Verificado</label>
                                            <asp:DropDownList class="form-control" ID="ddlVerfFechEgr1" runat="server">
                                                <asp:ListItem Value="Si">SI </asp:ListItem>
                                                <asp:ListItem Value="No">NO </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Observaciones</label>
                                            <asp:TextBox ID="txtObservEgres1" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </fieldset>
                            <br />
                            <fieldset>
                                <legend>Referencia Laboral #2</legend>
                                <div class="row">
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Empresa Anterior</label>
                                            <asp:TextBox ID="txtEmpAnt2" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Ultimo Salario</label>
                                            <asp:TextBox ID="txtUltSalEmp2" class="form-control"
                                                onkeypress="return soloNumeros(event)" runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Cargo</label>
                                            <asp:TextBox ID="txtCargoEmp2" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Motivo Salida</label>
                                            <asp:TextBox ID="txtMotivoSaldEmp2" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Fecha Ingreso</label>
                                            <div class="input-group input-append date" id="datePicker13">
                                                <asp:TextBox ID="txtFechIngEmp2" class="form-control datepicker"
                                                    runat="server"></asp:TextBox>
                                                 <cc1:MaskedEditExtender ID="MaskedEditExtender12" runat="server" ClearMaskOnLostFocus="False"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtFechIngEmp2" MaskType="Date">
                                                </cc1:MaskedEditExtender>
                                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Fecha Egreso</label>
                                            <div class="input-group input-append date" id="datePicker14">
                                                <asp:TextBox ID="txtFechaEgrsEmp2" class="form-control datepicker"
                                                    runat="server"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender13" runat="server" ClearMaskOnLostFocus="False"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" TargetControlID="txtFechaEgrsEmp2" MaskType="Date">
                                                </cc1:MaskedEditExtender>
                                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </fieldset>
                        </div>
                        <div class="tab-pane fade" id="Refer">
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Referencia Personal</label>
                                        <asp:TextBox ID="txtRefPers" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label" for="focusedInput">Verificado</label>
                                        <asp:DropDownList class="form-control" ID="ddlVerifRefPers" runat="server">
                                            <asp:ListItem Value="Si">SI </asp:ListItem>
                                            <asp:ListItem Value="No">NO </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Observaciones</label>
                                        <asp:TextBox ID="txtObservRefPers" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Parentesco</label>
                                        <asp:TextBox ID="txtparentRefPers" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label" for="focusedInput">Verificado</label>
                                        <asp:DropDownList class="form-control" ID="ddlVerifParentRefPers" runat="server">
                                            <asp:ListItem Value="Si">SI </asp:ListItem>
                                            <asp:ListItem Value="No">NO </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Observaciones</label>
                                        <asp:TextBox ID="txtObservParentRefPers" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Numero de Telefono</label>
                                        <asp:TextBox ID="txtNumTelfRefPers" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label" for="focusedInput">Verificado</label>
                                        <asp:DropDownList class="form-control" ID="ddlVerNumTelRefPers" runat="server">
                                            <asp:ListItem Value="Si">SI </asp:ListItem>
                                            <asp:ListItem Value="No">NO </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Observaciones</label>
                                        <asp:TextBox ID="txtObservNumTelRefPers" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Tiempo de Conocer</label>
                                        <asp:TextBox ID="txtTiempConRefPers" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label" for="focusedInput">Verificado</label>
                                        <asp:DropDownList class="form-control" ID="ddlVerTiempCncRefPers" runat="server">
                                            <asp:ListItem Value="Si">SI </asp:ListItem>
                                            <asp:ListItem Value="No">NO </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="control-label" for="focusedInput">Observaciones</label>
                                        <asp:TextBox ID="txtObservTiempCncRefPers" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="SaludEstud">
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Padece alguna enfermedad?</label>
                                        <asp:DropDownList class="form-control" ID="ddlPadEnfermedad" runat="server">
                                            <asp:ListItem Value="Si">SI </asp:ListItem>
                                            <asp:ListItem Value="No">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Tipo de Enfermedad</label>
                                        <asp:TextBox ID="txtTipoEnfermedad" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Desde</label>
                                        <asp:TextBox ID="txtDese" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="col-md-2 marginChkActivo">
                                            <div class="checkbox">
                                                <label>
                                                    Discapacidad?
                                                    <asp:CheckBox ID="ckdiscapacidad" runat="server" />
                                                </label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                            </div>
                            <fieldset>
                                <legend>Estudios</legend>
                                <div class="row">
                                    <div class="col-md-12 paddingContentTabs">
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Descripcion</label>
                                            <asp:TextBox ID="txtDescEstudios" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Centro de Estudios</label>
                                            <asp:TextBox ID="txtCentroEstudios" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </fieldset>
                        </div>
                        <div class="tab-pane fade" id="HistoEgres">
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <asp:GridView ID="gvHistEgresos" class="table table-striped table-hover" runat="server" Width="100%"
                                        AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                                        BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos">
                                        <AlternatingRowStyle Width="100px" />
                                        <Columns>
                                            <asp:BoundField DataField="fecha_ingreso" HeaderText="Ingreso" DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_egreso" HeaderText="Egreso" DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ubicacion" HeaderText="Ubicacion">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="depto" HeaderText="Departamento">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cargo" HeaderText="Cargo">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="motivo" HeaderText="Motivo" >
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecharegistro" HeaderText="Registro" DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <%--        <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btneditar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="editar" runat="server" Text="Editar" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Eliminar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btneliminar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="eliminar" runat="server" Text="Eliminar" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16px" />
                                                </asp:TemplateField>

                                            --%>
                                        </Columns>
                                    </asp:GridView>
                                    <%--        <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btneditar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="editar" runat="server" Text="Editar" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Eliminar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btneliminar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="eliminar" runat="server" Text="Eliminar" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16px" />
                                                </asp:TemplateField>

                                            --%>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="HistoSalario">
                            <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                    <asp:GridView ID="GvHistoricoSal" class="table table-striped table-hover" runat="server" Width="100%"
                                        AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                                        BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos">
                                        <AlternatingRowStyle Width="100px" />
                                        <Columns>
                                            <asp:BoundField DataField="id_salario" HeaderText="ID">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="salarioant" HeaderText="Sal. Ant.">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="salarionuevo" HeaderText="Sal. Nuevo">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="motivo" HeaderText="Motivo">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fechasol" HeaderText="Solicitud" DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fechaautoriza" HeaderText="Autoriza" DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="usuarioautoriza" HeaderText="Responsable">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <%--        <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btneditar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="editar" runat="server" Text="Editar" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Eliminar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btneliminar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="eliminar" runat="server" Text="Eliminar" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16px" />
                                                </asp:TemplateField>

                                            --%>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
                </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-3">
                        <%--<asp:Button ID="btnSalir" Class="btn btn btn-primary " runat="server" Text="Salir" OnClick="btnSalir_Click" />--%>
                        <asp:Button ID="btnAgregar" Class="btn btn-success" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                        <asp:Button ID="btnGuardar" Class="btn btn-info" runat="server" Text="Guardar" Visible="false" OnClick="btnGuardar_Click" />
                        <asp:Button ID="btnEliminar" Class="btn btn-danger" runat="server" Text="Eliminar" Visible="false" OnClick="btnEliminar_Click" />
                                                <asp:Button ID="btnReingresar" Class="btn btn-success" runat="server" Text="Reingresar" OnClick="btnReingresar_Click" Visible="false"/>

                    </div>
                     <div class="col-md-1">
                        <asp:Button ID="btnEstatusLiq" Class="btn btn-success" runat="server" Text="Liquidar" OnClick="btnEstatusLiq_Click" Visible="false" style="margin-left:500px;" />

                         </div>
                    <div class="col-md-1">
                        <asp:Button ID="BtnRenovar" Class="btn btn-warning" runat="server" Text="Renovar" OnClick="BtnRenovar_Click" Visible="false" style="margin-left:500px;" />

                         </div>
                </div>
            </div>
        </fieldset>
    </div>
    <script type="text/javascript">
        //$('#txtCedula').keyup(function (e) {
        //    var strongRegex = /^[0-9]*$/;
        //    // Use the literal regex notation so as not to double escape \ symbols!  Else,
        //    // var strongRegex = RegExp("^(?=.*\\d)(?=.*[!@@#$%^&*]+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).{8,}$");
           
        //     if (strongRegex.test($(this).val())) {
        //        alert('if');
        //    }
        //    else {
        //        alert('else');
        //    }

        //    return true;
        //});
        $(function () {
            $('#datePicker').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker1').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });


        $(function () {
            $('#datePicker2').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker3').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker4').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker5').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker6').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker7').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker8').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker9').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker10').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker11').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker12').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker13').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker14').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker15').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#datePicker16').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });

        $(function () {
            $('#<%= txtJefeInmed.ClientID %>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "JefeInmediato.asmx/getNombreJefe",
                        data: "{ 'nombreJefe': '" + request.term + "' }",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert('Ocurrio un error con su busqueda');
                        }
                    });
                },
                minLength: 0
            });
        });

        $(function () {
            $('#<%= txtBuscarNombre.ClientID %>').autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: "JefeInmediato.asmx/getNombreJefe",
                            data: "{ 'nombreJefe': '" + request.term + "' }",
                            type: "POST",
                            dataType: "json",
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                response(data.d);
                            },
                            error: function (result) {
                                alert('Ocurrio un error con su busqueda');
                            }
                        });
                    },
                    minLength: 0
                });
            });
            function soloNumeros(e) {
                key = e.keyCode || e.which;
                tecla = String.fromCharCode(key).toLowerCase();
                letras = "0123456789./";
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

