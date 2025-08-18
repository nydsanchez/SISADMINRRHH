<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepartamentoCargo.aspx.cs" Inherits="NominaRRHH.Presentacion.DepartamentoCargo" %>

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
                <div class="row custom-control">
                    <div>
                        <div class="col-md-3">
                            <asp:Label ID="Label" runat="server">Departamento</asp:Label>
                            <asp:dropdownlist ID="ddlDepartamento" class="form-control"
                                runat="server" autocomplete="off" tooltip="Cargo" OnSelectedIndexChanged="ddlDepartamento_SelectedIndexChanged" AutoPostBack="True"></asp:dropdownlist>
                        </div>  
                        <div class="col-md-3">
                 <asp:Label ID="Label1" runat="server">Cargo</asp:Label>
                 <asp:dropdownlist ID="ddlCargo" runat="server"  class="form-control"></asp:dropdownlist>
                 
                </div>
                        <div class="col-md-3">
                            <asp:Button class="btn btn-success" ID="BtnAgregar" runat="server" Text="Agregar" OnClick="BtnAgregar_Click" />
                            <br>
                </div>
                    </div>
                    </div>

                

                <div class="row">
                    <br />
                    <br />
                    <div class="col-md-16">
                        <asp:GridView ID="gvOperaciones" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" CellSpacing="2" DataKeyNames="codigo_cargo"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos"
                            AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvOperaciones_PageIndexChanging" OnSelectedIndexChanged="gvOperaciones_SelectedIndexChanged" PageSize="50" BackColor="White" BorderColor="#3366CC" >
                            <AlternatingRowStyle/>
                            <Columns>
                                <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Borrar">
                                    <ControlStyle Width="50px" />
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle Width="50px" />
                                </asp:CommandField>                              

                                <asp:TemplateField HeaderText="Codigo">
                                    <ItemTemplate>
                                        <asp:TextBox ID="codigo_cargo" class="form-control custom-control" ReadOnly="true" runat="server" Text='<% # Bind("codigo_cargo")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" /> 
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtnombre_cargo" class="form-control custom-control" ReadOnly="true" runat="server" Text='<% # Bind("nombre_cargo")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="250px" />                                    
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                            <PagerSettings PageButtonCount="50" FirstPageText="&lt;&lt;" LastPageText="&gt;&gt;" NextPageText="-&gt;" PreviousPageText="&lt;-" />
                            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                            <RowStyle BackColor="White" ForeColor="#003399" />
                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                            <SortedDescendingHeaderStyle BackColor="#002876" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
       </div>
    </div>
    <style>
    .custom-control {
    height: 30px;       /* Altura reducida */
    padding: 0px 0px;  /* Ajuste del padding para alinear el texto correctamente */
    font-size: 14px;    /* Reducción del tamaño de la fuente si es necesario */
}
        </style>
</asp:Content>

