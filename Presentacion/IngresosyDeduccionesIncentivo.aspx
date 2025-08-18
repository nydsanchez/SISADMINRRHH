<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IngresosyDeduccionesIncentivo.aspx.cs" Inherits="NominaRRHH.IngresosyDeduccionesIncentivo" %>

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

                <asp:Label class="control-label" for="focusedInput" ID="Label2" runat="server" Text="Ingresos y Deducciones VAT" Style="font-weight: 700"></asp:Label>

                <div class="row">

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
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#registro" data-toggle="tab" aria-expanded="true">Registro</a></li>
                            <li class=""><a href="#consulta" data-toggle="tab" aria-expanded="false">Consulta</a></li>
                        </ul>
                        <div id="myTabContent" class="tab-content paddingTab">
                            <div class="tab-pane fade active in" id="registro">
                                <div class="row">
                                    <div class="col-md-3">

                                        <asp:Label class="control-label" for="focusedInput" ID="Label1" runat="server" Text="Detalle permitido Tipo - Detalle" Style="font-weight: 700"></asp:Label>
                                        <asp:DropDownList class="form-control" ID="ddDetalle" runat="server">
                                            <asp:ListItem Value="DocenasAdicionales">1 - DocenasAdicionales</asp:ListItem>
                                            <asp:ListItem Value="OpCriticaYTransporte">1 - OpCriticaYTransporte</asp:ListItem>
                                            <asp:ListItem Value="ProduccionACompletar">1 - ProduccionACompletar</asp:ListItem>
                                            <asp:ListItem Value="ProduccionPendiente">1 - ProduccionPendiente</asp:ListItem>
                                            <asp:ListItem Value="BonoViernes">1 - BonoViernes</asp:ListItem>
                                            <asp:ListItem Value="DocenasMenos">2 - DocenasMenos</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <br />
                                <div class="row">

                                    <div class="col-md-5" runat="server">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cargar Achivo:  "></asp:Label>
                                        <asp:FileUpload ID="fuIngr" runat="server" />



                                    </div>
                                    <div class="col-md-3" runat="server">
                                        <asp:Button ID="btnCargarID" Class="btn btn-success" runat="server" Text="Cargar" OnClick="btnCargarID_Click" />

                                    </div>
                                    <div class="col-md-3" runat="server">
                                        <asp:Button ID="Button1" Class="btn btn-success" runat="server" Text="Guardar" OnClick="Button1_Click" />

                                    </div>


                                </div>

                                <div class="row">
                                    <asp:Panel ID="panelinc" runat="server">
                                        <br />
                                        <div class="col-md-10" runat="server" id="divIDGrid">
                                            <asp:GridView ID="gvING" class="table table-striped table-hover" runat="server"
                                                AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                                                BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                                                AllowPaging="True" AllowSorting="True" HorizontalAlign="Center" PageSize="20" OnPageIndexChanging="gvING_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                                                    <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                                                    <asp:BoundField DataField="detalle" HeaderText="Detalle" />
                                                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                                    <asp:BoundField DataField="valor" HeaderText="Valor" />
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
                            </div>
                            <div class="tab-pane fade" id="consulta">
                                <div class="row">

                                    <div class="col-md-3">
                                        <asp:Button ID="Button2" Class="btn btn-success" runat="server" Text="Mostrar Reporte" OnClick="Button2_Click" />


                                    </div>


                                </div>
                                <div class="row" runat="server" id="div2">

                                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                    </rsweb:ReportViewer>
                                </div>
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

            $('#datePicker2').datepicker({
                format: 'dd/mm/yyyy'
            })
                .on('changeDate', function (e) {
                    $('.datepicker2 dropdown-menu').hide();
                });

        });
    </script>
</asp:Content>


