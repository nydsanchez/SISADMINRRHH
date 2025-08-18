<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Amonestaciones.aspx.cs" Inherits="NominaRRHH.Presentacion.Amonestaciones" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="vgeneral" runat="server">
            <div class="container mar-top">
                <div class="panel panel-info">
                    <div class="panel-body" style="margin-top: 0px">

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <asp:Button ID="Button1" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Administración Amonestaciones -->" OnClick="Button1_Click" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="Button3" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Administrar Tipos Sancion" OnClick="Button3_Click" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="Button5" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Administrar Tipos Faltas" OnClick="Button5_Click" />
                                </div>
                            </div>
                        </div>

                        <br />
                        <br />
                        <br />

                        <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <asp:Label ID="lblAlert" runat="server"></asp:Label>
                        </div>
                        <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <asp:Label ID="LblSuccess" runat="server"></asp:Label>
                        </div>

                        <fieldset>
                            <legend>Agregar Amonestación</legend>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">
                                            <strong>Detalle Amonestación
                                            </strong>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Codigo Empleado</label>
                                        <asp:TextBox ID="txtCodigo" class="form-control"
                                            runat="server" onkeypress="return soloNumeros(event)" autocomplete="off" AutoPostBack="True" OnTextChanged="txtCodigo_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1"></div>
                                    <div class="col-md-8">
                                        <label class="control-label" for="focusedInput">Nombre</label>
                                        <asp:TextBox ID="txtBuscarNombre" class="form-control"
                                            runat="server" autocomplete="off" AutoPostBack="True" OnTextChanged="txtBuscarNombre_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Tipo de Amonestación</label>
                                        <asp:DropDownList class="form-control" ID="ddltipo" runat="server" DataTextField="descripcion_amonestacion" DataValueField="idAmonestacion" AutoPostBack="True" OnSelectedIndexChanged="ddltipo_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">CODIGO:</label>
                                        <asp:Label ID="lblcodigo" runat="server" Text="Label" class="control-label" for="focusedInput"></asp:Label>
                                        <label class="control-label" for="focusedInput">NIVEL FALTA:</label>
                                        <asp:Label ID="nivelf" runat="server" Text="Label" class="control-label" for="focusedInput"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                    </div>

                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Tipo Sanción</label>
                                        <asp:DropDownList class="form-control" ID="ddltipoSancion" runat="server" DataTextField="Descripcion" DataValueField="Id_Descripcion">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Fecha Amonestación</label>
                                        <div class="input-group input-append date" id="datePicker">
                                            <asp:TextBox ID="txtFechaIni" class="form-control datepicker"
                                                runat="server"></asp:TextBox>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-9">
                                    <label class="control-label" for="focusedInput">observación de Amonestación</label>
                                    <asp:TextBox ID="txtob" runat="server" TextMode="MultiLine" class="form-control" Height="67px" Width="700px"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Button ID="btnMostrar" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Aceptar" OnClick="btnMostrar_Click" />
                                    <asp:Button ID="btnActualizar" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Actualizar" OnClick="btnActualizar_Click" />
                                    <asp:Button ID="btnCancelar" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Cancelar" OnClick="btnCancelar_Click" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">

                                    <asp:Button ID="btnEliminar" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Eliminar " OnClick="btnEliminar_Click1" OnClientClick="return confirm('¿Desea EliminarAmonestación?');" />
                                    <asp:Button ID="btnEditar" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Editar " OnClick="btnEditar_Click1" />
                                </div>
                            </div>
                            <div class="row">
                            </div>
                        </fieldset>
                        <br />
                        <fieldset>
                            <legend>Consultar Amonestación Por Empleado</legend>
                            <br />
                            <br />
                            <div class="alert alert-dismissible alert-warning" id="divconsultaAlert" runat="server">
                                <button type="button" class="close" data-dismiss="alert">×</button>
                                <asp:Label ID="lblconsultaAlert" runat="server"></asp:Label>
                            </div>
                            <div class="alert alert-dismissible alert-success" id="divconsultaSuccess" runat="server">
                                <button type="button" class="close" data-dismiss="alert">×</button>
                                <asp:Label ID="lblconsultaSuccess" runat="server"></asp:Label>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <asp:RadioButtonList ID="rbCodgio" runat="server" OnSelectedIndexChanged="rbCodgio_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Selected="True" Value="1">Por Codigo</asp:ListItem>
                                            <asp:ListItem Value="2">Todos</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:RadioButtonList ID="rbRango" runat="server" OnSelectedIndexChanged="rbRango_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Selected="True" Value="1">Por Rango Fecha</asp:ListItem>
                                            <asp:ListItem Value="2">Todos</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2" >
                                        <asp:Label ID="lblcodigob" runat="server" Text="Codigo Empleado"></asp:Label>

                                        <asp:TextBox ID="txtBuscar" class="form-control" placeholder="Ingrese Codigo"
                                            runat="server" onkeypress="return soloNumeros(event)" autocomplete="off">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-md-2" runat="server" id="divfechaini" >
                                        <asp:Label ID="lblfechaini" runat="server" Text="Fecha Inicio"></asp:Label>
                                        <div class="input-group input-append date" id="datePicker1">
                                            <asp:TextBox ID="txtFechaIni2" class="form-control datepicker"
                                                runat="server"></asp:TextBox>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-md-2" runat="server" id="divfechafin">
                                        <asp:Label ID="lblfechafin" runat="server" Text="Fecha Fin"></asp:Label>
                                        <div class="input-group input-append date" id="datePicker2">
                                            <asp:TextBox ID="txtFechaFin2" class="form-control datepicker"
                                                runat="server"></asp:TextBox>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>


                                </div>
                            </div>
                            <br />
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2" style="margin-top: 27px;">
                                        <asp:Button Class="btn btn-info" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                        <asp:Button Class="btn btn-info" ID="btnfpcus" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Visible="False" />
                                    </div>
                                    <div class="col-md-2" style="margin-top: 27px;">
                                        <asp:Button class="btn btn-primary" ID="Button9" runat="server" Text="Generar Reporte" OnClick="Button9_Click" />

                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <br />

                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvAmonestaciones" class="table table-striped table-hover" runat="server"
                                        AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical"
                                        BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" OnSelectedIndexChanged="GVDetallePermisos_SelectedIndexChanged" DataKeyNames="idAmonestacion,IdSancion">
                                        <AlternatingRowStyle Width="578px" />
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                                <ControlStyle Width="5px" />
                                                <HeaderStyle Width="5px" />
                                                <ItemStyle Width="5px" />
                                            </asp:CommandField>

                                            <asp:BoundField DataField="NombreComplet" HeaderText="Nombre">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CodigoEmp" HeaderText="Codigo">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DetalleFalta" HeaderText="Falta">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Codigo" HeaderText="Codigo Falta" />
                                            <asp:BoundField DataField="fechaA" DataFormatString="{0:d}" HeaderText="Fecha Amonestaciòn">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Sancion" HeaderText="Sancion" />
                                            <asp:BoundField DataField="Observacion" HeaderText="Observacion" />
                                            <asp:BoundField DataField="Fechagraba" HeaderText="fecha Graba" DataFormatString="{0:d}">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Horagraba" HeaderText="horaGraba" />
                                            <asp:BoundField DataField="Usuario" HeaderText="usuario">
                                                <ItemStyle Width="60px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </fieldset>
                        <br />

                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwadmin" runat="server">
            <div class="container mar-top">
                <div class="panel panel-info">
                    <div class="panel-body" style="margin-top: 0px">
                        <br />
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <asp:Button ID="Button2" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Regresar <--" OnClick="Button2_Click" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="Button8" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Administrar Tipos Sancion" OnClick="Button8_Click" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="Button11" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Administrar Tipos Faltas" OnClick="Button11_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />

                        <div class="alert alert-dismissible alert-warning" id="divAmon1" runat="server">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <asp:Label ID="lblalertAmon" runat="server"></asp:Label>
                        </div>
                        <div class="alert alert-dismissible alert-success" id="divAmon2" runat="server">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <asp:Label ID="lblsuccesstAmon" runat="server"></asp:Label>
                        </div>

                        <fieldset>
                            <legend>Administración Tipos Amonestaciones</legend>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">
                                            <strong>Detalle Amonestación
                                            </strong>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <label class="control-label" for="focusedInput">Detalle Falta</label>
                                    <asp:TextBox ID="txtdetalleF" class="form-control"
                                        runat="server" autocomplete="off" TextMode="MultiLine" Height="73px" Width="1041px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Codigo Falta</label>
                                        <asp:TextBox ID="txtcodigoF" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Nivel Falta</label>
                                        <asp:DropDownList class="form-control" ID="ddlnivelF" runat="server" DataTextField="Descripcion" DataValueField="Id_Descripcion" AutoPostBack="True" OnSelectedIndexChanged="ddlnivelF_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>


                                </div>
                            </div>
                            <br />


                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Button ID="btnACeptarAmon" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Aceptar" OnClick="btnACeptarAmon_Click" />
                                    <asp:Button ID="btnActualizarAmon" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Actualizar" OnClick="btnActualizarAmon_Click" />
                                    <asp:Button ID="btnCancelarAmon" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Cancelar" OnClick="btnCancelarAmon_Click" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">

                                    <%-- <asp:Button ID="btnEliminarAmon" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Eliminar " OnClientClick="return confirm('¿Desea Eliminar Tipo Amonestación?');" OnClick="btnEliminarAmon_Click" />
                                    <asp:Button ID="btnEditarAmon" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Editar " OnClick="btnEditarAmon_Click" />--%>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtbuscarcodigo" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="Button4" runat="server" Text="Buscar" Class="btn btn-info" OnClick="Button4_Click" />
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvTiposAmonestacion" class="table table-striped table-hover" runat="server"
                                        AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical"
                                        BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" DataKeyNames="idAmonestacion,Id_Descripcion" OnSelectedIndexChanged="gvTiposAmonestacion_SelectedIndexChanged">

                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                                <ControlStyle Width="5px" />
                                                <HeaderStyle Width="5px" />
                                                <ItemStyle Width="5px" />
                                            </asp:CommandField>

                                            <asp:BoundField DataField="DetalleFalta" HeaderText="Detalle Falta"></asp:BoundField>
                                            <asp:BoundField DataField="Codigo" HeaderText="Codigo Falta"></asp:BoundField>
                                            <asp:BoundField DataField="NivelFalta" HeaderText="Nivel Falta"></asp:BoundField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </fieldset>
                        <br />

                        <br />


                    </div>
                </div>
        </asp:View>
        <asp:View ID="vwSancion" runat="server">
            <div class="container mar-top">
                <div class="panel panel-info">
                    <div class="panel-body" style="margin-top: 0px">
                        <br />
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <asp:Button ID="Button12" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Regresar <--" OnClick="Button12_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />

                        <div class="alert alert-dismissible alert-warning" id="divsancionAlert" runat="server">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <asp:Label ID="LabelALertSancion" runat="server"></asp:Label>
                        </div>
                        <div class="alert alert-dismissible alert-success" id="divsancionSuccess" runat="server">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <asp:Label ID="LabelSuccessSancion" runat="server"></asp:Label>
                        </div>

                        <fieldset>
                            <legend>Adminstración Tipos sanción</legend>
                            <br />

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Detalle Sancion</label>
                                        <asp:TextBox ID="txtDetallesancion" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-md-12">

                                    <asp:Button ID="btnAceptarSancion" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Aceptar" OnClick="btnAceptarSancion_Click" />

                                    <asp:Button ID="btnActualizarSancion" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Actualizar" OnClick="btnActualizarSancion_Click" />


                                    <asp:Button ID="btnCancelarSancion" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px; height: 45px;" Text="Cancelar" OnClick="btnCancelarSancion_Click" />

                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="col-md-3">
                                            <asp:GridView ID="gvtiposSancion" class="table table-striped table-hover" runat="server"
                                                AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical"
                                                BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" DataKeyNames="Id_Descripcion" OnSelectedIndexChanged="gvtiposSancion_SelectedIndexChanged">

                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                                        <ControlStyle Width="5px" />
                                                        <HeaderStyle Width="5px" />
                                                        <ItemStyle Width="5px" />
                                                    </asp:CommandField>

                                                    <asp:BoundField DataField="Descripcion" HeaderText="Tipo Sancion"></asp:BoundField>


                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-3">
                                    </div>
                                </div>
                        </fieldset>
                        <br />

                        <br />


                    </div>
                </div>
        </asp:View>
        <asp:View ID="vwFalta" runat="server">
            <div class="container mar-top">
                <div class="panel panel-info">
                    <div class="panel-body" style="margin-top: 0px">
                        <br />
                        <br />
                        <br />
                        <div class="row">

                            <div class="col-md-3">
                                <asp:Button ID="Button20" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Regresar <--" OnClick="Button20_Click1" />
                            </div>
                        </div>

                        <br />
                        <br />

                        <div class="alert alert-dismissible alert-warning" id="divfaltaAlert" runat="server">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <asp:Label ID="lblFaltaalert" runat="server"></asp:Label>
                        </div>
                        <div class="alert alert-dismissible alert-success" id="divfaltaSuccess" runat="server">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                            <asp:Label ID="lblFaltasuccess" runat="server"></asp:Label>
                        </div>

                        <fieldset>
                            <legend>Adminstración Tipos Falta</legend>
                            <br />
                            <div class="row">
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Detalle Falta</label>
                                        <asp:TextBox ID="txtDetalleFalta" class="form-control"
                                            runat="server" autocomplete="off"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <br />


                            <div class="row">
                                <div class="col-md-12">

                                    <asp:Button ID="btnAceptarFalta" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Aceptar" OnClick="btnAceptarFalta_Click" />

                                    <asp:Button ID="btnActualizarFalta" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Actualizar" OnClick="btnActualizarFalta_Click" />

                                    <asp:Button ID="btnCancelarFalta" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Cancelar" OnClick="btnCancelarFalta_Click" />

                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvFalta" class="table table-striped table-hover" runat="server"
                                        AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical"
                                        BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" DataKeyNames="Id_Descripcion" OnSelectedIndexChanged="gvFalta_SelectedIndexChanged">

                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                                <ControlStyle Width="5px" />
                                                <HeaderStyle Width="5px" />
                                                <ItemStyle Width="5px" />
                                            </asp:CommandField>

                                            <asp:BoundField DataField="Descripcion" HeaderText="Tipo Falta"></asp:BoundField>


                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </fieldset>
                        <br />

                        <br />

                    </div>
                </div>
            </div>
        </asp:View>

        <asp:View ID="vwreport" runat="server">
            <div class="container mar-top">
                <div class="panel panel-info">
                    <div class="panel-body" style="margin-top: 0px">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <asp:Button ID="Button10" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Regresar <--" OnClick="Button10_Click" />
                                </div>
                            </div>
                            <br />
                            <br />
                            <br />
                            <div class="row">
                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1107px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="430px">
                                </rsweb:ReportViewer>
                            </div>
                        </div>
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
        </asp:View>
        <asp:View ID="View2" runat="server"></asp:View>
    </asp:MultiView>

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
