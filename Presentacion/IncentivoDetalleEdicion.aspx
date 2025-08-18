<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncentivoDetalleEdicion.aspx.cs" Inherits="NominaRRHH.Presentacion.IncentivoDetalleEdicion" %>

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
                            <asp:TextBox ID="txtPeriodo" class="form-control" runat="server" ReadOnly="true" autocomplete="off" TabIndex="2"></asp:TextBox>
                        </div>
                        <div class="col-md-1" id="divSemana" runat="server" >
                            <label class="control-label" for="focusedInput">Semana</label>
                            <asp:DropDownList class="form-control" Style="width: 67px;" ID="ddlSemana" runat="server" OnSelectedIndexChanged="ddlSemana_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                                <asp:ListItem Value="1"> 1</asp:ListItem>
                                <asp:ListItem Value="2"> 2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    
                    </div>
                </div>
                        <%-- <div class="row">
                    <div class="col-md-12 ">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">B.Asistencia</label>
                            <asp:TextBox ID="TextBox1" class="form-control" autocomplete="off" TabIndex="4"
                                runat="server" OnTextChanged="txtCodigo_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Viatico</label>
                            <asp:TextBox ID="TextBox2" class="form-control"
                                runat="server" autocomplete="off" ReadOnly="true" TabIndex="5"></asp:TextBox>
                        </div>
            <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Otros Ing.</label>
                            <asp:TextBox ID="TextBox3" class="form-control" runat="server" ReadOnly="true" autocomplete="off" TabIndex="2"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Deducciones</label>
                            <asp:TextBox ID="TextBox4" class="form-control" runat="server" ReadOnly="true" autocomplete="off" TabIndex="2"></asp:TextBox>
                        </div>
                         <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Total</label>
                            <asp:TextBox ID="TextBox5" class="form-control" runat="server" ReadOnly="true" autocomplete="off" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                </div>--%>
          <div class="row">
                    <div class="col-md-12">
                        <asp:Label runat="server" class="control-label">VAT</asp:Label>
                        <asp:GridView ID="gvIngresosEmp" class="table table-striped table-hover" runat="server" Style="width: 100%;"
                            AutoGenerateColumns="False" DataKeyNames="" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" OnRowDataBound="gvIngresosEmp_RowDataBound" ShowFooter="True" 
                            OnSelectedIndexChanged="gvIngresosEmp_SelectedIndexChanged"                            
                            OnRowCommand="gvIngresosEmp_RowCommand">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                  
                            <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                    <ControlStyle Width="5px" />
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:CommandField>
                                  
                                <asp:TemplateField HeaderText="Rubro">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hfidtipoing" Value='<% # Bind("idtipoing")%>'/>
                                      
                                        <asp:TextBox ID="txtRubro" class="form-control" autocomplete="off" runat="server" Text='<% # Bind("devengadoNombre")%>' ReadOnly="true"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BonoA">
                                    <ItemTemplate>
                                         <asp:HiddenField runat="server" ID="hfbonoasistencia" Value='<% # Bind("bonoasistencia")%>'/>
                                        <asp:TextBox ID="txtbonoasistencia" class="form-control" autocomplete="off" runat="server" Text='<% # Bind("bonoasistencia")%>' onkeypress="return soloNumeros(event)"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Incentivo">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hfincentivo" Value='<% # Bind("incentivo")%>'/>
                                        <asp:TextBox ID="txtincentivo" class="form-control" autocomplete="off" runat="server" Text='<% # Bind("incentivo")%>' onkeypress="return soloNumeros(event)"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OtrosIng.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtotrosIngresos" class="form-control" ReadOnly="true" autocomplete="off" runat="server" Text='<% # Bind("otrosIngresos")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deduc.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtdeducciones" class="form-control" ReadOnly="true" autocomplete="off" runat="server" Text='<% # Bind("deducciones")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotal" class="form-control" ReadOnly="true" autocomplete="off" runat="server" Text='<% # Bind("total")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:Button ID="btneditar"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>
            "
                                            CommandName="editar" runat="server" Text="Editar" />
                                    </ItemTemplate>
                                     <ItemStyle Width="20px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemTemplate>
                                        <asp:Button ID="btneliminar"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>
            "
                                            CommandName="eliminar" runat="server" Text="Eliminar" />
                                    </ItemTemplate>
                                     <ItemStyle Width="20px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="idtipoing" HeaderText="IdTipo">
                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <br />
                
               <div class="row">
                    <div class="col-md-12">
                        <asp:Label runat="server" class="control-label">Desgloce</asp:Label>
                        <asp:GridView ID="GVrubroapldeduc" class="table table-striped table-hover" runat="server" Style="width: 100%;"
                            AutoGenerateColumns="False" DataKeyNames="" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            OnRowCommand="GVrubroapldeduc_RowCommand">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>

                                <asp:TemplateField HeaderText="Rubro">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hfditipo" Value='<% # Bind("Id_tipo")%>'/>
                                        <asp:HiddenField runat="server" ID="hfiddev" Value='<% # Bind("id_categoria")%>'/>
                                        <asp:HiddenField runat="server" ID="hfIdTipoIng" Value='<% # Bind("IdTipoIng")%>'/>                                       
                                        <asp:HiddenField runat="server" ID="hfGeneradoSistema" Value='<% # Bind("GeneradoSistema")%>'/>
                                        <asp:HiddenField runat="server" ID="hfcampoafecta" Value='<% # Bind("campoafecta")%>'/>
                                        <asp:HiddenField runat="server" ID="hfoperacion" Value='<% # Bind("operacion")%>'/>
                                        <asp:TextBox ID="txtRubro" class="form-control" autocomplete="off" runat="server" Text='<% # Bind("detalle")%>' ReadOnly="true"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Monto">
                                    <ItemTemplate>
                                         <asp:HiddenField runat="server" ID="hfvalor" Value='<% # Bind("valor")%>'/>
                                        <asp:TextBox ID="txtValor" class="form-control" autocomplete="off" runat="server" Text='<% # Bind("valor")%>' onkeypress="return soloNumeros(event)"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="130px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Editar">
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
                                </asp:TemplateField>
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

