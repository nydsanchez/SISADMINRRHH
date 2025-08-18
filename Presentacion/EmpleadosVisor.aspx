<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpleadosVisor.aspx.cs" Inherits="NominaRRHH.Presentacion.EmpleadosVisor" %>

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
                                            ErrorTooltipEnabled="True" runat="server" ID="mskD" />--%>
                                        <%-- ClearMaskOnLostFocus="False" --%>
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

