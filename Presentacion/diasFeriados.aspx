<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="diasFeriados.aspx.cs" Inherits="NominaRRHH.Presentacion.diasFeriados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    <script src="../Scripts/bootstrap-clockpicker.js"></script>
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
                    <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                </div>
                <fieldset>
                    <legend>Dias Feriados</legend>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Fecha</label>
                                <div class="input-group input-append date" id="datePicker">
                                    <asp:TextBox ID="txtFecha" class="form-control datepicker"
                                        runat="server"></asp:TextBox>
                                    <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Total Horas</label>
                                <asp:TextBox ID="txtCantidadDias" class="form-control"
                                    runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="control-label" for="focusedInput">Descripcion</label>
                                <asp:TextBox ID="txtDescripcion" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <asp:Button class="btn btn-success btnagregarPermiso" ID="BtnAgregar" runat="server" Text="Agregar" OnClick="BtnAgregar_Click" />
                                <asp:Button class="btn btn-info btnagregarPermiso" ID="BtnEditar" runat="server" Text="Editar" Visible ="false" OnClick="BtnEditar_Click" />
                                <asp:Button class="btn btn-danger btnagregarPermiso" ID="BtnEliminar" runat="server" Text="Eliminar" Visible ="false" OnClick="BtnEliminar_Click" />
                            </div>
                        </div>
                    </div>
                </fieldset>
                <br />
                <div class="row">
                    <div class="col-md-3">
                        <label class="control-label" for="focusedInput">Fecha Inicio</label>
                        <div class="input-group input-append date" id="datePicker1">
                            <asp:TextBox ID="txtFechaIni" class="form-control datepicker"
                                runat="server"></asp:TextBox>
                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label" for="focusedInput">Fecha Fin</label>
                        <div class="input-group input-append date" id="datePicker2">
                            <asp:TextBox ID="txtFechaFin" class="form-control datepicker"
                                runat="server"></asp:TextBox>
                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>                                 
                        <div class="col-md-2">
                            <asp:Button ID="btnBuscar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                        </div>
                    </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="GVdiasFeriados" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" OnPageIndexChanging="GVdiasFeriados_PageIndexChanging"
                            OnSelectedIndexChanged="GVdiasFeriados_SelectedIndexChanged" AllowPaging="True" AllowSorting="True">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                    <ControlStyle Width="5px" />
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:CommandField>
                                <asp:BoundField DataField="fechaFeriado" HeaderText="Fecha Feriada">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="observaciones" HeaderText="Descripcion">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horast" HeaderText="Total Horas">
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
        $(function () {
            $('#datePicker,#datePicker1,#datePicker2').datepicker({
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
