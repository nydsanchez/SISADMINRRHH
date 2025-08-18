<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonalIncFueraLayout.aspx.cs" Inherits="NominaRRHH.Presentacion.PersonalIncFueraLayout" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <link href="../Content/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datetimepicker.js"></script>
    <style type="text/css">
        .auto-style1 {
            left: 0px;
            top: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


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
       <div class="col-md-2">
                        <asp:Label class="control-label" for="focusedInput" ID="Label3" runat="server" Text="Periodo" Style="font-weight: 700"></asp:Label>
                        <asp:TextBox ID="txtPeriodo" class="form-control" placeholder="Digite un Periodo" 
                            runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                            <div class="col-md-2">
                              <asp:CheckBox runat="server" ID="ckfiltro" Checked="true"/>
                        <asp:Label class="control-label" for="focusedInput" ID="Label1" runat="server" Text="Mostrar solo variaciones" Style="font-weight: 700"></asp:Label>
                        
                    </div>
                     <div class="col-md-3">
                            <div class="col-md-3 col-sm-3 col-xs-5">
                                <asp:Button ID="Button4" Class="btn btn-success" runat="server" Text="Consultar" OnClick="Button4_Click" />
                            </div>
                        </div>
                    
                       
                    </div>
                <br />
                 <div class="row" visible="false" runat="server" id="div2">
                     <label class="control-label" for="focusedInput">Personal fuera del Layout</label>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1121px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                    </rsweb:ReportViewer>
                </div>
            </div>

        </div>
    </div>

    <script type="text/javascript">

        $(function () {
            $('#datePicker').datepicker({
                format: 'dd/mm/yyyy'
            })
                .on('changeDate', function (e) {
                    $('.datepicker dropdown-menu').hide();
                });

            $('#datePicker2').datepicker({
                format: 'dd/mm/yyyy'
            })
                .on('changeDate', function (e) {
                    $('.datepicker2 dropdown-menu').hide();
                });

        });
    </script>
</asp:Content>

