<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialContratacionesXEmpleado.aspx.cs" Inherits="NominaRRHH.Presentacion.HistorialContratacionesXEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top">
        <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server" visible="false">
            <button type="button" class="close" data-dismiss="alert">×</button>
            <asp:Label ID="lblAlert" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server" visible="false">
            <button type="button" class="close" data-dismiss="alert">×</button>
            <asp:Label ID="LblSuccess" runat="server" Visible="false"></asp:Label>
        </div>
        <fieldset class="marginFields">
            <legend>Histórico Contrataciones</legend>
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label class="control-label" for="focusedInput">Documento Identidad</label>
                        <asp:TextBox ID="txtdoc_ident" autofocus="true" class="form-control" placeholder="Digite Codigo"
                            runat="server" onkeypress="return soloNumeros(event)" autocomplete="off"></asp:TextBox>
                    </div>

                    <div class="col-md-1" style="margin-left: 50px;">
                        <asp:ImageButton ID="btnBuscar" Class="btnSearch" ImageUrl="~/Images/lupa3.png" runat="server" OnClick="btnBuscar_Click" />
                    </div>

                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="GVDetNomEmpl" class="table table-striped table-hover" runat="server"
                        AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                        BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                        AllowPaging="True" AllowSorting="True" OnRowDataBound="GVDetNomEmpl_RowDataBound" >
                        <AlternatingRowStyle Width="100px" />
                        <Columns>
                           
                            <asp:BoundField DataField="nombrecompleto" HeaderText="nombrecompleto">
                                <ItemStyle Width="5px" HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="cedula_identidad" HeaderText="Doc Identidad">
                                <ItemStyle Width="5px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_empleado" HeaderText="Codigo">
                                <ItemStyle Width="5px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cargo" HeaderText="Cargo">
                                <ItemStyle Width="5px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_depto" HeaderText="Departamento">
                                <ItemStyle Width="5px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechaprimeringreso" HeaderText="fecha Ingreso" DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle Width="5px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecha_egreso" HeaderText="Fecha Egreso">
                                <ItemStyle Width="5px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="observacion" HeaderText="Observacion">
                                <ItemStyle Width="5px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Foto">
                                <ItemTemplate>
                                    
                                     <asp:Image ID="imgfoto" class="sizeFoto" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>

