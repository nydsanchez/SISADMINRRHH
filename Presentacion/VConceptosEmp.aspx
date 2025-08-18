<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VConceptosEmp.aspx.cs" Inherits="NominaRRHH.Presentacion.VConceptosEmp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
                    <div class="col-md-4">
                        <label class="control-label" for="focusedInput">Lista de Ingresos y Deducciones</label>
                    </div>
                    <div class="col-md-12">                       
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Tipo de Asignacion</label>
                            <asp:DropDownList class="form-control" ID="ddlTipo" runat="server" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="0"> Seleccione Una Opcion</asp:ListItem>
                                <asp:ListItem Value="1">Ingresos</asp:ListItem>
                                <asp:ListItem Value="2">Egresos</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Concepto</label>
                            <asp:DropDownList class="form-control" ID="ddlConceptos" runat="server">                              
                            </asp:DropDownList>
                        </div>                       
                         <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Periodo</label>
                            <asp:TextBox ID="txtperiodo" class="form-control" placeholder="Digite el Periodo"
                                runat="server" autocomplete="off" Width="150px"></asp:TextBox>
                         </div>
                          <div class="col-md-2">
                            <label class="control-label" for="focusedInput">Semana</label>
                            <asp:DropDownList class="form-control" ID="ddlSemana" runat="server" Width="150px">  
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>                            
                            </asp:DropDownList>
                        </div> 
                        <div class="col-md-3">
                            <asp:Button ID="btnProcesar" Class="btn btn-success" runat="server" Style="margin-top: 25px" Text="Aceptar" OnClick="btnProcesar_Click" />
                        </div>
                    </div>
                </div><br />
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <div class="row">
                    <div class="col-md-12">
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="CompanicsaRHDataSetTableAdapters.spPreplanillaIngTableAdapter"></asp:ObjectDataSource>
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1122px" Font-Names="Verdana" Font-Size="8pt" ShowPrintButton="True" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" ProcessingMode="local">
                            <ServerReport Timeout="900000" />
                        </rsweb:ReportViewer>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>