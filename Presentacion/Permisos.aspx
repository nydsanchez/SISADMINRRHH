<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="NominaRRHH.Presentacion.Permisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/bootstrap-clockpicker.css" rel="stylesheet" />
    <link href="../Content/clockpicker.css" rel="stylesheet" />
    <link href="../Content/standalone.css" rel="stylesheet" />
    <link href="../Content/Styles.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    <script src="../Scripts/bootstrap-clockpicker.js"></script>
    <script src="../Scripts/jquery-clockpicker.js"></script>
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
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
                <fieldset>
                    <legend>Agregar Permisos</legend>
                    <div class="row">
                        <div class="col-md-12">
                            <%--<div class="col-md-3">
                                <label class="control-label" for="focusedInput">Ubicacion</label>
                                <asp:DropDownList class="form-control" ID="ddlUbicacion" runat="server">
                                </asp:DropDownList>
                            </div>--%>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Asignacion Permisos</label>
                                <asp:DropDownList class="form-control" ID="ddlAsigPerm" runat="server" OnSelectedIndexChanged="ddlAsigPerm_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="1" Selected="True"> Individual</asp:ListItem>
                                    <asp:ListItem Value="2"> Por Departamento</asp:ListItem>
                                    <asp:ListItem Value="3"> General</asp:ListItem>
                                    <asp:ListItem Value="4"> Ubicacion</asp:ListItem>
                                    <asp:ListItem Value="5"> Deptos x Ubicacion</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Tipo Permiso</label>
                                <asp:DropDownList class="form-control" ID="ddlTipoPermiso" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Dias u Horas</label>
                                <asp:DropDownList class="form-control" ID="ddlDiasHrs" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDiasHrs_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Selected="True"> Dias</asp:ListItem>
                                    <asp:ListItem Value="2"> Horas</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div runat="server" id="divempleado">
                                <div class="col-md-3">
                                    <label class="control-label" for="focusedInput">Codigo Empleado</label>
                                    <asp:TextBox ID="txtCodigo" class="form-control"
                                        runat="server" autocomplete="off" OnTextChanged="txtCodigo_TextChanged" AutoPostBack="True"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label class="control-label" for="focusedInput">Nombre Empleado</label>
                                    <asp:TextBox ID="txtNombreEmpleado" class="form-control"
                                        runat="server" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" runat="server" id="divubic" visible="false">
                                <label class="control-label" for="focusedInput">Ubicacion</label>
                                <asp:DropDownList class="form-control" ID="ddlubicacion" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3" runat="server" id="divproceso" visible="false">
                                <label class="control-label" for="focusedInput">Proceso</label>
                                <asp:DropDownList class="form-control" ID="ddlProceso" runat="server">
                                </asp:DropDownList>
                            </div>
                              
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Fecha Inicio</label>
                                <div class="input-group input-append date" id="datePicker">
                                    <asp:TextBox ID="txtFechaIni" class="form-control datepicker"
                                        runat="server" OnTextChanged="txtFechaIni_TextChanged" AutoPostBack="True"></asp:TextBox>
                                    <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                            <div runat="server" id="divdias">
                                <div class="col-md-3" id="datifechafin" runat="server">
                                    <label class="control-label" for="focusedInput">Fecha Fin</label>
                                    <div class="input-group input-append date" id="datePicker2">
                                        <asp:TextBox ID="txtFechaFin" class="form-control datepicker"
                                            runat="server" OnTextChanged="txtFechaFin_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-3" runat="server">
                                    <label class="control-label" for="focusedInput">Cantidad Dias</label>
                                    <asp:TextBox ID="txtCantidadDias" class="form-control"
                                        runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="divhoras" visible="false">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Hora Inicio</label>
                                <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                    <asp:TextBox ID="txtHoraIni" class="form-control"
                                        runat="server" OnTextChanged="txtHoraIni_TextChanged" AutoPostBack="True"></asp:TextBox>
                                    <span class="input-group-addon borderCalendar">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Hora Fin</label>
                                <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                    <asp:TextBox ID="txtHoraFin" class="form-control"
                                        runat="server" OnTextChanged="txtHoraFin_TextChanged" AutoPostBack="True"></asp:TextBox>
                                    <span class="input-group-addon borderCalendar">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-3" runat="server" id="div1" visible="true">
                                <label class="control-label" for="focusedInput">Cantidad Horas</label>
                                <asp:TextBox ID="txtHoras" class="form-control"
                                    runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <label class="control-label" for="focusedInput">Observacion</label>
                                <asp:TextBox ID="txtObserv" class="form-control" Rows="10" runat="server" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button class="btn btn-success btnagregarPermiso" ID="BtnAgregar" runat="server" Text="Agregar" OnClick="BtnAgregar_Click" />
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
        <div class="panel panel-info">
            <div class="panel-body ">
                <fieldset>
                    <legend>Consultar Permisos Por Empleado</legend>
                     <div class="row">
                                <div class="col-md-12">                                   
                                    <div class="col-md-3">
                                        <asp:RadioButtonList ID="rbRango" runat="server" OnSelectedIndexChanged="rbRango_SelectedIndexChanged" AutoPostBack="True">
                                             <asp:ListItem Selected="True" Value="1">Por Rango Fecha</asp:ListItem>
                                            <asp:ListItem Value="2">Tipo</asp:ListItem>
                                            <asp:ListItem Value="3">Todos</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Codigo Empleado</label>
                                <asp:TextBox ID="txtBuscar" class="form-control" placeholder="Ingrese Codigo"
                                    runat="server" onkeypress="return soloNumeros(event)" autocomplete="off">
                                </asp:TextBox>
                            </div>
                              <div class="col-md-3" id="divtipo" runat="server" visible="false">
                                <label class="control-label" for="focusedInput">Tipo Permiso</label>
                                <asp:DropDownList class="form-control" ID="ddltipoB" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div id="divrango" runat="server">
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Fecha Inicio</label>
                                <div class="input-group input-append date" id="datePicker3">
                                    <asp:TextBox ID="txtFechaIni2" class="form-control datepicker"
                                        runat="server"></asp:TextBox>
                                    <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Fecha Fin</label>
                                <div class="input-group input-append date" id="datePicker4">
                                    <asp:TextBox ID="txtFechaFin2" class="form-control datepicker"
                                        runat="server"></asp:TextBox>
                                    <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                                
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Hora Inicio</label>

                                <asp:TextBox ID="TxtHoraini2" class="form-control datepicker" runat="server"></asp:TextBox>

                            </div>

                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Hora Fin</label>

                                <asp:TextBox ID="TxtHorafin2" class="form-control datepicker"
                                    runat="server"></asp:TextBox>

                            </div>
                                </div>
                            <div class="col-md-2"  runat="server" visible="false" id="lblTitle">
                                <label class="control-label" for="focusedInput">Dias</label>
                                <asp:TextBox ID="txtDiasEdit" class="form-control col-md-2"
                                    runat="server" onkeypress="return soloNumeros(event)" autocomplete="off" ReadOnly="True"></asp:TextBox>
                            </div>

                            <div class="col-md-2"  runat="server" visible="false" id="lblTitle2">
                                <label class="control-label" for="focusedInput">Horas</label>
                                <asp:TextBox ID="txtHorasEdit" class="form-control col-md-2" runat="server" onkeypress="return soloNumeros(event)" autocomplete="off" ReadOnly="True"></asp:TextBox>
                            </div>

                            <div class="col-md-2" runat="server" visible="false" style="margin-top: 27px;" id="divEdit">
                                <asp:Button class="btn btn-warning" ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click" Visible="False" />
                            </div>
                            <div class="col-md-2" style="margin-top: 27px;" runat="server" visible="false" id="divElim">
                                <asp:Button class="btn btn-danger" ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
                            </div>
                            <div class="col-md-2" style="margin-top: 27px;">
                                <asp:Button class="btn btn-primary" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                            </div>
                        </div>
                        <br />
                        <div class="col-md-12">
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GVDetallePermisos" class="table table-striped table-hover" runat="server"
                                AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical"
                                BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" OnSelectedIndexChanged="GVDetallePermisos_SelectedIndexChanged">
                                <AlternatingRowStyle Width="578px" />
                                <Columns>
                                    <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                        <ControlStyle Width="5px" />
                                        <HeaderStyle Width="5px" />
                                        <ItemStyle Width="5px" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechaini" DataFormatString="{0:d}" HeaderText="Fecha Inicio">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechafin" DataFormatString="{0:d}" HeaderText="Fecha Fin">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="horaini" HeaderText="Hora Inicio" />
                                    <asp:BoundField DataField="horafin" HeaderText="Hora Fin" />
                                    <asp:BoundField DataField="dias" HeaderText="Dias">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="horas" HeaderText="Horas" />
                                    <asp:BoundField DataField="obs" HeaderText="Motivo Permiso">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Tipo Permiso">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </fieldset>
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

    </script>
</asp:Content>
