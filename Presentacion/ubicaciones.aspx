<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ubicaciones.aspx.cs" Inherits="NominaRRHH.Presentacion.ubicaciones" %>

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
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Empresa</label>
                            <asp:DropDownList class="form-control" ID="ddlEmpresa" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Ubicacion</label>
                            <asp:TextBox ID="txtUbicacion" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                           <div class="col-md-2">
                                <label class="control-label" for="focusedInput">Tipo de Nomina</label>
                                <asp:DropDownList class="form-control" ID="ddlTipNomina" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                     <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Telefono</label>
                            <asp:TextBox ID="txtTelefono" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Num Patronal</label>
                            <asp:TextBox ID="txtNpatronal" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Num Ruc</label>
                            <asp:TextBox ID="txtNruc" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                       
                    </div>
                         </div>
                    <div class="row">
                    <div class="col-md-12">
                         <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Departamento</label>
                            <asp:TextBox ID="txtDepartamento" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Municipio</label>
                            <asp:TextBox ID="txtMunicipio" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-8">
                            <label class="control-label" for="focusedInput">Direccion</label>
                            <asp:TextBox ID="txtDireccion" class="form-control"
                                runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-md-2" style="margin-top: 25px;">
                            <asp:Button class="btn btn-success" ID="BtnAgregar" runat="server" Text="Agregar" OnClick="BtnAgregar_Click" />
                            <asp:Button class="btn btn-info" ID="btnEditar" Visible="false" runat="server" Text="Editar" OnClick="btnEditar_Click"/>
                        </div>
                    </div>
                </div>
                <br />             
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvUbicaciones" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="3" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos" OnSelectedIndexChanged="gvUbicaciones_SelectedIndexChanged" OnRowDataBound="gvUbicaciones_RowDataBound">
                            <AlternatingRowStyle Width="578px" />
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Sel">
                                    <ControlStyle Width="5px" />
                                    <HeaderStyle Width="5px" />
                                    <ItemStyle Width="5px" />
                                </asp:CommandField>
                                <asp:BoundField DataField="codigo_ubicacion" HeaderText="Codigo">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre_ubicacion" HeaderText="Nombre">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                 <asp:TemplateField HeaderText="Planilla">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltplanilla" runat="server" Text='<%# Eval("tplanilla") %>' Visible="false" />
                                                        <asp:DropDownList runat="server" ID="ddltplanilla">                                                           
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" />
                                                </asp:TemplateField>
                                <asp:BoundField DataField="telefono" HeaderText="Telefono">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="direccion" HeaderText="Direccion">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Npatronal" HeaderText="Npatronal">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nruc" HeaderText="Nruc">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="departamento" HeaderText="Departamento">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="municipio" HeaderText="Municipio">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

