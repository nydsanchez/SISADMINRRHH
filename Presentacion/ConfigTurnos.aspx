<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfigTurnos.aspx.cs" Inherits="NominaRRHH.Presentacion.ConfigTurnos" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.8.2.js"></script>
    <link href="../Content/Styles.css" rel="stylesheet" />
    <link href="../Content/bootstrap-switch.css" rel="stylesheet" />
    <link href="../Content/bootstrap-clockpicker.css" rel="stylesheet" />
    <link href="../Content/clockpicker.css" rel="stylesheet" />
    <link href="../Content/standalone.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-switch.js"></script>
    <script src="../Scripts/bootstrap-clockpicker.js"></script>
    <script src="../Scripts/jquery-clockpicker.js"></script>
    <script src="../Scripts/clockpicker.js"></script>
    <script src="../Scripts/jquery.maskedinput.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container mar-top">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <label class="control-label" for="focusedInput">TABLA DE TURNOS</label>
                    </div>
                </div>
                <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="LblSuccess" runat="server"></asp:Label>
                </div>
                <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server" >
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <asp:Label ID="lblAlert" runat="server"></asp:Label>
                </div>
                <div class="form-group row">
                    <div class="col-md-4">
                        <asp:ImageButton ID="ImageButton3" runat="server" Height="40px" ImageUrl="~/Images/agregar.png" ToolTip="NUEVO TURNO" Width="40px" OnClick="ImageButton1_Click" />
                    </div>
                </div>
                <asp:Panel ID="plnEditarT" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Nombre Turno</label>
                                <asp:TextBox ID="txtNombTurno" class="form-control"
                                    runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                             <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Horas Turno</label>
                                <asp:TextBox ID="TxtHrsTurno" class="form-control"
                                    runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Minutos Comodin</label>
                                <asp:TextBox ID="txtMinComodin" class="form-control"
                                    runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Horas Septimo</label>
                                <asp:TextBox ID="txtTotalHoras" class="form-control"
                                    runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                            </div>
                          <div class="col-md-1 marginChkActivo">
                                            <div class="checkbox">
                                                <label>
                                                    <asp:CheckBox ID="ChkConsolidar" runat="server" />
                                                    Consolidar?
                                                </label>
                                            </div>
                                        </div>
                        </div>
                    </div>
                     <div class="row">
                        <div class="col-md-5">
                                <asp:Button ID="btnAgregar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Guardar" OnClick="btnAgregar_Click" />
                                <asp:Button class="btn btn-danger btnagregarPermiso" ID="btnEliminar" Text="Cancelar" runat="server" OnClick="btnEliminar_Click" />
                            </div>
                         </div>
                </asp:Panel>
                <br />
                <div class="form-group row">
                    <div class="col-md-12">
                        <asp:GridView ID="GVTURNOS" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="True" AllowSorting="True" DataKeyNames="idturno" HorizontalAlign="Center" OnPageIndexChanging="GVTURNOS_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="nombreturno" HeaderText="Nombre Turno" />
                                <asp:BoundField DataField="bonus" HeaderText="Minutos Comodin" />
                                <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                                <asp:BoundField DataField="horaseptimo" HeaderText="HorasSeptimo" />
                                 <asp:BoundField DataField="horasturno" HeaderText="Horas Turno" />
                                  <asp:TemplateField HeaderText="Consolidar">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkConsolidar" runat="server" Checked='<% # Bind("consolidar")%>' Enabled="false"/>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbT1" runat="server" OnClick="LbT1_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">VerDetalle</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbT2" runat="server" OnClick="LbT2_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Editar</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbT3" runat="server" OnClick="lbT3_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Eliminar</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

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
                <asp:Panel ID="plnDetalle" runat="server">
                    <div class="form-group row">
                        <div class="col-md-4">
                            <asp:Label ID="lblDetalle" runat="server" class="control-label" for="focusedInput" Text="Label"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-4">
                            <asp:ImageButton ID="ImageButton1" runat="server" Height="40px" ImageUrl="~/Images/agregar.png" ToolTip="NUEVO DIA" Width="40px" OnClick="ImageButton2_Click" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-md-8">
                            <asp:GridView ID="gvdetalle" class="table table-striped table-hover" runat="server"
                                AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                                BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" DataKeyNames="idturno,diasemana">
                                <Columns>
                                    <asp:BoundField DataField="nombreturno" HeaderText="Nombre Turno" />
                                    <asp:BoundField DataField="NombreDia" HeaderText="Dia" />
                                    <asp:BoundField DataField="horaini" HeaderText="Hora Inicio" />
                                    <asp:BoundField DataField="horafin" HeaderText="Hora Fin" />
                                    <asp:BoundField DataField="almuerzo" HeaderText="Almuerzo" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbD1" runat="server" OnClick="LbD1_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Editar</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbD2" runat="server" OnClick="LbD2_Click" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Eliminar</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                <asp:Panel ID="plnDetalleEditar" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <label class="control-label" for="focusedInput">Dia Semana</label>
                                <asp:DropDownList ID="ddldias" runat="server" DataTextField="Nombre" DataValueField="numero">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Hora Inicio</label>
                                <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                    <asp:TextBox ID="txthoraI" class="form-control"
                                        runat="server" placeholder="Ej:07:00" onkeypress="return Hora(event)" onblur="return ValidarHora('txthoraI')"></asp:TextBox>
                                    <span class="input-group-addon borderCalendar">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Hora Fin</label>
                                <div class="input-group clockpicker" data-placement="left" data-align="top" data-autoclose="true">
                                    <asp:TextBox ID="txtHoraF" class="form-control"
                                        runat="server" placeholder="Ej:04:30" onkeypress="return Hora(event)" onblur="return ValidarHora('txtHoraF')"></asp:TextBox>
                                    <span class="input-group-addon borderCalendar">
                                        <span class="glyphicon glyphicon-time"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Horas Almuerzo</label>
                                <asp:TextBox ID="txtAlmuerzo" class="form-control"
                                    runat="server" onkeypress="return soloNumeros(event)" onblur="return ValidarHora(event)" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-5">
                                <asp:Button ID="btnGuardarDetalle" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Guardar" OnClick="btnGuardarDetalle_Click" />
                                <asp:Button class="btn btn-danger btnagregarPermiso" ID="btnEliminarDetalle" runat="server" Text="Cancelar" OnClick="btnEliminarDetalle_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $('.clockpicker').clockpicker();

        jQuery(function ($) {
            $.mask.definitions['H'] = '[012]';
            $.mask.definitions['N'] = '[012345]';
            $.mask.definitions['n'] = '[0123456789]';
            $("#txthoraI").mask("Hn:Nn");
            $("#txthoraF").mask("Hn:Nn");
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

        function Hora(e) {
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = "0123456789:";
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

        function ValidarHora(campo) {
            var vcampo = document.getElementById('MainContent_' + campo).value;
            var arrHora = [];
            arrHora = vcampo.split(":");
            var c = 0;
            if (arrHora.length != 2) {
                c = c + 1;
            }
            if (parseInt(arrHora[0]) < 0 || parseInt(arrHora[0]) > 23) {
                c = c + 1;
            }

            if (parseInt(arrHora[1]) < 0 || parseInt(arrHora[1]) > 59) {
                c = c + 1;

            }
            if (arrHora[0].length > 2 || arrHora[1].length > 2)
            {
                c = c + 1;
            }
            if (c > 0) {
                //$('#alertValida').css("display", "none");
                //$('#lblAlert').css("display", "none");
                //$('#lblAlert').text = 'FORMATO DE HORA INCORRECTO';
                return false;
            }
            else {
                //$('#alertValida').css("display", "block");
                //$('#lblAlert').css("display", "block");
                return true;
            }

        }
    </script>
</asp:Content>

