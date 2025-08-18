<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CalculoIncentivos.aspx.cs" Inherits="NominaRRHH.Presentacion.CalculoIncentivos" MaintainScrollPositionOnPostback="true" %>

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
                       <%-- <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Fecha Inicio</label>
                            <div class="input-group input-append date" id="datePicker">
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
                        </div>--%>
                        <div class="col-md-2">
                            <asp:Label class="control-label" for="focusedInput" ID="Label3" runat="server" Text="Periodo" Style="font-weight: 700"></asp:Label>
                            <asp:TextBox ID="txtPeriodo" class="form-control" placeholder="Digite un Periodo"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-1">

                            <asp:Label class="control-label" for="focusedInput" ID="lblSemana" runat="server" Text="Semana" Style="font-weight: 700"></asp:Label>
                            <asp:DropDownList class="form-control" ID="ddlTipo" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <div class="col-md-3 col-sm-3 col-xs-5">
                                <asp:CheckBox ID="cbSeleccion" runat="server" AutoPostBack="True" OnCheckedChanged="cbSeleccion_CheckedChanged" Text="Cargar Otros Archivos" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="col-md-3 col-sm-3 col-xs-5">
                                <asp:Button ID="Button1" Class="btn btn-success" runat="server" Text="Generar Planilla Incentivos" OnClick="Button1_Click" />
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-md-3">
                            <div class="col-md-3 col-sm-3 col-xs-5">
                                <asp:Button ID="btAlicarPlanilla" Class="btn btn-success" runat="server" Text="APLICAR PLANILLA A PAGO" OnClick="btAlicarPlanilla_Click" />
                            </div>
                        </div>


                    </div>

                </div>
                <asp:Panel ID="panelDZ" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="Label5" runat="server" Text="CARGUE EL ARCHIVO CON LAS DOCENAS QUE SE SERAN TRANSFERIDAS ENTRE COLABORADORES" ForeColor="#3333FF" Font-Bold="True"></asp:Label>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-5">
                            <asp:CheckBox ID="cbDedDZ" runat="server" AutoPostBack="True" OnCheckedChanged="cbDedDZ_CheckedChanged" Text="Cargar Archivo con Deducciones de DZ" />

                        </div>
                        <div class="col-md-5" runat="server" id="divDDZ">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cargar Achivo:  "></asp:Label>
                            <asp:FileUpload ID="fuDDZ" runat="server" />
                            <asp:Button ID="btProcesarDZ" Class="btn btn-success" runat="server" Text="Procesar Archivo" OnClick="btProcesarDZ_Click" />

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-10" runat="server" id="divDDZGrid">
                            <asp:GridView ID="gvReajusteDZ" class="table table-striped table-hover" runat="server"
                                AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                                BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                                AllowPaging="True" AllowSorting="True" HorizontalAlign="Center" PageSize="50" OnPageIndexChanging="gvReajusteDZ_PageIndexChanging" OnRowDataBound="gvReajusteDZ_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="Codigosumar" HeaderText="CodigoSumar" />
                                    <asp:BoundField DataField="Codigorestar" HeaderText="CodigoRestar" />
                                    <asp:BoundField DataField="DZ" HeaderText="DZ" />
                                    <%-- <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:yyyy-MM-dd}" />--%>
                                    <asp:BoundField DataField="observacion" HeaderText="Observación" />
                                    <asp:BoundField DataField="Comentario" HeaderText="comentario" />
                                    <asp:BoundField DataField="Aprobado" HeaderText="Aprobado para Procesar" />

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
                </asp:Panel>
                <asp:Panel ID="panelID" runat="server">
                    <div class="row">
                        <div class="col-md-5">
                            <asp:CheckBox ID="cbIngDed" runat="server" AutoPostBack="True" OnCheckedChanged="cbIngDed_CheckedChanged" Text="Cargar Archivo deducciones" />

                        </div>
                        <div class="col-md-5" runat="server" id="divID">
                            <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cargar Achivo:  "></asp:Label>
                            <asp:FileUpload ID="fuIngrDed" runat="server" />
                            <asp:Button ID="btnCargarID" Class="btn btn-success" runat="server" Text="Procesar Archivo" OnClick="btnCargarID_Click" />

                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-10" runat="server" id="divIDGrid">
                            <asp:GridView ID="gvINGDD" class="table table-striped table-hover" runat="server"
                                AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                                BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                                AllowPaging="True" AllowSorting="True" HorizontalAlign="Center" PageSize="30">
                                <Columns>
                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                    <asp:BoundField DataField="valor" HeaderText="Deducción" />
                                    <%--       <asp:BoundField DataField="TipoIDD" HeaderText="Tipo Ingreso o Deduccion" />--%>
                                    <asp:BoundField DataField="TipoCal" HeaderText="Tipo Cálculo" />
                                    <%--          <asp:BoundField DataField="Valor" HeaderText="Valor" />--%>
                                    <asp:BoundField DataField="observacion" HeaderText="Observación" />
                                    <%--    <asp:BoundField DataField="Comentario" HeaderText="Comentario" />--%>
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
                </asp:Panel>

                <asp:Panel ID="pnlAQL" runat="server">
                    <div class="row">
                        <div class="col-md-5">
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="Cargar Archivo AQL" OnCheckedChanged="CheckBox1_CheckedChanged" />

                        </div>
                        <div class="col-md-5" runat="server" id="divAQL">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cargar Achivo:  "></asp:Label>
                            <asp:FileUpload ID="fuAQL" runat="server" />
                            <asp:Button ID="Button2" Class="btn btn-success" runat="server" Text="Procesar Archivo" OnClick="Button2_Click" />

                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-10" runat="server" id="divAQLGrid">
                            <asp:GridView ID="gvAQL" class="table table-striped table-hover" runat="server"
                                AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                                BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                                AllowPaging="True" AllowSorting="True" HorizontalAlign="Center" PageSize="30">
                                <Columns>
                                    <asp:BoundField DataField="Modulo" HeaderText="Modulo" />
                                    <asp:BoundField DataField="AQL" HeaderText="% AQL" />
                                    <asp:BoundField DataField="AQLMeta" HeaderText="% AQL META" />
                                    <asp:BoundField DataField="Total" HeaderText="% TOTAL" />
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
                </asp:Panel>


                <%-- <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:yyyy-MM-dd}" />--%>


                <br />

                <div class="form-group row">
                    <div class="col-md-12">
                        <div class="form-group col-sm-offset-2 col-xs-offset-2 col-md-offset-2 col-md-2 col-sm-2 col-xs-2">
                            <div class="col-md-3 col-sm-3 col-xs-5">
                                <%--       <asp:BoundField DataField="TipoIDD" HeaderText="Tipo Ingreso o Deduccion" />--%>
                            </div>
                        </div>

                    </div>
                </div>
                <br />
                <br />

                <asp:Panel ID="pnlDZ" runat="server">
                    <div class="form-group row">
                        <div class="col-md-12">
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-12">
                        </div>
                    </div>
                    <div class="form-group row">
                    </div>
                </asp:Panel>

                <br />
                <div class="row">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                    </rsweb:ReportViewer>
                </div>
                <div class="row">
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

            $('#datePicker2').datepicker({
                format: 'dd/mm/yyyy'
            })
                .on('changeDate', function (e) {
                    $('.datepicker2 dropdown-menu').hide();
                });

        });
    </script>
</asp:Content>

