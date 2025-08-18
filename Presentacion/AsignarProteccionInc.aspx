<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsignarProteccionInc.aspx.cs" Inherits="NominaRRHH.Presentacion.AsignarProteccionInc" MaintainScrollPositionOnPostback="true" %>

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
                    <div class="col-md-12">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#Modulo" data-toggle="tab" aria-expanded="true">Modulo</a></li>
                            <li class=""><a href="#Individual" data-toggle="tab" aria-expanded="false">Individual</a></li>
                        </ul>
                        <div id="myTabContent" class="tab-content paddingTab">
                            <div class="tab-pane fade active in" id="Modulo">
                                <div class="row">
                                    <div class="col-md-12 ">
                                        <asp:Panel ID="panelID" runat="server">
                                            <div class="row">
                                                <asp:Label class="control-label" for="focusedInput" ID="Label1" runat="server" Text="Protecciones por Modulo" Style="font-weight: 700"></asp:Label>

                                                <div class="col-md-12" runat="server" id="divFile">

                                                    <div class="col-md-5">
                                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cargar Achivo:  "></asp:Label>
                                                        <asp:FileUpload ID="fileProtectedDz" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Button ID="Button6" Class="btn btn-success" runat="server" Text="Procesar" OnClick="Button6_Click"/>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Button ID="Button1" Class="btn btn-success" runat="server" Text="Guardar" OnClick="Button1_Click" Visible="false" />
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-8" runat="server" id="divGrid">
                                                    <asp:GridView ID="gvINGDD" class="table table-striped table-hover" runat="server"
                                                        AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                                                        BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" HorizontalAlign="Center" PageSize="30">
                                                        <Columns>
                                                            <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                                            <asp:BoundField DataField="modulo" HeaderText="Modulo" />
                                                            <asp:BoundField DataField="problema" HeaderText="Problema" />
                                                            <asp:BoundField DataField="dz" HeaderText="DZ" />
                                                            <asp:BoundField DataField="observacion" HeaderText="Observación" />

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

                                            <div class="col-md-3">
                                                <div class="col-md-3 col-sm-3 col-xs-5">
                                                    <asp:Button ID="Button4" Class="btn btn-success" runat="server" Text="Consultar" OnClick="Button4_Click" />
                                                </div>
                                            </div>


                                        </div>
                                        <br />
                                        <div class="row" visible="false" runat="server" id="div2">
                                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                                            </rsweb:ReportViewer>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="tab-pane fade" id="Individual">

                                <div class="row">
                                    <asp:Label class="control-label" for="focusedInput" ID="Label3" runat="server" Text="Protecciones Individuales" Style="font-weight: 700"></asp:Label>

                                    <div class="col-md-12 ">
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Codigo</label>
                                            <asp:TextBox ID="txtCodigo" class="form-control" autocomplete="off" TabIndex="4"
                                                runat="server" OnTextChanged="txtCodigo_TextChanged" AutoPostBack="true" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label class="control-label" for="focusedInput">Nombre</label>
                                            <asp:TextBox ID="TxtNombreE" class="form-control"
                                                runat="server" autocomplete="off" ReadOnly="true" TabIndex="5"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Asistencia</label>
                                            <asp:TextBox ID="txtAsistencia" class="form-control"
                                                runat="server" autocomplete="off" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Viatico</label>
                                            <asp:TextBox ID="txtViatico" class="form-control"
                                                runat="server" autocomplete="off" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>

                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Porcentaje</label>
                                            <asp:TextBox ID="txtPorcentaje" class="form-control"
                                                runat="server" autocomplete="off" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12">


                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Semanas</label>
                                            <asp:TextBox ID="txtRepeticiones" class="form-control"
                                                runat="server" autocomplete="off" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="ChkRecurrente" runat="server" />
                                                        <strong>Recurrente</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="ChkActivo" runat="server" Checked="true" />
                                                        <strong>Activo</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnAgregarDed" Class="btn btn-info" Style="margin-top: 22px;" runat="server" Text="Guardar" OnClick="btnAgregarDed_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-md-12 ">
                                        <div class="col-md-1">
                                            <label class="control-label" for="focusedInput">Periodo</label>
                                            <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" autocomplete="off" TabIndex="2" onkeypress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="Button2" Class="btn btn-success" runat="server" Text="Buscar" OnClick="Button2_Click" Style="margin-top: 22px;"/>


                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row" visible="false" runat="server" id="div1">
                                    <rsweb:ReportViewer ID="ReportViewer2" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
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

