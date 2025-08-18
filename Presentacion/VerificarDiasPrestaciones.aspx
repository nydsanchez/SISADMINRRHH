<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerificarDiasPrestaciones.aspx.cs" Inherits="NominaRRHH.Presentacion.VerificarDiasPrestaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
   
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
                <div class="row">
                    <div class="col-md-12" style="margin-left: 0px; top: 0px; left: 0px;">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Codigo</label>
                            <asp:TextBox ID="txtCodEmp" autofocus="true" class="form-control" placeholder="Digite Codigo"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                            <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Fecha Corte</label>
                                        <div class="input-group input-append date" id="datePicker2">
                                            <asp:TextBox ID="txtFecCorte" class="form-control datepicker" 
                                                runat="server"></asp:TextBox>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                          <div class="col-md-1">
                            <%--<asp:Button ID="btnCalcLiq" Class="btn btn-info" Style="margin-top: 28px;" Visible="false" runat="server" Text="Calcular" />--%>
                            <asp:Button ID="Button1" Class="btn btn-info" Style="margin-top: 28px;" runat="server" Text="Buscar" OnClick="Button1_Click" />

                        </div>
                       
                       
                        <div class="col-md-1">
                                      
                                            <div ID="btnExport" Class="btn btn-info" Style="margin-top: 28px;">Exportar</div>
                                        
                                    </div>
                        </div>
                    </div>
                 <div id="Listado">
            <div class="row">
                    <div class="col-md-12" style="margin-left: 0px; top: 0px; left: 0px;">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Codigo:</label>
                            <label  id="TxtCodigo" class ="form-control" runat="server" ></label>
                        </div>
                         <div class="col-md-5">
                            <label class="control-label" for="focusedInput">Nombre:</label>
                            <label ID="txtNombre" class="form-control"
                                runat="server" ></label>
                        </div>
                            <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Ingreso:</label>
                            <label ID="txtFechaing" class="form-control" 
                                runat="server" ></label>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Salario:</label>
                            <label ID="TxtTipoSalario" class="form-control" 
                                runat="server" ></label>
                        </div>

                    </div></div>
                       <div class="row">
                    <div class="col-md-12" style="margin-left: 0px; top: 0px; left: 0px;">
                         <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Estado:</label>
                            <label ID="TxtEstado" class="form-control" 
                                runat="server" ></label>
                        </div>
                        </div></div>
                <br />
               
                <div class="row">
                    <div class="col-md-12">
                        <label class="control-label" for="focusedInput">Informacion General</label>
                        <asp:GridView ID="GVLiquidacion" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="True" AllowSorting="True" >
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                
                                 <asp:BoundField DataField="vacacumuladas" HeaderText="Acumuladas"  DataFormatString="{0:F2}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="vacdescansadas" HeaderText="Descansadas"  DataFormatString="{0:F2}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                              <%--   <asp:BoundField DataField="dsubsidios" HeaderText="Subsidios"  DataFormatString="{0:F2}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="vacpagadas" HeaderText="Pagadas" DataFormatString="{0:F2}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="saldovacaciones" HeaderText="Saldo Actual" DataFormatString="{0:F2}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                               
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>                    
               <br />
                <div class="row">
                    <div class="col-md-12">
                      <label class="control-label" for="focusedInput">Detalle de Vacaciones Descansadas: </label> <label class="control-label" for="focusedInput" runat="server" id="lblmes"></label>
                      <asp:GridView ID="GVDEtalleDesc" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" ShowFooter="True" 
                             AllowSorting="True" OnRowDataBound="GVDEtalleDesc_RowDataBound">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>                             
                                 <asp:BoundField DataField="FechaIni" HeaderText="FechaIni" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FechaFin" HeaderText="FechaFin" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horaini" HeaderText="Hora Inicio" >
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horafin" HeaderText="Hora Fin" >
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cantVacaciones" HeaderText="Dias">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="horas" HeaderText="Hrs" DataFormatString="{0:F2}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:F2}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                               
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                  <%--    <br />
                <div class="row">
                    <div class="col-md-12">
                      <label class="control-label" for="focusedInput">Detalle de Subsidios: </label> <label class="control-label" for="focusedInput" runat="server" id="Label2"></label>
                      <asp:GridView ID="GVDetalleSubsidios" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" ShowFooter="True" 
                             AllowSorting="True" OnRowDataBound="GVDetalleSubsidios_RowDataBound">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>                             
                                 <asp:BoundField DataField="FechaIni" HeaderText="FechaIni" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FechaFin" HeaderText="FechaFin" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>                              
                                <asp:BoundField DataField="diascalendario" HeaderText="Dias" DataFormatString="{0:F2}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                               <asp:BoundField DataField="diasprestaciones" HeaderText="Dias Prestaciones" DataFormatString="{0:F2}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>--%>
                      <br />
                <div class="row">
                    <div class="col-md-12">
                      <label class="control-label" for="focusedInput">Detalle de Vacaciones Pagadas: </label> <label class="control-label" for="focusedInput" runat="server" id="Label1"></label>
                      <asp:GridView ID="GVDEtallePag" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" ShowFooter="True" 
                             AllowSorting="True" OnRowDataBound="GVDEtallePag_RowDataBound">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>                                                           
                                <asp:BoundField DataField="Periodo" HeaderText="Periodo">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="feccerrado" HeaderText="Cierre" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                   <asp:BoundField DataField="diasVacacionesPagar" HeaderText="Dias" DataFormatString="{0:F2}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                               
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                </div>
          </div>
        </div>
    </div>
    
        <script type="text/javascript">
        $(function () {
            $('#datePicker,#datePicker2').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
            $("#btnExport").click(function (e) {
                window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('#Listado').html()));
                e.preventDefault();
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

