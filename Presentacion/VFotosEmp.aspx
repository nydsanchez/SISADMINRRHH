<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VFotosEmp.aspx.cs" Inherits="NominaRRHH.Presentacion.VFotosEmp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
    <link href="../Content/standalone.css" rel="stylesheet" />
    <link href="../Content/Styles.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <link href="../Content/fileinput.css" rel="stylesheet" />
    <script src="../Scripts/fileinput.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top">
        <div class="panel panel-info">
            <div class="panel-body" style="margin-top: 0px">
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
                        <div class="col-md-6">
                            <label class="control-label" for="focusedInput"><strong>Fotos Personal</strong></label>
                        </div>
                    </div>
                </div>
                 <div class="row">
                    <div class="col-md-12" >
                    <div class="col-md-2" >
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="ChkExcel" runat="server" AutoPostBack="True" OnCheckedChanged="ChkExcel_CheckedChanged" Checked="false"/>
                                            <strong>¿Cargar excel con empleados?</strong>
                                        </label>
                                    </div>
                                </div> 
                        </div>
                     </div>
                <div class="row" id="divdpto" runat="server" visible="true">
                    <div class="col-md-12" >
                        <div class="col-md-3">
                            <label class="control-label" runat="server" for="focusedInput">Departamento</label>
                            <asp:DropDownList class="form-control" ID="ddlDepto1" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label class="control-label" runat="server" for="focusedInput">Departamento</label>
                            <asp:DropDownList class="form-control" ID="ddlDepto2" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                   
                </div>
                 <div class="row" id="divexcel" runat="server" visible="false">
                         <div class="col-md-12" style="margin-left: 50px; margin-top: 15px;">
                        <div class="col-md-6" style="margin-left: 110px; margin-top: 15px;">
                            <label class="control-label" for="focusedInput">Cargar Archivo de Empleados</label>
                            <asp:FileUpload ID="file" class="file" runat="server" />
                        </div>
                        <div class="col-md-2" style="margin-left: 15px; margin-top: 18px;">
                            <asp:Button ID="btnCargar" Class="btn btn-success" Style="margin-top: 22px;" runat="server" Text="Cargar"  OnClick="btnCargar_Click" />
                        </div>                      
                    </div>
                            </div>
                <div class="row">
                     <div class="col-md-3">
                        <asp:Button ID="btnMostrar" Visible="true" Class="btn btn-info" runat="server" Style="margin-top: 25px" Text="Aceptar" OnClick="btnMostrar_Click" />
                    </div>
                    </div>
            </div>
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="row">
                <div class="col-md-12">
                    <div>
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="771px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <LocalReport ReportEmbeddedResource="NominaRRHH.Reportes.Ingresos.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
