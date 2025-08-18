<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleMarcasTemporal.aspx.cs" Inherits="NominaRRHH.Presentacion.DetalleMarcasTemporal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    <style>
        table#gvDatosEmp tr td,table#GVDetNomEmpl tr td,table#gvpermisos tr td,table#gvResumen tr td {
            line-height:1.2;
        }
         table#gvDatosEmp th,table#GVDetNomEmpl th,table#gvpermisos th,table#gvResumen th {
            line-height:1.2;
        }
         div#marcas .row{
             margin-top: -16px;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top" id="marcas">
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
                    <div class="col-md-2">
                        <label class="control-label" for="focusedInput">Fecha Inicio</label>
                        <div class="input-group input-append date" id="datePicker">
                            <asp:TextBox ID="txtFechaIni" class="form-control datepicker"
                                runat="server"></asp:TextBox>
                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <label class="control-label" for="focusedInput">Fecha Fin</label>
                        <div class="input-group input-append date" id="datePicker2">
                            <asp:TextBox ID="txtFechaFin" class="form-control datepicker"
                                runat="server"></asp:TextBox>
                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
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
                <div class="row" visible="false" id="editMarcas" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Codigo</label>
                            <asp:TextBox ID="txtCodigo" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Fecha Inicio</label>
                            <div class="input-group input-append date" id="datePicker3">
                                <asp:TextBox ID="txtFecha" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" runat="server" id="calendarFin">
                            <label class="control-label" for="focusedInput">Fecha Fin</label>
                            <div class="input-group input-append date" id="datePicker4">
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
                        <asp:GridView ID="gvDatosEmp" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="False" AllowSorting="False" ShowFooter="false" ClientIDMode="Static">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                 <asp:BoundField DataField="nombre_Depto" HeaderText="Departamento">
                                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Codigo" HeaderText="Codigo">
                                    <ItemStyle Width="20px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombrecompleto" HeaderText="Nombre Completo">
                                    <ItemStyle Width="280px" HorizontalAlign="Left" />
                                </asp:BoundField>                               
                                <asp:BoundField DataField="cargo" HeaderText="Cargo">
                                    <ItemStyle Width="280px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="fecha_ingreso" HeaderText="Ingreso" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="30px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="estado" HeaderText="Estado">
                                    <ItemStyle Width="10px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombreturno" HeaderText="Turno">
                                    <ItemStyle Width="30px" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" CssClass="FooterStyle" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblRM" runat="server" Text="Registro de Marcas y Permisos" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="GVDetNomEmpl" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical" ClientIDMode="Static"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" OnSelectedIndexChanged="GVDetNomEmpl_SelectedIndexChanged" OnRowDataBound="GVDetNomEmpl_RowDataBound" ShowFooter="True">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                    <ControlStyle Width="5px" />
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:CommandField>

                                <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horae" HeaderText="Marca E">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horas" HeaderText="Marca S">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horaini" HeaderText="Hora E">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horafin" HeaderText="Hora S">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horast" HeaderText="HorasT" DataFormatString="{0:0.00}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="TotalHorasP" HeaderText="Horas Permisos">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>--%>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" CssClass="FooterStyle" />
                        </asp:GridView>
                    </div>
                </div>               
                <div class="row">
                    <div class="col-md-12">

                        <asp:GridView ID="gvpermisos" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical" ClientIDMode="Static"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="False" AllowSorting="False">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                <asp:BoundField DataField="fechaini" HeaderText="Inicio" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fechafin" HeaderText="Fin" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horaini" HeaderText="Hr Inicio">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horafin" HeaderText="Hr Fin">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="gocedeSalario" HeaderText="C/G.Salario">
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Descripcion" HeaderText="Tipo">
                                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="obs" HeaderText="Observacion">
                                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cantVacaciones" HeaderText="Dias">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horas" HeaderText="Horas">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="usuariograba" HeaderText="Registró">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" CssClass="FooterStyle" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblrh" runat="server" Text="Resumen de Horas" Font-Bold="True" ForeColor="#0033CC"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvResumen" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical" ClientIDMode="Static"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="False" AllowSorting="False">
                            <Columns>
                                <asp:BoundField DataField="horast" HeaderText="Marcas" DataFormatString="{0:0.00}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horasv" HeaderText="Vac" DataFormatString="{0:0.00}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horascg" HeaderText="C/G.Salario" DataFormatString="{0:0.00}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horassg" HeaderText="S/G.Salario" DataFormatString="{0:0.00}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="totalp" HeaderText="Total" DataFormatString="{0:0.00}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" CssClass="FooterStyle" />
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
            $('#datePicker,#datePicker2,#datePicker3,#datePicker4').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });

           

        });
    </script>
</asp:Content>
