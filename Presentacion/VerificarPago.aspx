<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerificarPago.aspx.cs" Inherits="NominaRRHH.Presentacion.VerificarPago" %>

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
                <br />
                  <div class="row">
                    <div class="col-md-12">
                   
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Salario Mensual:</label>
                            <label ID="txtSalMens" class="form-control" 
                                runat="server" ></label>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Salario Mayor/Dia:</label>
                            <label ID="txtSalMayor" class="form-control" 
                                runat="server" ></label>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Salario Promedio/Dia:</label>
                            <label ID="txtSalProm" class="form-control" 
                                runat="server" ></label>
                        </div>
                       
                    </div>
              
                </div>
               
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <label class="control-label" for="focusedInput">Ingresos de los ultimos Seis Meses</label>
                        <asp:GridView ID="GVLiquidacion" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="GVLiquidacion_SelectedIndexChanged">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                    <ControlStyle Width="5px" />
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:CommandField>
                                 <asp:BoundField DataField="MesNumero" HeaderText="No.">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MesNombre" HeaderText="Mes">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Salario" HeaderText="Salario" DataFormatString="{0:C}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Incentivo" HeaderText="Incentivo" DataFormatString="{0:C}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Beneficio" HeaderText="Beneficio" DataFormatString="{0:C}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Ingreso" HeaderText="Total" DataFormatString="{0:C}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PromedioDias" HeaderText="Pago x Dia" DataFormatString="{0:F2}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>                    
               <br />
                <div class="row">
                    <div class="col-md-12">
                      <label class="control-label" for="focusedInput">Pagos correspondientes a: </label> <label class="control-label" for="focusedInput" runat="server" id="lblmes"></label>
                      <asp:GridView ID="GVDEtallePago" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" ShowFooter="True" 
                            AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="GVLiquidacion_SelectedIndexChanged" OnRowDataBound="GVDEtallePago_RowDataBound">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>                             
                                 <asp:BoundField DataField="FechaIni" HeaderText="FechaIni">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FechaFin" HeaderText="FechaFin">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Periodo" HeaderText="Periodo" >
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Semana" HeaderText="Semana" >
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Salario" HeaderText="Salario" DataFormatString="{0:C}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Incentivo" HeaderText="Incentivo" DataFormatString="{0:C}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Beneficio" HeaderText="Beneficio" DataFormatString="{0:C}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Dias" HeaderText="Dias" >
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SalarioDias" HeaderText="SalarioDias" DataFormatString="{0:C}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IncentivoDias" HeaderText="IncentivoDias" DataFormatString="{0:C}">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BeneficioDias" HeaderText="BeneficioDias" DataFormatString="{0:C}">
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

