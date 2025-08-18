<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsignacionDeduccionesConfig.aspx.cs" Inherits="NominaRRHH.Presentacion.AsignacionDeduccionesConfig" %>

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
                 <div class="row">
                        <div class="col-md-12">
                            <%--<div class="col-md-3">
                                <label class="control-label" for="focusedInput">Ubicacion</label>
                                <asp:DropDownList class="form-control" ID="ddlUbicacion" runat="server">
                                </asp:DropDownList>
                            </div>--%>
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Asignacion por</label>
                                <asp:DropDownList class="form-control" ID="ddlAsigPerm" runat="server" OnSelectedIndexChanged="ddlAsigPerm_SelectedIndexChanged" AutoPostBack="True">
                                    <%--<asp:ListItem Value="1" Selected="True"> Individual</asp:ListItem>--%>
                                    <asp:ListItem Value="2" Selected="True"> General</asp:ListItem>
                                    <asp:ListItem Value="1"> Por Departamento</asp:ListItem>                                    
                                    <asp:ListItem Value="3"> Ubicacion</asp:ListItem>
                                    <asp:ListItem Value="4"> Deptos x Ubicacion</asp:ListItem>
                                </asp:DropDownList>
                            </div>                                     
                            <div class="col-md-2" runat="server" id="divubic" visible="false">
                                <label class="control-label" for="focusedInput">Ubicacion</label>
                                <asp:DropDownList class="form-control" ID="ddlubicacion" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3" runat="server" id="divproceso" visible="false">
                                <label class="control-label" for="focusedInput">Proceso</label>
                                <asp:DropDownList class="form-control" ID="ddlProceso" runat="server">
                                </asp:DropDownList>
                            </div>
             <%--                <div class="col-md-2">
                            <div class="col-md-1 marginChkActivo">
                                <div class="checkbox">
                                    <label class="control-label" for="focusedInput">
                                        <asp:CheckBox ID="ckembargos" runat="server" Checked="true" />
                                        <strong>Excluir Embargos</strong>
                                    </label>
                                </div>
                            </div>
                        </div>
                             <div class="col-md-2">
                            <div class="col-md-1 marginChkActivo">
                                <div class="checkbox">
                                    <label class="control-label" for="focusedInput">
                                        <asp:CheckBox ID="ckefectivo" runat="server" Checked="true" />
                                        <strong>Excluir Efectivo</strong>
                                    </label>
                                </div>
                            </div>
                        </div>--%>
                           
                        </div>
                    </div>
                <br />
                 <div class="row">
                                <div class="col-md-12 paddingContentTabs">
                                   <%--       <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Codigo</label>
                                            <asp:TextBox ID="txtcodigoAsig" class="form-control"
                                                runat="server" autocomplete="off" OnTextChanged="txtcodigoAsig_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Nombre</label>
                                            <asp:TextBox ID="TxtNombreE" class="form-control" ReadOnly="true"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>--%>
                                        <div id="editarhide2" runat="server">
                        <div class="col-md-1" runat ="server" >
                            <label class="control-label" for="focusedInput">Periodo</label>
                            <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                         </div>
                                    <div id="editarhide" runat="server">
                                         <div class="col-md-3" runat ="server" id="divTipoDeduc">
                                            <label class="control-label" for="focusedInput">Tipo Deducciones</label>
                                            <asp:DropDownList class="form-control" ID="ddlTipDeduc" runat="server" >
                                            </asp:DropDownList>
                                        </div>
                          
                                        </div>                               
                               
                       
                        <div class="col-md-2"  runat ="server" >
                            <label class="control-label" for="focusedInput">Total</label>
                            <asp:TextBox ID="txtTotal" class="form-control" runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                          
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Monto Cuota</label>
                            <asp:TextBox ID="txtCuotas" class="form-control" runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                        </div>
                         
                        <div id="editarhide3" runat="server">
                                <div class="col-md-2">
                            <div class="col-md-1 marginChkActivo">
                                <div class="checkbox">
                                    <label class="control-label" for="focusedInput">
                                        <asp:CheckBox ID="chkPorc" runat="server" />
                                        <strong>Porcentual</strong>
                                    </label>
                                </div>
                            </div>
                        </div>
                             <div class="col-md-2">
                            <div class="col-md-1 marginChkActivo">
                                <div class="checkbox">
                                    <label class="control-label" for="focusedInput">
                                        <asp:CheckBox ID="chkRecurrente" runat="server" />
                                        <strong>Recurrente</strong>
                                    </label>
                                </div>
                            </div>
                        </div>
                          <%--                     <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Fecha Solicitud</label>
                                        <div class="input-group input-append date" id="datePicker2">
                                            <asp:TextBox ID="txtFecSol" class="form-control datepicker" ReadOnly="true"
                                                runat="server"></asp:TextBox>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <label class="control-label" for="focusedInput">Fecha Autorizacion</label>
                                        <div class="input-group input-append date" id="datePicker3">
                                            <asp:TextBox ID="txtFecAut" class="form-control datepicker" ReadOnly="true"
                                                runat="server"></asp:TextBox>
                                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>  --%>

                                </div>
                     
                    </div>
                </div>
                 <div class="row">
             
                    <div class="col-md-12">
                        <%--<div id="editarhide4" runat="server">
                         <div class="col-md-2">
                            <div class="col-md-1 marginChkActivo">
                                <div class="checkbox">
                                    <label class="control-label" for="focusedInput">
                                        <asp:CheckBox ID="chkvalidez" runat="server" />
                                        <strong>Periodo Validez</strong>
                                    </label>
                                </div>
                            </div>
                        </div>
                                <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Fecha Activacion</label>
                            <div class="input-group input-append date" id="datePicker5">
                                <asp:TextBox ID="txtFecha" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2" runat="server" id="calendarFin">
                            <label class="control-label" for="focusedInput">Fecha Expira</label>
                            <div class="input-group input-append date" id="datePicker4">
                                <asp:TextBox ID="TxtFecha2" class="form-control datepicker"
                                    runat="server"></asp:TextBox>
                                <span runat="server" id="borcalendar" class="input-group-addon add-on borderCalendar"><span runat="server" id="calendar" class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                        </div>
                            </div>--%>
                             <div runat="server" id="editarhide5">
                           <div class="col-md-1">
                            <asp:Button ID="btnGuardar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Asignar" OnClick="btnGuardar_Click" />
                           
                        </div>
                                 <%--<div class="col-md-1">
                                      <asp:Button ID="btnEditar" Class="btn btn-success" Style="margin-top: 22px;" runat="server"  Text="Editar" OnClick="btnEditar_Click"  />
                                     </div>--%>
                        </div>
                        </div>
                     </div>
              
              <%--  <br />
                  <div class="row">
                        <div class="col-md-12">

                             <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Neto Promedio Recibido por Periodo</label>
                            <asp:TextBox ID="TxtNetoProm" class="form-control" ReadOnly="true"
                                runat="server" autocomplete="off" ></asp:TextBox>
                        </div>
                             
                        </div>
                    </div>  
                <br />
                <fieldset>
                    <legend></legend>
                    <label class="control-label" for="focusedInput">Ingresos y Deducciones Acumuladas a la Fecha</label>
                 <div class="row" id="AdIndemnizacion" runat="server" style="margin-top:10px;">
                     <div class="col-md-12">
             
                    <div class="row">
                        <div class="col-md-12">

                             <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Indemnizacion</label>
                            <asp:TextBox ID="txtIndemnizacion" class="form-control" ReadOnly="true"
                                runat="server" autocomplete="off" ></asp:TextBox>
                        </div>
                              <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Aguinaldo</label>
                            <asp:TextBox ID="txtAguinaldo" class="form-control" ReadOnly="true"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                            <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Vacaciones</label>
                            <asp:TextBox ID="txtTotalVac" class="form-control" ReadOnly="true"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                             <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Saldo Total</label>
                            <asp:TextBox ID="txtSubTotal" class="form-control" ReadOnly="true"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                            <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Saldo Disponible</label>
                            <asp:TextBox ID="TxtSaldoDisp" class="form-control" ReadOnly="true"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        </div>
                    </div>                      
                         <br />
                          <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="GVDeducciones" class="table table-striped table-hover" runat="server" DataKeyNames="tipoDeduc"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical" ShowFooter="True" 
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="True" AllowSorting="True" OnRowDeleting="GVDeducciones_RowDeleting" OnSelectedIndexChanged="GVDeducciones_SelectedIndexChanged" OnRowDataBound="GVDeducciones_RowDataBound" OnPageIndexChanging="GVDeducciones_PageIndexChanging">
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
                              
                                <asp:BoundField DataField="deduccionNombre" HeaderText="Deduccion">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="valorCuotas" HeaderText="Valor Cuotas">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="Total">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Debe" HeaderText="Pendiente">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="porcentual" HeaderText="Porcentual">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="recurrente" HeaderText="Recurrente">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecreg" HeaderText="Registro">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fechaultimopago" HeaderText="Ultimo Pago">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fechaexpira" HeaderText="Fecha Expira">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="btnDeshabilitar" Class="btn btn-danger" Style="margin-top: 22px;" runat="server" Visible="false" Text="Deshabilitar" OnClick="btnDeshabilitar_Click" />
                    </div>
                </div>
                    <div class="row" style="margin-top:10px;">
                          <asp:GridView ID="GvEgresos" class="table table-striped table-hover" runat="server"
                                    AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                                    BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                                    AllowPaging="True" AllowSorting="True">
                                   
                                    <Columns>
                                        <asp:BoundField DataField="deduccionNombre" HeaderText="Deduccion" ConvertEmptyStringToNull="False">
                                            <ItemStyle Width="20px" HorizontalAlign="Left" />
                                        </asp:BoundField>                                          
                                        <asp:BoundField DataField="total" HeaderText="Total" ConvertEmptyStringToNull="False">
                                            <ItemStyle Width="20px" HorizontalAlign="Left" />
                                        </asp:BoundField>                                 
                                    </Columns>
                                </asp:GridView>

                    </div>
                    </div>
                </div>
                    </fieldset>--%>
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

