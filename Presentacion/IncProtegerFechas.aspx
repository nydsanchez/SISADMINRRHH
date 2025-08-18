<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncProtegerFechas.aspx.cs" Inherits="NominaRRHH.Presentacion.IncProtegerFechas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />    
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table>
    <tr><td>
                <asp:Label ID="Cabecera1" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue">Proteccion de Horas</asp:Label>
           </td></tr>
        <tr><td>
                <asp:Label ID="Cabecera2" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue">Proteccion de Horas</asp:Label>
            </td></tr>
    <tr><td>
                <asp:Label ID="Cabecera3" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue">Proteccion de Horas</asp:Label>
           </td></tr>

       <tr>
           <td colspan="2">
                <asp:Label ID="Cabecera" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue">Proteccion de Horas</asp:Label>
           </td>
       </tr>                
</table>
<table>
        <tr>            
            <td>
                <asp:Label ID="Cabecera00" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue">Codigo:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtCodigo" runat="server" AutoComplete="off" Width="69px"></asp:TextBox>
            </td>
            <td>                
                <asp:Button ID="BtnBuscarNombre" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="White" Text="?" BackColor="#0066CC" OnClick="BtnBuscarNombre_Click"/>
            </td>
            <td>
                <asp:TextBox ID="TxtNombre" runat="server" Width="281px" BackColor="#FFFFCC" ReadOnly="True"></asp:TextBox>    
            </td>
            <td>
                
            </td>
        </tr>
</table>
<table>
        <tr>                   
            <td class="style3">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue">Fecha Origen:</asp:Label>
            </td>
        </tr>
    </table>
<table>    
    <tr>
            <td>
                 <asp:DropDownList ID="ddlAnoI" runat="server">
                     <asp:ListItem Selected="True">2023</asp:ListItem>
                     <asp:ListItem>2024</asp:ListItem>
                     <asp:ListItem>2025</asp:ListItem>
                     <asp:ListItem>2026</asp:ListItem>
                     <asp:ListItem>2027</asp:ListItem>
                     <asp:ListItem>2028</asp:ListItem>
                     <asp:ListItem>2029</asp:ListItem>
                     <asp:ListItem>2030</asp:ListItem>
                 </asp:DropDownList>
                 <asp:DropDownList ID="ddlMesI" runat="server" CssClass="auto-style2" OnSelectedIndexChanged="ddlMesI_SelectedIndexChanged">
                     <asp:ListItem Selected="True" Value="1">Enero</asp:ListItem>
                     <asp:ListItem Value="2">Febrero</asp:ListItem>
                     <asp:ListItem Value="3">Marzo</asp:ListItem>
                     <asp:ListItem Value="4">Abril</asp:ListItem>
                     <asp:ListItem Value="5">Mayo</asp:ListItem>
                     <asp:ListItem Value="6">Junio</asp:ListItem>
                     <asp:ListItem Value="7">Julio</asp:ListItem>
                     <asp:ListItem Value="8">Agosto</asp:ListItem>
                     <asp:ListItem Value="9">Septiembre</asp:ListItem>
                     <asp:ListItem Value="10">Octubre</asp:ListItem>
                     <asp:ListItem Value="11">Noviembre</asp:ListItem>
                     <asp:ListItem Value="12">Diciembre</asp:ListItem>
                 </asp:DropDownList>
            </td>
             <td>
                <asp:DropDownList ID="ddlDiaI" runat="server">
                 </asp:DropDownList>
            </td>
        <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
</table>
    <table>
        <tr>            
            <td class="style3">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue">Fecha Destino:</asp:Label>

            </td>
        </tr>
    </table>
    <table>    
    <tr>
            <td>
                 <asp:DropDownList ID="ddlAnoF" runat="server">
                     <asp:ListItem Selected="True">2023</asp:ListItem>
                     <asp:ListItem>2024</asp:ListItem>
                     <asp:ListItem>2025</asp:ListItem>
                     <asp:ListItem>2026</asp:ListItem>
                     <asp:ListItem>2027</asp:ListItem>
                     <asp:ListItem>2028</asp:ListItem>
                     <asp:ListItem>2029</asp:ListItem>
                     <asp:ListItem>2030</asp:ListItem>
                 </asp:DropDownList>
                 <asp:DropDownList ID="ddlMesF" runat="server" CssClass="auto-style2" OnSelectedIndexChanged="ddlMesF_SelectedIndexChanged">
                     <asp:ListItem Selected="True" Value="1">Enero</asp:ListItem>
                     <asp:ListItem Value="2">Febrero</asp:ListItem>
                     <asp:ListItem Value="3">Marzo</asp:ListItem>
                     <asp:ListItem Value="4">Abril</asp:ListItem>
                     <asp:ListItem Value="5">Mayo</asp:ListItem>
                     <asp:ListItem Value="6">Junio</asp:ListItem>
                     <asp:ListItem Value="7">Julio</asp:ListItem>
                     <asp:ListItem Value="8">Agosto</asp:ListItem>
                     <asp:ListItem Value="9">Septiembre</asp:ListItem>
                     <asp:ListItem Value="10">Octubre</asp:ListItem>
                     <asp:ListItem Value="11">Noviembre</asp:ListItem>
                     <asp:ListItem Value="12">Diciembre</asp:ListItem>
                 </asp:DropDownList>
            </td>
             <td>
                <asp:DropDownList ID="ddlDiaF" runat="server">
                 </asp:DropDownList>
            </td>
        </tr>
</table>
    <table>
        <tr>        
            <td class="style2">
                <asp:Button ID="BtnBuscar" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="White" Text="Buscar" BackColor="#0066CC" OnClick="BtnBuscar_Click" />
            </td>
            <td class="style2">
                <asp:Button ID="BtnAplicar" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="White" Text="Aplicar" BackColor="#0066CC" OnClick="BtnAplicar_Click" />
            </td>
            
            <td class="style3">
                <asp:Label ID="LblMsg" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue"></asp:Label>

            </td>
        </tr>
    </table>
    <table>
        <tr>                    
            <td class="auto-style3">
                <asp:GridView ID="GVResult" class="table table-striped table-hover" runat="server"
                            AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical"
                            BorderStyle="None" BorderWidth="1px" EmptyDataText="No hay Datos">
                            <AlternatingRowStyle Width="100px" />
                            <Columns>
                                <asp:BoundField DataField="codigo_empleado" FooterText="Codigo" HeaderText="Codigo" />
                                <asp:BoundField DataField="nombrecompleto" HeaderText="Nombre" />
                                <asp:BoundField DataField="fechaini" DataFormatString="{0:d}" HeaderText="Desde" />
                                <asp:BoundField DataField="fechafin" DataFormatString="{0:d}" HeaderText="Hasta" />
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" CssClass="FooterStyle" />
                        </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

