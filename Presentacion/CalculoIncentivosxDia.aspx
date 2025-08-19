<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CalculoIncentivosxDia.aspx.cs" Inherits="NominaRRHH.Presentacion.CalculoIncentivosxDia" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


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
                    <div class="col-md-2">
                        <label class="control-label" for="focusedInput">Corte Aprobacion</label>
                        <div class="input-group input-append date" id="datePicker3">
                            <asp:TextBox ID="TxtCorteAprobacion" class="form-control datepicker"
                                runat="server"></asp:TextBox>
                            <span class="input-group-addon add-on borderCalendar"><span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <asp:Label class="control-label" for="focusedInput" ID="Label3" runat="server" Text="Periodo" Style="font-weight: 700"></asp:Label>
                        <asp:TextBox ID="txtPeriodo" class="form-control" placeholder="Digite un Periodo" AutoPostBack="true" OnTextChanged="txtPeriodo_TextChanged"
                            runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                <div class="col-md-2">
                <asp:Label class="control-label" for="focusedInput" ID="Label4" runat="server" Text="Dias a Pagar" Style="font-weight: 700"></asp:Label>
                <asp:TextBox ID="txtDiasaPagar" class="form-control" placeholder="Dias a Pagar" Text="5" runat="server" autocomplete="off"></asp:TextBox>
                </div>  
                    <div class="col-md-1">
                        <asp:Label class="control-label" for="focusedInput" ID="lblSemana" runat="server" Text="Semana" Style="font-weight: 700" Visible="False"></asp:Label>
                        <asp:DropDownList class="form-control" ID="ddlTipo" runat="server" Visible="False">
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                        </asp:DropDownList>
                    </div>


                </div>
                <br>
                <div class="row">

                
                    <div class="col-md-3" runat="server" id="divGen" visible="false">
                       <asp:Button ID="BtnGenerar" Class="btn btn-success" runat="server" Text="Generar" OnClick="BtnGenerar_Click" />
                    </div>
                    <div class="col-md-3" runat="server" id="divApl" visible="false">
                        <asp:Button ID="btAlicarPlanilla" Class="btn btn-success" runat="server" Text="Aplicar" OnClick="btAlicarPlanilla_Click" />
                    </div>
                        <div class="col-md-3">
                        <asp:Button ID="BtnConsultar1" Class="btn btn-success" runat="server" Text="Consultar" OnClick="BtnConsultar1_Click" />
                    </div>
                </div>

                <asp:Panel ID="panelID" runat="server" Visible="false">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:CheckBox ID="ChkCargarProtecciones" runat="server" AutoPostBack="True" Text="Cargar Protecciones" OnCheckedChanged="ChkCargarProtecciones_CheckedChanged" />

                        </div>
                        <div class="col-md-12" runat="server" id="divFile" visible="false">
                            <div class="col-md-5">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cargar Achivo:  "></asp:Label>
                                <asp:FileUpload ID="fileProtectedDz" runat="server" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="BtnProcesar" Class="btn btn-success" runat="server" Text="Procesar" OnClick="BtnProcesar_Click" />
                            </div>


                            <div class="col-md-12" runat="server" id="divGrid" visible="false">
                                <asp:GridView ID="gvINGDD" class="table table-striped table-hover" runat="server"
                                    AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                                    BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                                    AllowPaging="True" AllowSorting="True" HorizontalAlign="Center" PageSize="30">
                                    <Columns>
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                        <asp:BoundField DataField="modulo" HeaderText="Modulo" />
                                        <asp:BoundField DataField="docenas" HeaderText="DZ" />
                                        <asp:BoundField DataField="observacion" HeaderText="Observación" />
                                        <asp:BoundField DataField="area" HeaderText="Area" />
                                    </Columns>
                                    <EmptyDataRowStyle BorderColor="Blue" ForeColor="Black" />
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#128f76" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>


                            </div>
                        </div>



                    </div>
                </asp:Panel>
                <br />
                <div class="row" runat="server" visible="false">

                    <div class="col-md-3">
                        <div class="col-md-3 col-sm-3 col-xs-5">
                            <asp:Button ID="BtnDetalleModulos" Class="btn btn-success" runat="server" Text="Detalle Modulos" OnClick="BtnDetalleModulos_Click" Visible="false" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-3 col-sm-3 col-xs-5">
                            <asp:Button ID="BtnDetalleEmpleados" Class="btn btn-success" runat="server" Text="Detalle Empleado" OnClick="BtnDetalleEmpleados_Click" Visible="false" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-3 col-sm-3 col-xs-5">
                            <asp:Button ID="BtnIncentivoTotal" Class="btn btn-success" runat="server" Text="Incentivo Total" OnClick="BtnIncentivoTotal_Click" Visible="false" />
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-3 col-sm-3 col-xs-5">
                            <asp:Button ID="BtnPendPago" Class="btn btn-success" runat="server" Text="Pendiente Pago" OnClick="BtnPendPago_Click" Visible="false" />
                        </div>
                    </div>



                </div>
                <div class="row" runat="server" id="divrpt" visible="false">
                    <div class="col-md-12 paddingContentTabs">


                        <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Reporteria:</label>
                            <asp:DropDownList class="form-control" ID="ddlReporteria" runat="server">
                                <asp:ListItem Value="1">Detalle Modulos</asp:ListItem>
                                 <asp:ListItem Value="5">Cumplimiento Semanal</asp:ListItem>
                                    <asp:ListItem Value="3" Selected="True">Incentivo Total</asp:ListItem>
                                  <asp:ListItem Value="4">Incentivo Pend.</asp:ListItem>
                                <asp:ListItem Value="2">Detalle Empleados</asp:ListItem>
                            
                              
                               
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="BtnPrint" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Imprimir" OnClick="BtnPrint_Click" />
                            <asp:Button ID="BtnAutorizarPagosPendientes" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Autorizar Pagos Pendientes" OnClick="BtnAutorizarPagosPendientes_Click" />
                        </div>


                    </div>
                </div>
                              
                <div class="row" runat="server" id="div5" visible="false">
                    <div class="col-md-12 paddingContentTabs">
                        <div class="col-md-2">
                            <asp:Label class="control-label" for="focusedInput" ID="Label1" runat="server" Text="Codigo" Style="font-weight: 700"></asp:Label>
                            <asp:TextBox ID="TxtCodigo" class="form-control" placeholder="Digite un Codigo"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-3" style="margin-top: 23px;">
                            <div class="col-md-3 col-sm-3 col-xs-5">
                                <asp:Button ID="BtnConsultar2" Class="btn btn-success" runat="server" Text="Consultar" OnClick="BtnConsultar2_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            

            </div>


            <br />
            <div class="row" visible="false" runat="server" id="div1">
                <label class="control-label" for="focusedInput">Produccion por Modulo</label>
                <rsweb:ReportViewer ID="ReportViewer3" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                </rsweb:ReportViewer>
            </div>
            <div class="row" visible="false" runat="server" id="div2">
                <label class="control-label" for="focusedInput">Detalle Empleado por Dia</label>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                </rsweb:ReportViewer>
            </div>
            <div class="row" visible="false" runat="server" id="div3">
                <label class="control-label" for="focusedInput">Incentivo total</label>
                <rsweb:ReportViewer ID="ReportViewer2" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                </rsweb:ReportViewer>
            </div>
            <div class="row" visible="false" runat="server" id="div4">
                <label class="control-label" for="focusedInput">Desgloce pago por Empleado</label>
                <rsweb:ReportViewer ID="ReportViewer4" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                </rsweb:ReportViewer>
            </div>
            <div class="row" visible="false" runat="server" id="div6">
                <label class="control-label" for="focusedInput">Pendiente de pago</label>
                <rsweb:ReportViewer ID="ReportViewer5" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                </rsweb:ReportViewer>
            </div>
            <div class="row" visible="false" runat="server" id="div7">
                <label class="control-label" for="focusedInput">Cumplimiento Semanal</label>
                <rsweb:ReportViewer ID="ReportViewer6" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                </rsweb:ReportViewer>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        $(function () {            

            $('#datePicker3').datepicker({
                format: 'dd/mm/yyyy'
            })
                .on('changeDate', function (e) {
                    $('.datepicker3 dropdown-menu').hide();
                });
        });
    </script>
</asp:Content>

