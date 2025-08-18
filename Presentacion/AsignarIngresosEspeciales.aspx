<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsignarIngresosEspeciales.aspx.cs" Inherits="NominaRRHH.Presentacion.AsignarIngresosEspeciales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
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
                <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="lblAlert" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="LblSuccess" runat="server" Visible="false"></asp:Label>
                </div>
                <%--  <div class="row">
                    <div class="col-md-12" style="margin-left: 250px;">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Codigo</label>
                            <asp:TextBox ID="txtCodigo" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Tipo Deducciones</label>
                            <asp:DropDownList class="form-control" ID="ddlTipDeduc2" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnBuscar" Class="btn btn-info" Style="margin-top: 22px;" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                        </div>
                    </div>
                </div>--%>
                <br />
                <div class="row">
                    <div class="col-md-12 paddingContentTabs">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Codigo</label>
                            <asp:TextBox ID="txtcodigoAsig" class="form-control"
                                runat="server" autocomplete="off" OnTextChanged="txtcodigoAsig_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label" for="focusedInput">Nombre</label>
                            <asp:TextBox ID="TxtNombreE" class="form-control" ReadOnly="true"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                         <div runat="server" id="divsalario" visible="false">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Sal. Esp. Prestaciones</label>
                            <asp:TextBox ID="TxtSalarioEsp" class="form-control" runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                         <div class="col-md-1">
                            <asp:Button ID="Button1" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="--> Cambiar" OnClick="btnCambiar_Click" />
                        </div>
                    </div>

                    </div>
                </div>
              
                <div class="row">

                    <div class="col-md-12">
                        <div id="editarhide2" runat="server">
                            <div class="col-md-1" runat="server">
                                <label class="control-label" for="focusedInput">Periodo</label>
                                <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div id="editarhide" runat="server">
                            <div class="col-md-3" runat="server" id="divTipoDeduc">
                                <label class="control-label" for="focusedInput">Tipo Ingreso</label>
                                <asp:DropDownList class="form-control" ID="ddlTipIng" runat="server">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-2" runat="server">
                            <label class="control-label" for="focusedInput">Total</label>
                            <asp:TextBox ID="txtTotal" class="form-control" runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>

                        <div class="col-md-2" runat="server" visible="false">
                            <div class="col-md-1 marginChkActivo">
                                <div class="checkbox">
                                    <label class="control-label" for="focusedInput">
                                        <asp:CheckBox ID="chkRecurrente" runat="server" Checked="true" />
                                        <strong>Recurrente</strong>
                                    </label>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>


                <div class="row">
                    <div runat="server" id="editarhide6">
                        <div class="col-md-10 ">
                            <label class="control-label" for="focusedInput">Observacion</label>
                            <asp:TextBox ID="txtObservEmpl" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div runat="server" id="editarhide5">
                        <div class="col-md-1">
                            <asp:Button ID="btnGuardar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Asignar" OnClick="btnGuardar_Click" />
                            <asp:Button ID="btnEditar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Visible="false" Text="Editar" OnClick="btnEditar_Click" />
                        </div>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="btnDeshabilitar" Class="btn btn-danger" Style="margin-top: 22px;" runat="server" Visible="false" Text="Deshabilitar" OnClick="btnDeshabilitar_Click" />
                    </div>
                </div>

                <br />
                <fieldset>
                    <legend></legend>
                    <label class="control-label" for="focusedInput">Ingresos Especiales</label>
                    <div class="row" id="AdIndemnizacion" runat="server" style="margin-top: 10px;">
                        <div class="col-md-12">

                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GVIngresos" class="table table-striped table-hover" runat="server" DataKeyNames="id,tipoIng"
                                        AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical" ShowFooter="True"
                                        BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                                        AllowPaging="True" AllowSorting="True" OnRowDeleting="GVIngresos_RowDeleting" OnSelectedIndexChanged="GVIngresos_SelectedIndexChanged"
                                        OnRowDataBound="GVIngresos_RowDataBound" OnPageIndexChanging="GVIngresos_PageIndexChanging">
                                        <AlternatingRowStyle Width="100px" />
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                                <ControlStyle Width="5px" />
                                                <HeaderStyle Width="5px" />
                                                <ItemStyle Width="5px" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="id" HeaderText="ID">
                                                <ItemStyle Width="5px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%-- <asp:BoundField DataField="codigo_empleado" HeaderText="Codigo Empleado">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombrecompleto" HeaderText="Nombre">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>--%>
                                            <asp:BoundField DataField="devengadoNombre" HeaderText="Ingreso">
                                                <ItemStyle Width="5px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="total" HeaderText="Total">
                                                <ItemStyle Width="5px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecreg" HeaderText="Registro">
                                                <ItemStyle Width="5px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>

                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#datePicker2,#datePicker3,#datePicker1').datepicker({
                format: 'dd/mm/yyyy'
            })
                .on('changeDate', function (e) {
                    $('.datepicker dropdown-menu').hide();
                });
            $('#datePicker4,#datePicker5').datepicker({
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

