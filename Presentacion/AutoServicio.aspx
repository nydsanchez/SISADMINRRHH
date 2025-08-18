<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AutoServicio.aspx.cs" Inherits="NominaRRHH.Presentacion.AutoServicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/font-awesome.css" rel="stylesheet" />
    <link href="../Content/Botones.css" rel="stylesheet" />
    <link href="../Content/font-awesome.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="container mar-top">
            <div class="col-lg-3col-xs-3 col-md-3 col-sm-3">
                <div class="panel-body btn2">
                    <i class="fa fa-money" aria-hidden="true"></i>
                   <%-- <button class="btn2 btn2-circle">
                        <div class="ripple-container">
                            <span class="ripple-effect glyphicon glyphicon-align-left"></span>
                        </div>
                        
                    </button>--%>
                </div>
            </div>
            <div class="col-lg-3 col-xs-3 col-md-3 col-sm-3">
                <div class="panel-body">
                    <asp:Button ID="Button3" runat="server" Text="" class="btn2 btn2-circle" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <div class="row">
                    <div class="container mar-top">
                        <div class="panel  col-lg-12 col-xs-12 col-md-12 col-sm-12" style="border-color: #eaf4fb;">
                            <div class="panel-body">
                               
                            </div>
                        </div>
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>

</asp:Content>
