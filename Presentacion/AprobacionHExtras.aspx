<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AprobacionHExtras.aspx.cs" Inherits="NominaRRHH.Presentacion.AprobacionHExtras" %>

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
                    <div class="col-md-12">

                        <br />
                        <div class="row">
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

                            <div class="col-md-2">
                                <asp:Button ID="btnBuscar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                            </div>


                        </div>
                        <div class="row">
                            <div class="col-md-12 paddingContentTabs">
                                <div class="col-md-2" id="divChkDptoVac" runat="server" visible="false">
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="ChkEstatus" runat="server" Checked="false" OnCheckedChanged="ChkEstatus_CheckedChanged" AutoPostBack="true" />
                                            <strong>En Revision</strong>
                                        </label>
                                    </div>
                                </div>
                                <div runat="server" id="divfiltro" visible="false">
                                    <div class="col-md-3">
                                        <label class="control-label" for="focusedInput">Pertenece al Area de:</label>
                                        <asp:DropDownList class="form-control" ID="ddlProceso" runat="server" OnSelectedIndexChanged="ddlProceso_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="Button1" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Guardar" OnClick="Button1_Click" />
                                    </div>
                                </div>

                            </div>
                        </div>

                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="GVHEx" class="table table-striped table-hover" runat="server" Width="100%"
                                    AutoGenerateColumns="False" DataKeyNames="codigo_empleado,fecha,tipoingrdeduc" CellPadding="2" GridLines="Vertical"
                                    BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" OnRowDataBound="GVHEx_RowDataBound"
                                    AllowSorting="True" OnRowCommand="GVHEx_RowCommand">
                                    <AlternatingRowStyle Width="100px" />
                                    <Columns>

                                        <asp:BoundField DataField="codigo_empleado" HeaderText="Codigo">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombrecompleto" HeaderText="Nombre">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                <%--        <asp:BoundField DataField="nombre_depto" HeaderText="Departamento">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Cargar a">
                                             <HeaderTemplate>
                                                 <asp:DropDownList runat="server" ID="ddlDeptoAfectaColumn" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlDeptoAfectaColumn_SelectedIndexChanged">
                                                </asp:DropDownList>  
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeptoAfecta" runat="server" Text='<%# Eval("depto_afecta") %>' Visible="false" />
                                                <asp:DropDownList runat="server" ID="ddlDeptoAfecta" Width="150px">
                                                </asp:DropDownList>                                               
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="horaini" HeaderText="Entra">
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="horafin" HeaderText="Sale">
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total" HeaderText="Total" DataFormatString="{0:0.00}">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Tiempo Libre">
                                               <HeaderTemplate>
                                                <asp:DropDownList runat="server" ID="ddlTiempoLibreColumn" Width="150px" 
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlTiempoLibreColumn_SelectedIndexChanged">
                                                    <asp:ListItem Value="0.00">No tomo hora para alimentacion</asp:ListItem>
                                                    <asp:ListItem Value="0.50">Tomo 1/2 hr para alimentacion</asp:ListItem>
                                                    <asp:ListItem Value="1.00">Tomo 1 hr para alimentacion</asp:ListItem>
                                                </asp:DropDownList> 
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTiempoLibre" runat="server" Text='<%# Eval("tiempo_libre") %>' Visible="false" />
                                                <asp:DropDownList runat="server" ID="ddlTiempoLibre" Width="150px" 
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlTiempoLibre_SelectedIndexChanged">
                                                    <asp:ListItem Value="0.00">No tomo hora para alimentacion</asp:ListItem>
                                                    <asp:ListItem Value="0.50">Tomo 1/2 hr para alimentacion</asp:ListItem>
                                                    <asp:ListItem Value="1.00">Tomo 1 hr para alimentacion</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="A Pagar">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hfhrvalidas" Value='<% # Bind("HEPagar")%>'/>
                                                <asp:TextBox ID="txtHEPagar" class="form-control" Text='<% # Bind("HEPagar")%>' Width="80px" DataFormatString="{0:0.00}" 
                                                    onkeypress="return soloNumeros(event)" onblur="return ValidarHorasPagar(this)" autocomplete="off" runat="server" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="acumulado" HeaderText="Acum." DataFormatString="{0:0.00}">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>

                                        <%--  <asp:TemplateField HeaderText="Bono">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="ChkAllBono" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAllBono_CheckedChanged" />Todos Bono
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkBono" runat="server" Checked='<%# Convert.ToBoolean(Eval("bono"))%>' AutoPostBack="True" OnCheckedChanged="ChkBono_CheckedChanged" />
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HE">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="ChkAllHE" runat="server" AutoPostBack="True" OnCheckedChanged="ChkAllHE_CheckedChanged" />Todos HE
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkHE" runat="server" Checked='<%# Convert.ToBoolean(Eval("he"))%>' AutoPostBack="True" OnCheckedChanged="ChkHE_CheckedChanged" />
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Tipo">
                                             <HeaderTemplate>
                                                <asp:DropDownList runat="server" ID="ddlTipoColumn" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoColumn_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTipo" runat="server" Text='<%# Eval("tipoingrdeduc") %>' Visible="false" />
                                                <asp:DropDownList runat="server" ID="ddlTipo">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

<%--                                        <asp:BoundField DataField="estatus" HeaderText="Estatus">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="comentario" HeaderText="Comentario" >
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <%--    <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btneditar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="editar" runat="server" Text="Editar" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Eliminar">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btneliminar"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="eliminar" runat="server" Text="Eliminar" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="16px" />
                                                </asp:TemplateField>--%>

                                        <%--                                                <asp:ButtonField ButtonType="Button" Text="Editar" CommandName="editar">
                                                    <ControlStyle CssClass="btn btn-success btn-xs" />
                                                    <ItemStyle Width="16px" />
                                                </asp:ButtonField>
                                                <asp:ButtonField ButtonType="Button" CommandName="eliminar" Text="Eliminar">
                                                    <ControlStyle CssClass="btn btn-danger btn-xs" />
                                                    <ItemStyle Width="16px" />
                                                </asp:ButtonField>--%>
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

        function ValidarHorasPagar(textbox) {           
            var vdigitado = document.getElementById(textbox.id).value;                       
            
            var array = [];
            array = textbox.id.split("_")

            var index = 0;
            index = array[3];

            var vcalculado = document.getElementById(textbox.id.substr(0, 18) + "hfhrvalidas" + "_" + index).value;
           
            if (vdigitado > vcalculado) {
                document.getElementById(textbox.id).value = vcalculado;
            }
        }
        $(function () {
            $('#datePicker,#datePicker2').datepicker({
                format: 'dd/mm/yyyy'
            })
                .on('changeDate', function (e) {
                    $('.datepicker dropdown-menu').hide();
                });



        });
    </script>
</asp:Content>
