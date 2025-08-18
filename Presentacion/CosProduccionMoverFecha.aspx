<%@ Page Title="Reportar Produccion" Language="C#"  AutoEventWireup="true" CodeBehind="CosProduccionMoverFecha.aspx.cs" Inherits="CRP.CosProduccionMoverFecha" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            width: 14px;
        }
        .style3
        {
            width: 354px;
        }
        .auto-style1 {
            width: 62px;
        }
        .auto-style2 {
            margin-right: 0px;
        }
    </style>
    <%-- <script src="Scripts/jquery.current.js" type="text/javascript"></script>
      <script src="Scripts/SHD_Common_2.js" type="text/javascript"></script>
       <script src="Scripts/adapt.min.js" type="text/javascript"></script>  --%>
</head>    
<body onload= "zoom()">
<form id="Form1" runat="server">
<table>
       <tr>
           <td colspan="2">
                <asp:Label ID="Cabecera" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue">
                    Recuadraje
 - Mover Fecha</asp:Label>
           </td>
       </tr>                
</table>
<table>
        <tr>        
            <td class="style2">
                <asp:TextBox ID="TxtLeerCaja" runat="server" 
                    AutoCompleteType="Disabled" Width="87px" ></asp:TextBox>
            </td>
            <td class="style3">
                <asp:Label ID="LblMsg" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue" Visible="False"></asp:Label>

            </td>
        </tr>
    </table>
<table>
    <tr>        
            <td>
                &nbsp;</td>
            <td>
                 <asp:DropDownList ID="ddlAno" runat="server">
                     <asp:ListItem Selected="True">2023</asp:ListItem>
                     <asp:ListItem>2024</asp:ListItem>
                     <asp:ListItem>2025</asp:ListItem>
                     <asp:ListItem>2026</asp:ListItem>
                     <asp:ListItem>2027</asp:ListItem>
                     <asp:ListItem>2028</asp:ListItem>
                     <asp:ListItem>2029</asp:ListItem>
                     <asp:ListItem>2030</asp:ListItem>
                 </asp:DropDownList>
                 <asp:DropDownList ID="ddlMes" runat="server" CssClass="auto-style2" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged">
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
                <asp:DropDownList ID="ddlDia" runat="server">
                 </asp:DropDownList>
            </td>
        </tr>
</table>
</form>
</body>
<script type="text/javascript">

    function zoom() {
        document.body.style.zoom = "500%"
    }

</script>
</html>