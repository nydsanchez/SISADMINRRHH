<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncTablaIncentivos.aspx.cs" Inherits="NominaRRHH.Presentacion.IncTablaIncentivos" %>
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
    
    <div class="container mar-top">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <label class="control-label" for="focusedInput">TABLA DE INCENTIVOS</label>
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
                <asp:Panel ID="plnEditarT" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">ID Area</label>
                                <asp:TextBox ID="txtIdArea" class="form-control" 
                                    runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Construccion</label>
                                <asp:DropDownList class="form-control" ID="ddlConstruccion" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Bono Asistencia</label>
                                <asp:TextBox ID="txtBonoasistencia" class="form-control"
                                    runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Bono Calidad</label>
                            <asp:TextBox ID="txtBonocalidad" class="form-control"
                            runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Meta 5 dias:</label>
                                <asp:TextBox ID="txtMeta5dias" class="form-control"
                                    runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                            </div>
                            </div>
                        </div>
                    <div class="row">
                    <div class="col-md-12">                         
                          <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Rango oql</label>
                           <label class="control-label" for="focusedInput">Ubicacion</label>
                            <asp:DropDownList class="form-control" ID="ddlRangooql" runat="server">
                            </asp:DropDownList>
                           </div>
                            <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Incentivo Semanal</label>
                            <asp:TextBox ID="txtIncsemanal" class="form-control"
                            runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Efic. Desde:</label>
                            <asp:TextBox ID="txtEficdesde" class="form-control"
                            runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Efic. hasta:</label>
                            <asp:TextBox ID="txtEfichasta" class="form-control"
                            runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                            </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Buscar Eficiencia:</label>
                            <asp:Button ID="btnBuscarEficiencia" Class="btn btn-success" 
                            runat="server" Text="Buscar Efic." OnClick="btnBuscarEficiencia_Click" />
                        </div>
                            <div class="col-md-2">
                            <label class="control-label" for="focusedInput"></label>
                            <asp:TextBox ID="txtId" class="form-control"
                            runat="server" onkeypress="return soloNumeros(event)" autocomplete="off" Visible="False"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                     <div class="row">
                        <div class="col-md-9">
                                <asp:Button ID="btnAgregar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                                <asp:Button ID="btnGuardar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                                <asp:Button class="btn btn-danger" ID="btnEliminar" Style="margin-top: 22px;" Text="Eliminar" runat="server" OnClick="btnEliminar_Click" />
                                <asp:Button ID="btnBuscarConstruccion" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Buscar Cons." OnClick="btnBuscarConstruccion_Click" />
                                <asp:Button ID="btnGuardarEficiencia" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Guardar Efc." OnClick="btnGuardarEficiencia_Click" />
                                <asp:Button ID="btnAgregarEfic" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Agregar Efc." OnClick="btnAgregarEfic_Click" />
                                <asp:Button class="btn btn-danger" ID="btnEliminarEfic" Style="margin-top: 22px;" Text="Eliminar Efic." runat="server" OnClick="btnEliminarEfic_Click" />
                            </div>
                         </div>
                </asp:Panel>
                <br />
                <div class="form-group row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvTabla" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="True" AllowSorting="True" HorizontalAlign="Center" OnPageIndexChanging="gvTabla_PageIndexChanging" PageSize="20" OnSelectedIndexChanged="gvTabla_SelectedIndexChanged" DataKeyNames="ID,IdConstruccion">
                            <Columns>
                                <asp:CommandField SelectText="Sel" ShowSelectButton="True" />
                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                <asp:BoundField DataField="idArea" HeaderText="Area" />
                                <asp:BoundField DataField="Construccion" HeaderText="Const.">
                                </asp:BoundField>
                                <asp:BoundField DataField="BonoAsist" HeaderText="Bono Asist." DataFormatString="{0:0}" />
                                 <asp:BoundField DataField="DzDesde" HeaderText="DzDesde" />
                                  <asp:BoundField DataField="DzHasta" HeaderText="DzHasta" />
                                <asp:BoundField DataField="idRangoOql" HeaderText="Rango Oql" />
                                <asp:BoundField DataField="CostoDz" HeaderText="Costo Dz" />
                                <asp:BoundField DataField="EficienciaDesde" HeaderText="Efic. Desde" />
                                <asp:BoundField DataField="EficienciaHasta" HeaderText="Efic. Hasta" />
                                <asp:BoundField DataField="meta4dias" HeaderText="Meta 4" DataFormatString="{0:0}" />
                                <asp:BoundField DataField="meta5dias" HeaderText="Meta 5" DataFormatString="{0:0}" />
                                <asp:BoundField DataField="BonoCalidad" HeaderText="Bono Cal." DataFormatString="{0:0}" />
                                <asp:BoundField DataField="CostoSem" HeaderText="Costo Semanal" DataFormatString="{0:0}" />

                                <asp:BoundField DataField="IdConstruccion" Visible="False" />

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

    </script>
</asp:Content>

