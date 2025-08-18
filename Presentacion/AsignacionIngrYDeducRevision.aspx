<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsignacionIngrYDeducRevision.aspx.cs" Inherits="NominaRRHH.Presentacion.AsignacionIngrYDeducRevision" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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

                <div class="row">
                    <div class="col-md-12">
                          <%--<div class="col-md-2">
                                    <label class="control-label" for="focusedInput">Ubicacion</label>
                                    <asp:DropDownList class="form-control" ID="ddlUbicacion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUbicacion_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div> 
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Tipos De Planilla</label>
                            <asp:DropDownList class="form-control" ID="ddlTiposPlanilla" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTiposPlanilla_SelectedIndexChanged" TabIndex="1">
                            </asp:DropDownList>
                        </div>--%>
                     
                        <%--<div class="col-md-1" id="divSemana" runat="server" visible="false">
                            <label class="control-label" for="focusedInput">Semana</label>
                            <asp:DropDownList class="form-control" Style="width: 67px;" ID="ddlSemana" runat="server" OnSelectedIndexChanged="ddlSemana_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                                <asp:ListItem Value="1"> 1</asp:ListItem>
                                <asp:ListItem Value="2"> 2</asp:ListItem>
                            </asp:DropDownList>
                        </div>--%>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 ">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Codigo</label>
                            <asp:TextBox ID="txtCodigo" class="form-control" autocomplete="off" TabIndex="4"
                                runat="server" OnTextChanged="txtCodigo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label" for="focusedInput">Nombre</label>
                            <asp:TextBox ID="TxtNombreE" class="form-control"
                                runat="server" autocomplete="off" ReadOnly="true" TabIndex="5"></asp:TextBox>
                        </div>
                           <div class="col-md-1">
                            <label class="control-label" for="focusedInput">Periodo</label>
                            <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" autocomplete="off" TabIndex="2" OnTextChanged="txtPeriodo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                       <%-- <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Tipo Asignacion</label>
                            <asp:DropDownList class="form-control" ID="ddlAsig" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAsig_SelectedIndexChanged" TabIndex="6">
                                <asp:ListItem Value="0"> Seleccione una Opcion</asp:ListItem>
                                <asp:ListItem Value="1"> Ingreso</asp:ListItem>
                                <asp:ListItem Value="2"> Deduccion</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2" runat="server" id="divIngrs" visible="false">
                            <label class="control-label" for="focusedInput">Tipo Ingresos</label>
                            <asp:DropDownList class="form-control" ID="ddlTipoIngrs" runat="server" TabIndex="7">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2" runat="server" id="divEgrs" visible="false">
                            <label class="control-label" for="focusedInput">Tipo Deducciones</label>
                            <asp:DropDownList class="form-control" ID="ddlTipDeduc" runat="server" TabIndex="8">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Total</label>
                            <asp:TextBox ID="txtTotal" class="form-control" runat="server" onkeypress="return soloNumeros(event)" autocomplete="off" TabIndex="9"></asp:TextBox>
                        </div>--%>

                    </div>
                </div>
             <%--   <div class="row">
                    <div class="col-md-12 ">
                        <div class="col-md-2">
                            <asp:Button ID="btnAgregar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Agregar" OnClick="btnAgregar_Click" TabIndex="10" />
                        </div>
                    </div>
                </div>--%>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label runat="server" class="control-label">Ingresos</asp:Label>
                        <asp:GridView ID="gvIngresosEmp" class="table table-striped table-hover" runat="server" Style="width: 100%;"
                            AutoGenerateColumns="False" DataKeyNames="id,idDevengado" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            >
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                               <%-- <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                    <ControlStyle Width="5px" />
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:CommandField>--%>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtId" Style="width: 100px;" class="form-control" autocomplete="off" ReadOnly="true" runat="server" Text='<% # Bind("id")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IdDevengado">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIdDevengado" Style="width: 100px;" class="form-control" autocomplete="off" ReadOnly="true" runat="server" Text='<% # Bind("idDevengado")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="devengadoNombre" HeaderText="Concepto">
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="nsemana" HeaderText="Semana">
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotal" class="form-control" autocomplete="off" runat="server" Text='<% # Bind("valor")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                         <%--       <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:Button ID="btneditar"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>
            "
                                            CommandName="editar" runat="server" Text="Editar" />
                                    </ItemTemplate>
                                     <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemTemplate>
                                        <asp:Button ID="btneliminar"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>
            "
                                            CommandName="eliminar" runat="server" Text="Eliminar" />
                                    </ItemTemplate>
                                     <ItemStyle Width="30px" />
                                </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label runat="server" class="control-label">Deducciones</asp:Label>
                        <asp:GridView ID="GVDetNomEmpl" class="table table-striped table-hover" runat="server" Style="width: 100%;"
                            AutoGenerateColumns="False" DataKeyNames="id,idDeduccion" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                          >
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                             <%--   <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                    <ControlStyle Width="5px" />
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:CommandField>--%>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtId" Style="width: 100px;" class="form-control" autocomplete="off" ReadOnly="true" runat="server" Text='<% # Bind("id")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IdDeduccion">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIdDeduccion" Style="width: 100px;" class="form-control" autocomplete="off" ReadOnly="true" runat="server" Text='<% # Bind("idDeduccion")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="10px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="deduccionNombre" HeaderText="Concepto">
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="nsemana" HeaderText="Semana">
                                    <ItemStyle Width="50px" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotal" class="form-control" autocomplete="off" runat="server" Text='<% # Bind("valor")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                              <%-- <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:Button ID="btneditar"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>
            "
                                            CommandName="editar" runat="server" Text="Editar" />
                                    </ItemTemplate>
                                     <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemTemplate>
                                        <asp:Button ID="btneliminar"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>
            "
                                            CommandName="eliminar" runat="server" Text="Eliminar" />
                                    </ItemTemplate>
                                     <ItemStyle Width="30px" />
                                </asp:TemplateField>--%>
                            </Columns>
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

