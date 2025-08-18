<%@ Page Title="Reportar Produccion" Language="C#"  AutoEventWireup="true" CodeBehind="CosProduccionMoverFechaRango.aspx.cs" Inherits="CRP.CosProduccionMoverFechaRango" %>
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
        .auto-style2 {
            margin-right: 0px;
        }
        .auto-style3 {
            width: 300px;
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
            <td class="style3">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue">Fecha Origen:</asp:Label>

            </td>
        </tr>
    </table>
<table>
        <tr>            
            <td>
                &nbsp;</td><td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
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
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue">Modulo:</asp:Label>

            </td>
            <td>
                <asp:DropDownList ID="ddlModulo" runat="server">
                    <asp:ListItem>NO</asp:ListItem>
                    <asp:ListItem>01</asp:ListItem>
                    <asp:ListItem>02</asp:ListItem>
                    <asp:ListItem>03</asp:ListItem>
                    <asp:ListItem>04</asp:ListItem>
                    <asp:ListItem>05</asp:ListItem>
                    <asp:ListItem>06</asp:ListItem>
                    <asp:ListItem>07</asp:ListItem>
                    <asp:ListItem>08</asp:ListItem>
                    <asp:ListItem>09</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>13</asp:ListItem>
                    <asp:ListItem>14</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>16</asp:ListItem>
                    <asp:ListItem>17</asp:ListItem>
                    <asp:ListItem>18</asp:ListItem>
                    <asp:ListItem>19</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>21</asp:ListItem>
                    <asp:ListItem>22</asp:ListItem>
                    <asp:ListItem>23</asp:ListItem>
                    <asp:ListItem>24</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>26</asp:ListItem>
                    <asp:ListItem>27</asp:ListItem>
                    <asp:ListItem>28</asp:ListItem>
                    <asp:ListItem>29</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                    <asp:ListItem>31</asp:ListItem>
                    <asp:ListItem>32</asp:ListItem>
                    <asp:ListItem>33</asp:ListItem>
                    <asp:ListItem>34</asp:ListItem>
                    <asp:ListItem>35</asp:ListItem>
                    <asp:ListItem>36</asp:ListItem>
                    <asp:ListItem>37</asp:ListItem>
                    <asp:ListItem>38</asp:ListItem>
                    <asp:ListItem>39</asp:ListItem>
                    <asp:ListItem>40</asp:ListItem>
                    
</asp:DropDownList>
            </td>
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
                <asp:GridView ID="GVResult" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</form>
</body>
<script type="text/javascript">
</script>
</html>