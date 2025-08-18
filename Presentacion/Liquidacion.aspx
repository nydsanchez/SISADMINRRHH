<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Liquidacion.aspx.cs" Inherits="NominaRRHH.Presentacion.Liquidacion" %>

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
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off" OnTextChanged="txtCodEmp_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label" for="focusedInput">Nombre</label>
                            <asp:TextBox ID="txtNombre" autofocus="true" class="form-control"
                                runat="server" autocomplete="off" ReadOnly="True"></asp:TextBox>
                        </div>
                            <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Fecha Ingreso </label>
                            <asp:TextBox ID="txtFechaing" class="form-control" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy}"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        </div>
                    </div>
                         <div class="row">
                    <div class="col-md-12" style="margin-left: 0px; top: 0px; left: 0px;">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Fecha Egreso</label>
                            <div class="input-group input-append date" id="datePicker">
                                <asp:TextBox ID="txtFechRenuncia" class="form-control datepicker" DataFormatString="{0:dd/MM/yyyy}"
                                    runat="server"  ReadOnly="True"></asp:TextBox>
                               <%-- <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>--%>
                                <%--</span>--%>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label" for="focusedInput">Motivo</label>
                            <asp:TextBox ID="lblmotivo" Visible="false" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                            <asp:DropDownList class="form-control" ID="ddlMotivo" runat="server" OnSelectedIndexChanged="ddlMotivo_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnProcLiq" Class="btn btn-danger" Visible="false" Style="margin-top: 28px;" runat="server" Text="Procesar" OnClick="btnProcLiq_Click" />
                            <%--<asp:Button ID="btnProcLiq" Class="btn btn-danger" Visible="false" Style="margin-top: 28px;" runat="server" Text="procesar" OnClick="btnProcLiq_Click" />--%>
                        </div>
                      
                        <div class="col-md-2" style="margin-left: -70px;">
                            <%--<asp:Button ID="btnCalcLiq" Class="btn btn-info" Style="margin-top: 28px;" Visible="false" runat="server" Text="Calcular" />--%>
                            <asp:Button ID="Btnrprint" Class="btn btn-info" Style="margin-top: 28px;" runat="server" Text="Reimprimir" OnClick="Btnrprint_Click" />
                        </div>
                        
                        
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="GVLiquidacion" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            >
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
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
                               
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12">
                    
                       
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Salario Mensual </label>
                            <asp:TextBox ID="txtSalMens" class="form-control" ReadOnly="true"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Salario Mayor </label>
                            <asp:TextBox ID="txtSalMayor" class="form-control" ReadOnly="true"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Salario Promedio </label>
                            <asp:TextBox ID="txtSalProm" class="form-control" ReadOnly="true"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                         <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Dias Indemnizacion </label>
                            <asp:TextBox ID="txtDiasLab" class="form-control" ReadOnly="true"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Indemnizacion </label>
                            <asp:TextBox ID="txtIndemnizacion" class="form-control" ReadOnly="false"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off" AutoPostBack="True" OnTextChanged="txtIndemnizacion_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <br />

                    <div class="col-md-12" style="margin-top: 20px;">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Dias Aguinaldo </label>
                            <asp:TextBox ID="txtDiasAguinaldo" class="form-control" ReadOnly="true"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Aguinaldo</label>
                            <asp:TextBox ID="txtAguinaldo" class="form-control" ReadOnly="true"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Dias Vacaciones </label>
                            <asp:TextBox ID="txtDiasVac" class="form-control" ReadOnly="true"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Vacaciones A Pagar </label>
                            <asp:TextBox ID="txtTotalVac" class="form-control" ReadOnly="true"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                       <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Sub Total</label>
                            <asp:TextBox ID="txtSubTotal" class="form-control" ReadOnly="true"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-12" style="margin-top: 20px;">
                         
                        <div class="col-md-2">
                             <label class="control-label" for="focusedInput">INSS</label>
                            <asp:TextBox ID="TxtINSS" class="form-control" ReadOnly="true"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off" ></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                             <label class="control-label" for="focusedInput">IR</label>
                            <asp:TextBox ID="TxtIR" class="form-control" ReadOnly="true"
                                runat="server" autocomplete="off" ></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                           <label class="control-label" for="focusedInput">Total A Recibir</label>
                            <asp:TextBox ID="txtNetoRecib" class="form-control" ReadOnly="true"
                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12 ">
                                    <label class="control-label" for="focusedInput">Observacion</label>
                                    <asp:TextBox ID="txtObservEmpl" class="form-control"
                                        runat="server" autocomplete="off"></asp:TextBox>
                                </div>
                    <asp:HiddenField runat="server" ID="hfIngresoMayorMes" />
                    <asp:HiddenField runat="server" ID="hfIngresoProMes" />
                 
                    <div class="row" visible="true" id="Div1" runat="server">
                        <div class="col-md-12" style="margin-top: 25px;">
                            <fieldset>
                                <legend>Cargos Pendientes:</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Tipo de Pago</label>
                                            <asp:DropDownList class="form-control" ID="ddlTipoPago" runat="server">
                                                <%-- <asp:ListItem Value="1">Pago por Hora</asp:ListItem>
                                                <asp:ListItem Value="2">Pago por Dia</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Tiempo Pendiente</label>
                                            <asp:TextBox ID="txtTiempoPend" class="form-control" ReadOnly="false"
                                                runat="server" autocomplete="off" AutoPostBack="True" OnTextChanged="txtHorasp_TextChanged1" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Salario Pendiente</label>
                                            <asp:TextBox ID="txtSalPend" class="form-control" ReadOnly="True"
                                                runat="server" onkeypress="return soloNumeros(event)" autocomplete="off" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Concepto</label>
                                            <asp:DropDownList class="form-control" ID="ddlTipoConcepto" runat="server" OnSelectedIndexChanged="ddlTipoConcepto_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Value="0">_</asp:ListItem>
                                                <asp:ListItem Value="1">Ingreso</asp:ListItem>
                                                <asp:ListItem Value="2">Deduccion</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Tipo de Concepto</label>
                                            <asp:DropDownList class="form-control" ID="ddlTipo" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Total</label>
                                            <asp:TextBox ID="txtTotal" class="form-control" runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnAgregar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>

                    <div class="row" id="ingDed" runat="server">
                        <div class="col-md-12" style="margin-top: 25px;">
                            <div class="col-md-6">
                                <h3>Ingresos</h3>
                                <asp:Label class="control-label" ID="LblDetalleIngreso" runat="server" Text="Label">.</asp:Label>
                                <asp:Label class="control-label" ID="LblIngresosExtra" runat="server" Text="Label" ForeColor="Blue">.</asp:Label>
                                <asp:GridView ID="GVIngresos" class="table table-striped table-hover" runat="server"
                                    AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                                    BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                                    >
                                    <AlternatingRowStyle Width="100px" />
                                    <Columns>
                                        <asp:BoundField DataField="Id" HeaderText="Codigo" />
                                        <asp:BoundField DataField="Ingreso" HeaderText="Ingreso" ConvertEmptyStringToNull="False">
                                            <ItemStyle Width="20px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total" HeaderText="Total" ConvertEmptyStringToNull="False">
                                            <ItemStyle Width="20px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-md-6">
                                <h3>Deducciones</h3>
                                <asp:Label class="control-label" ID="lblDetallePrestamo" runat="server" Text="Label">.</asp:Label>
                                <asp:Label class="control-label" ID="lbldebe" runat="server" Text="." ForeColor="Blue"></asp:Label>
                                <%--<h2>Detalle de Prestamos:</h2>--%>
                                <asp:GridView ID="GvEgresos" class="table table-striped table-hover" runat="server"
                                    AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical" DataKeyNames="id,tipoDeduc"
                                    BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                                    >
                                    <AlternatingRowStyle Width="100px" />
                                    <Columns>
                                         <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="True" Visible="false">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="tipoDeduc" HeaderText="TipoDeduc" ReadOnly="True" Visible="false">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="deduccionNombre" HeaderText="Deduccion" ConvertEmptyStringToNull="False">
                                            <ItemStyle Width="20px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total" HeaderText="Total" ConvertEmptyStringToNull="False">
                                            <ItemStyle Width="20px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>


                </div>
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

