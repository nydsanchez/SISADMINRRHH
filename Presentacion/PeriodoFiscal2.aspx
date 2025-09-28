<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PeriodoFiscal2.aspx.cs" Inherits="NominaRRHH.Presentacion.PeriodoFiscal2" %>

<%--<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="false" %>>--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="../Styles/periodoFiscal.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="relleno">
        <div class="alert alert-dismissible alert-warning" id="alertError" runat="server" visible="false">
            <button type="button" class="close" data-dismiss="alert">×</button>
            <asp:Label ID="lblAlert" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="alert alert-dismissible alert-success" id="alertSuccess" runat="server" visible="false">
            <button type="button" class="close" data-dismiss="alert">×</button>
            <asp:Label ID="lblSuccess" runat="server" Visible="false"></asp:Label>
        </div>
    </div>
    <div class="panel-heading">
        <h1 class="title">Gestión de Periodos Fiscales</h1>
    </div>

    <div class="box">

        <div class="subtitle">
            <h3 class="heading2">Crear Nuevo Periodo Fiscal</h3>
        </div>
        <div class="center">

            <asp:TextBox ID="txtAnioFiscal" CssClass=" text_form" runat="server" placeholder="Año fiscal" Enabled="false"></asp:TextBox>

            <div class="center_v2">
                <asp:TextBox ID="txtFechaInicio" CssClass=" text_form" autocomplete="off" runat="server" placeholder="Fecha inicio: dd/mm/yyyy" Enabled="false" />
                <cc1:MaskedEditExtender ID="msk_txtDate" runat="server" Mask="99/99/9999" TargetControlID="txtFechaInicio" />
                <asp:ImageButton ID="imgPopup" Width="30px" Height="30px" runat="server" ImageUrl="~/Images/calendar.gif" Enabled="false" />
                <cc1:CalendarExtender ID="Calendar1" runat="server" TargetControlID="txtFechaInicio" Format="dd/MM/yyyy" PopupButtonID="imgPopup" />
            </div>

            <asp:TextBox ID="txtNoPlanilla" CssClass="text_form" placeholder="No. de Planilla" runat="server" Enabled="false"></asp:TextBox>

            <asp:Button ID="btnCrearPeriodo" runat="server" Text="Crear Periodo" CssClass="btn btn-success align_right" Enabled="false" OnClick="btnCrearPeriodo_Click"/>

        </div>


    </div>


    <div class="box">

        <div class="subtitle">
            <h3 class="heading2">Periodos Fiscales Existentes</h3>
        </div>
        <div class="panel-body">
            <asp:GridView ID="gvPeriodosFiscales" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" GridLines="Vertical" BackColor="White" BorderColor="#005bb5" BorderStyle="None" BorderWidth="1px" OnRowCommand="gvPeriodosFiscales_RowCommand"
                AllowPaging="True" OnPageIndexChanging="gvPeriodosFiscales_PageIndexChanging" PageSize="3">
                <AlternatingRowStyle BackColor="#F8F9FA" Width="578px" />
                <Columns>
                    <asp:BoundField DataField="anioFiscal" HeaderText="Año Fiscal">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaini" HeaderText="Fecha de Inicio" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fechafin" HeaderText="Fecha de Final" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Estado" HeaderText="Estado">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="btnCerrar" runat="server" CssClass="btn btn-danger btn-xs" CommandName="CerrarPeriodo" CommandArgument='<%# Eval("anioFiscal") %>' Text="Cerrar Periodo" Visible='<%# Eval("estado").ToString() == "Abierto" %>' />

                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#18bc9c" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" CssClass="text-center" />
                <RowStyle BackColor="White" ForeColor="#333333" />
                <FooterStyle BackColor="#F8F9FA" ForeColor="Black" />
                <PagerStyle BackColor="#DCDCDC" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#0066cc" Font-Bold="True" ForeColor="White" />

            </asp:GridView>
        </div>
    </div>
</asp:Content>
