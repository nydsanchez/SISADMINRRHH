<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VLiquidacionesEmp.aspx.cs" Inherits="NominaRRHH.VLiquidacionesEmp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/bootstrap-clockpicker.css" rel="stylesheet" />
    <link href="../Content/clockpicker.css" rel="stylesheet" />
    <link href="../Content/standalone.css" rel="stylesheet" />
    <link href="../Content/Styles.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    <script src="../Scripts/bootstrap-clockpicker.js"></script>
    <script src="../Scripts/jquery-clockpicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top">
        <div class="panel panel-info">
            <div class="panel-body" style="margin-top: 0px">
                <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="lblAlert" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server" visible="false">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="LblSuccess" runat="server" Visible="false"></asp:Label>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput"><strong>Detalle Liquidaciones</strong></label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <label class="control-label" for="focusedInput">Mostrar:</label>
                            <asp:RadioButtonList ID="rbl" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbl_SelectedIndexChanged">
                                <asp:ListItem Value="1" Selected="True">Por Fechas</asp:ListItem>
                                <asp:ListItem Value="2">Por empleado</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div id="divfec" runat="server" visible="true">
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

                        </div>
                        <div class="col-md-2" id="divcodigo" runat="server" visible="false">
                            <label class="control-label" for="focusedInput">Codigo</label>
                            <asp:TextBox ID="txtcodigo" class="form-control" placeholder="Digite un Codigo"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnMostrar" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Aceptar" OnClick="btnMostrar_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label runat="server" class="control-label">Liquidaciones</asp:Label>
                        <asp:GridView ID="gvIngresosEmp" class="table table-striped table-hover" runat="server" Style="width: 100%;"
                            AutoGenerateColumns="False" DataKeyNames="id,codigo,cerrada" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            OnRowCommand="gvIngresosEmp_RowCommand" OnRowDataBound="gvIngresosEmp_RowDataBound">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                <%-- <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                    <ControlStyle Width="5px" />
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:CommandField>--%>
                                <asp:BoundField DataField="codigo" HeaderText="Codigo">
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombrecompleto" HeaderText="Nombre">
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fechaingreso" HeaderText="Ingreso" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fechaegreso" HeaderText="Egreso" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="motivo" HeaderText="Motivo">
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Cerrar">
                                    <ItemTemplate>
                                        <asp:Button ID="btncerrar"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>
            "
                                            CommandName="cerrar" runat="server" Text="Cerrar" />
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Imprimir">
                                    <ItemTemplate>
                                        <asp:Button ID="btnImprimir"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>
            "
                                            CommandName="imprimir" runat="server" Text="Imprimir" />
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
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
        $(function () {
            $('#datePicker2').datepicker({
                format: 'dd/mm/yyyy'
            })
            .on('changeDate', function (e) {
                $('.datepicker dropdown-menu').hide();
            });
        });
    </script>
</asp:Content>
