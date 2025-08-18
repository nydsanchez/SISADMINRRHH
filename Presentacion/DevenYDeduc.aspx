<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DevenYDeduc.aspx.cs" Inherits="NominaRRHH.Presentacion.DevenYDeduc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#deducciones" data-toggle="tab" aria-expanded="true">Deducciones</a></li>
                            <li class=""><a href="#devengados" data-toggle="tab" aria-expanded="false">Devengados</a></li>
                        </ul>
                        <div id="myTabContent" class="tab-content paddingTab">
                            <div class="tab-pane fade active in" id="deducciones">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Deduccion</label>
                                            <asp:TextBox ID="txtDeduccion" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">Prioridad</label>
                                            <asp:DropDownList class="form-control" ID="ddlprioridad" runat="server">
                                                <asp:ListItem Value="1">Ley/Embargos</asp:ListItem>
                                                <asp:ListItem Value="2">Internas</asp:ListItem>
                                                <asp:ListItem Value="3">Externas</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="col-md-1 marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="ChkActivo" runat="server" />
                                                        <strong>Activo</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <div class="marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="ChkAplicaAg" runat="server" />
                                                        <strong>Aplica Aguinaldo</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="ChkAplicaVac" runat="server" />
                                                        <strong>Aplica Vacaciones</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="ChkMostrarComp" runat="server" Checked="true" />
                                                        <strong>Mostrar en Boleta</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="ChkAplicaDeducIBruto" runat="server" />
                                                        <strong>Ded Ingreso Bruto</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <asp:Button ID="btnAgregarDed" Class="btn btn-info" Style="margin-top: 22px;" runat="server" Text="Agregar" OnClick="btnAgregarDed_Click" />
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GVDeducciones" class="table table-striped table-hover" runat="server" Width="100%"
                                            AutoGenerateColumns="False" DataKeyNames="idDeduccion" CellPadding="2" GridLines="Vertical"
                                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" OnRowDataBound="GVDeducciones_RowDataBound"
                                            AllowPaging="True" AllowSorting="True" OnRowCommand="GVDeducciones_RowCommand" OnPageIndexChanging="GVDeducciones_PageIndexChanging">
                                            <AlternatingRowStyle Width="100px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtId" class="form-control" ReadOnly="true" runat="server" Text='<% # Bind("idDeduccion")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Descripcion">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDesc" class="form-control" autocomplete="off" runat="server" Text='<% # Bind("deduccionNombre")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activo">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAct" runat="server" Checked='<% # Bind("deduccionActiva")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Aplica Ag.">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkapag" runat="server" Checked='<% # Bind("aplicaAgui")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Aplica Vac.">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkapva" runat="server" Checked='<% # Bind("aplicaVac")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Prioridad">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblprioridad" runat="server" Text='<%# Eval("idprioridad") %>' Visible="false" />
                                                        <asp:DropDownList runat="server" ID="ddldeducp">
                                                            <asp:ListItem Value="1">Ley/Embargos</asp:ListItem>
                                                            <asp:ListItem Value="2">Internas</asp:ListItem>
                                                            <asp:ListItem Value="3">Externas</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mostrar">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkmostrar" runat="server" Checked='<% # Bind("mostrarc")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deduc I. Bruto">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkdeducib" runat="server" Checked='<% # Bind("deduccionIBruto")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Editar">
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
                                                </asp:TemplateField>

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
                            <div class="tab-pane fade" id="devengados">
                                 <div class="row">
                                    <div class="col-md-12 paddingContentTabs">
                                         <div class="col-md-3">
                                            <label class="control-label" for="focusedInput">Devengado</label>
                                            <asp:TextBox ID="txtDevengado" class="form-control"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        
                                        <div class="col-md-2">
                                            <div class="marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="chkInss" runat="server" />
                                                        <strong>Aplica INSS</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="chkIr" runat="server" />
                                                        <strong>Aplica IR</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="ChKLiq" runat="server" />
                                                        <strong>Aplica Liq.</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        </div>
                                     </div>
                                <div class="row">
                                    <div class="col-md-12 paddingContentTabs">
                                       
                                          <div class="col-md-2">
                                            <div class="marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="ChkAplicaDeduc" runat="server" />
                                                        <strong>Aplica Ded.</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="ChkBoleta" runat="server" Checked="true" />
                                                        <strong>Mostrar en Boleta</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="marginChkActivo">
                                                <div class="checkbox">
                                                    <label class="control-label" for="focusedInput">
                                                        <asp:CheckBox ID="ChkDobleP" runat="server" OnCheckedChanged="ChkDobleP_CheckedChanged" AutoPostBack="true" />
                                                        <strong>Aplica Ded Ing. Bruto</strong>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                   <div runat="server" id="divasociado" visible="false">
                                       <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">ID Ingreso Asociado</label>
                                            <asp:TextBox ID="TxtIngAsociado" class="form-control" onkeypress="return soloNumeros(event)"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label" for="focusedInput">ID Deduccion Asociada</label>
                                            <asp:TextBox ID="TxtDeducAsociada" class="form-control" onkeypress="return soloNumeros(event)"
                                                runat="server" autocomplete="off"></asp:TextBox>
                                        </div>
                                       </div>
                                                                                <asp:Button ID="btnAgregarDev" Class="btn btn-info" Style="margin-top: 22px;" runat="server" Text="Agregar" OnClick="btnAgregarDev_Click" />

                                        </div>
                                    </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GVdevengados" class="table table-striped table-hover" runat="server" Width="100%"
                                            AutoGenerateColumns="False" DataKeyNames="idDevengado" CellPadding="2" GridLines="Vertical"
                                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                                            AllowPaging="True" AllowSorting="True" OnRowCommand="GVdevengados_RowCommand" OnPageIndexChanging="GVdevengados_PageIndexChanging">
                                            <AlternatingRowStyle Width="100px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtId" class="form-control" ReadOnly="true" runat="server" Text='<% # Bind("idDevengado")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Descripcion">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDesc" class="form-control" autocomplete="off" runat="server" Text='<% # Bind("devengadoNombre")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Aplica INSS">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkInss" runat="server" Checked='<% # Bind("aplicaINSS")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Aplica IR">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkIR" runat="server" Checked='<% # Bind("aplicaIR")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Aplica Liq">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChKLiq" runat="server" Checked='<% # Bind("aplicaLiq")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Aplica Deduc">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkaplicadeduc" runat="server" Checked='<% # Bind("aplicaDeduc")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Mostrar">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkmostrar" runat="server" Checked='<% # Bind("mostrarc")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deduc I. Bruto">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkdeducib" runat="server" Checked='<% # Bind("deduccionIBruto")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IDIngAsoc">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TxtIngAsociado" class="form-control" runat="server" Text='<% # Bind("idIngresoAsociado")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="IDDeducAsoc">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TxtDeducAsociada" class="form-control" runat="server" Text='<% # Bind("idDeduccionIBruto")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Editar">
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
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
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
    </script>
</asp:Content>
