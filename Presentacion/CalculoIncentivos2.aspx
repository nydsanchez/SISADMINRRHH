<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CalculoIncentivos2.aspx.cs" Inherits="NominaRRHH.CalculoIncentivos2" %>

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
                    <div class="col-md-3">
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
                    </div>
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
                        </div>
                    </div>

                </div>
                <br />
                <br />
                <div class="row">

                    <div class="col-md-5" runat="server">
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cargar Achivo:  "></asp:Label>
                        <asp:FileUpload ID="fuIngr" runat="server" />
                        <asp:Button ID="btnCargarID" Class="btn btn-success" runat="server" Text="Procesar Archivo" OnClick="btnCargarID_Click" />

                    </div>
                    <div class="col-md-3" runat="server">

                        <div class="col-md-3 col-sm-3 col-xs-5">
                            <asp:Button ID="Button1" Class="btn btn-success" runat="server" Text="Aplicar Penalizaciones a Incentivos" OnClick="Button1_Click" />
                        </div>
                    </div>
                    <br />
                    <br />

                </div>

                <div class="row">
                    <asp:Panel ID="panelinc" runat="server">
                        <asp:Label ID="Label1" runat="server" Text="INDICAR EL TIPO DE INGRESO: INCENTIVO , PROTEGIDO , RECLAMOS" Font-Bold="True"></asp:Label>
                        <br />
                        <asp:Label ID="Label2" runat="server" Text="ADEMAS DE INDICAR EL TIPO DE INGRESO, DEBE EXPLICAR EL MOTIVO EN LA COLUMNA " Font-Bold="True"></asp:Label>
                        <asp:Label ID="Label5" runat="server" Text="COMENTARIO" Font-Bold="True" ForeColor="Red"></asp:Label>
                        <div class="col-md-10" runat="server" id="divIDGrid">
                            <asp:GridView ID="gvING" class="table table-striped table-hover" runat="server"
                                AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                                BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                                AllowPaging="True" AllowSorting="True" HorizontalAlign="Center" PageSize="30" OnPageIndexChanging="gvING_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="codigoEmpleado" HeaderText="Codigo" />
                                    <asp:BoundField DataField="idtipo" HeaderText="TipoIngreso" />
                                    <asp:BoundField DataField="valor" HeaderText="Cantidad Incentivo" />
                                    <asp:BoundField DataField="Comentario" HeaderText="Comentario" />
                                    <asp:BoundField DataField="TipoIngreso" HeaderText="Tipo Ingreso" />
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
                    </asp:Panel>
                </div>
                <div class="row">
                    <div class="col-md-3">
                    </div>
                </div>


                <br />
                <br />
                <br />
                <div class="row">
                    <div class="col-md-3">
                        <div class="col-md-3 col-sm-3 col-xs-5">
                            <%--<asp:Button ID="btAlicarPlanilla" Class="btn btn-success" runat="server" Text="APLICAR PLANILLA A PAGO" OnClick="btAlicarPlanilla_Click" />--%>
                        </div>
                    </div>


                </div>

            </div>


            <br />
            <br />


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


