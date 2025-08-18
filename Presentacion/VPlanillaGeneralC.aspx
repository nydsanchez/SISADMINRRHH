<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VPlanillaGeneralC.aspx.cs" Inherits="NominaRRHH.Presentacion.VPlanillaGeneralC" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
                        <label class="control-label" for="focusedInput">Informes de Planilla</label>
                    </div>
                     </div>
                <div class="row">                 
                    <div class="col-md-12">
                 <%--        <div class="col-md-2">
                            <asp:Label  class="control-label"  for="focusedInput" ID="Label3" runat="server" Text="Tipo de Reportes" style="font-weight: 700"></asp:Label>
                            <asp:DropDownList class="form-control" ID="ddlTipoReporte" runat="server">                              
                                <asp:ListItem Value="1">Empleados en Negativo</asp:ListItem>
                                <asp:ListItem Value="2">Detalle de Planilla</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                           <asp:Label  class="control-label"  for="focusedInput" ID="Label2" runat="server" Text="Tipo de Planilla" style="font-weight: 700"></asp:Label>
                            <asp:DropDownList class="form-control" ID="ddlTipoPlanilla" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoPlanilla_SelectedIndexChanged">                              
                                <asp:ListItem Value="1">Catorcenal</asp:ListItem>
                                <asp:ListItem Value="2">Quincenal</asp:ListItem>
                                <asp:ListItem Value="4">Vacaciones</asp:ListItem>
                                <asp:ListItem Value="5">Aguinaldo</asp:ListItem>
                            </asp:DropDownList>
                        </div>--%>
                        <div class="col-md-2">
                           <asp:Label  class="control-label"  for="focusedInput" ID="Label1" runat="server" Text="Periodo 1" style="font-weight: 700"></asp:Label>
                            <asp:TextBox ID="txtPeriodo" class="form-control" placeholder="Digite un Periodo" onkeypress="return soloNumeros(event)"
                                runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                           <div class="col-md-2" runat="server" visible="false" id="divp">
                           <asp:Label  class="control-label"  for="focusedInput" ID="Label2" runat="server" Text="Periodo 2" style="font-weight: 700"></asp:Label>
                            <asp:TextBox ID="txtPeriodo2" class="form-control" placeholder="Digite un Periodo" onkeypress="return soloNumeros(event)"
                                runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                           <div class="col-xs-2">
                            <%--<div class="col-md-1 marginChkActivo">--%>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="ChkConsolida" runat="server" OnCheckedChanged="ChkConsolida_CheckedChanged" Checked="false" AutoPostBack="true" />
                                    Consolidar Periodos
                                </label>
                            </div>
                            <%--</div>--%>
                        </div>
                             <div class="col-xs-1">
                            <%--<div class="col-md-1 marginChkActivo">--%>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="ChkAll" runat="server" OnCheckedChanged="ChkAll_CheckedChanged" Checked="true" AutoPostBack="true" />
                                    Tarjetas
                                </label>
                            </div>
                            <%--</div>--%>
                        </div>
                        <div class="col-xs-2">
                            <%--<div class="col-md-1 marginChkActivo">--%>
                            <div class="checkbox">
                                <label>
                                    <asp:CheckBox ID="ChkEfectivo" runat="server" Checked="false" OnCheckedChanged="ChkEfectivo_CheckedChanged" AutoPostBack="true" />
                                    Todos
                                </label>
                            </div>
                            <%--</div>  --%>
                        </div>
                    
                       <%-- <div class="col-md-2">
                           
                            <asp:Label  class="control-label"  for="focusedInput" ID="lblSemana" runat="server" Text="Semana" style="font-weight: 700"></asp:Label>
                    <asp:DropDownList class="form-control" ID="ddlTipo" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem Value="10">Consolidado</asp:ListItem>
                            </asp:DropDownList>
                        </div>--%>

                        <div class="col-md-3">
                            <asp:Button ID="btnProcesar" Class="btn btn-success" runat="server" Style="margin-top: 25px" Text="Aceptar" OnClick="btnProcesar_Click" />
                        </div>
                    </div>
                </div><br />
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <div class="row">
                    <div class="col-md-12">
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1122px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" ProcessingMode="Remote">
                        </rsweb:ReportViewer>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
     
            function soloNumeros(e) {
                key = e.keyCode || e.which;
                tecla = String.fromCharCode(key).toLowerCase();
                letras = "0123456789./";
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

