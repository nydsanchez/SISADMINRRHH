<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mantenimiento.aspx.cs" Inherits="NominaRRHH.Presentacion.Mantenimiento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mar-top">
        <div class="panel panel-info">
            <div class="panel-body">
                <div id="divAlerts">
                    <div class="alert alert-dismissible alert-warning" id="alertValida" runat="server" visible="false">
                        <button type="button" class="close" data-dismiss="alert">×</button>
                        <asp:Label ID="lblAlert" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div class="alert alert-dismissible alert-success" id="alertSucces" runat="server" visible="false">
                        <button type="button" class="close" data-dismiss="alert">×</button>
                        <asp:Label ID="LblSuccess" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:MultiView ID="mvPermisos" runat="server">
                    <asp:View ID="vwPanelUsuario" runat="server">
                        <div style="margin-left: -210px;">
                            <asp:Panel ID="pnlUser" runat="server" class="card" Style="margin-left: 560px;">
                                <fieldset style="margin-left: -310px;">
                                    <legend>Usuario</legend>
                                    <div class="col m12 s12 mbot-30">
                                        <asp:Literal ID="ltMsgPanel" runat="server"></asp:Literal>
                                    </div>
                                    <div class="row" style="margin-left: 285px;">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <label id="lblnombre" runat="server" class="control-label" for="focusedInput">
                                                            Nombres</label>
                                                        <asp:TextBox ID="tbNombre" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label id="lblApellido" runat="server" class="control-label" for="focusedInput">
                                                            Apellidos</label>
                                                        <asp:TextBox ID="tbApellido" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 8px;">
                                                <div class="col-md-12">
                                                    <div class="col-md-3" id="User">
                                                        <label id="lblUsuario" class="control-label" runat="server" for="focusedInput">
                                                            Usuario</label>
                                                        <asp:TextBox ID="tbUsuario" class="form-control" runat="server"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-3" id="pass">
                                                        <label id="lblPass" runat="server" for="focusedInput" class="control-label">
                                                            Contraseña</label>
                                                        <asp:TextBox ID="tbPass" class="form-control" runat="server" TextMode="Password"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 8px;">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <label for="focusedInput" class="control-label">Perfil</label>
                                                        <asp:DropDownList ID="dllPerfil" class="form-control" runat="server"
                                                            DataTextField="Descripcion" DataValueField="IdPerfil">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-1 marginChkActivo">
                                                        <div class="checkbox">
                                                            <label class="control-label">
                                                                <asp:CheckBox ID="cbactivoU" runat="server" AutoPostBack="false" Checked='<%#Convert.ToBoolean(Eval("activou")) %>' />
                                                                <strong>Activo</strong>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12" style="margin-left: 60px;">
                                                    <asp:Button ID="btnIngresar" runat="server" class="waves-effec btn btn-primary botonPnl blue darken-3"
                                                        Text="Guardar" OnClick="btnIngresar_Click" />
                                                    <asp:Button ID="BtnActualizar" runat="server" class="btn btn-success botonPnl blue darken-3"
                                                        Text="Actualizar" OnClick="BtnActualizar_Click" />
                                                    <asp:Button ID="BtnCancelar" runat="server" class="btn btn-danger botonPnl grey darken-1"
                                                        Text="Cancelar" OnClick="BtnCancelar_Click1" CausesValidation="False" />
                                                    <asp:Button ID="btnCerrar" runat="server" class="btn btn-danger botonPnl teal darken-1"
                                                    Text="Cerrar" OnClick="btnCerrar_Click" CausesValidation="False" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </asp:Panel>
                        </div>
                    </asp:View>
                    <asp:View ID="wvUsuario" runat="server">
                        <div class="card-panel white">
                            <div class="row">
                                <div class="col s12">
                                    <asp:ImageButton ID="BubtnRolesvu" runat="server" class="waves-effec botonPnl blue darken-3" Style="margin-left: 70px; margin-bottom: -58px;"
                                        OnClick="BubtnRolesvu_Click" ImageUrl="~/Images/roles.png" Enabled="True" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col s12">
                                    <div style="margin-left: 20px;">
                                        <asp:ImageButton ID="btnCrearNuevaU2" runat="server" Height="40px" ImageUrl="~/Images/1438913241_user_add.png" type="button"
                                            Width="40px" data-position="right" data-delay="20" data-toggle="tooltip" data-placement="top" title="" data-original-title="Tooltip on top"
                                            OnClick="btnCrearNuevaU2_Click" />
                                        <asp:Button ID="popup" runat="server" Text="Cerrar" Style='display: none' />
                                        <cc1:ModalPopupExtender ID="btnCrearNuevaU_MPExtender" runat="server" TargetControlID="popup"
                                            PopupControlID="pnlUser" BackgroundCssClass="ModalPopupBG" DynamicServicePath=""
                                            Enabled="True">
                                        </cc1:ModalPopupExtender>
                                    </div>
                                    <asp:Literal ID="ltMessage" runat="server"></asp:Literal><div class="hoverable">
                                        <asp:GridView ID="gvUser" runat="server" EmptyDataText="No hay Usuarios" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center" DataKeyNames="idUsuario,nombre,apellido,activou,Activo,IdPerfil,pass"
                                            AllowPaging="True" CssClass="table table-striped col s12 m12" OnPageIndexChanging="gvUser_PageIndexChanging"
                                            GridLines="Vertical" BackColor="White" BorderStyle="None" BorderWidth="1px">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Estado">
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgActivoInactivo" runat="server" ImageUrl='<%# (Eval("activou").Equals(true) ? "~/Images/Verde.png" : (Eval("activou").Equals(false) ? "~/Images/Rojo.png" : "~/Images/Rojo.png"))%>'
                                                            Height="15px" Width="15px" CssClass="tooltipped" data-position="right" data-delay="10"
                                                            data-tooltip='<%# (Eval("activou").Equals(true) ? "Activo" : (Eval("activou").Equals(false) ? "Inactivo" : "Inactivo"))%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                                                <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
                                                <asp:BoundField DataField="Descripcion" HeaderText="Rol" />
                                                <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgEdit" runat="server" Height="25px" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            ImageUrl="~/Images/userEdit.png" Width="25px" OnClick="imgEdit_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Detalle">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgDetails" runat="server" Height="28px" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            ImageUrl="~/Images/UserDetail.png" Width="28px" OnClick="imgDetails_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                Página
                                    <asp:DropDownList ID="paginasDropDownList" Font-Size="12px" AutoPostBack="true" OnSelectedIndexChanged="GoPageu"
                                        runat="server">
                                    </asp:DropDownList>
                                                de
                                    <asp:Label ID="lblTotalNumberOfPages" runat="server" />&#160;&#160;
                                    <asp:Button ID="Button4" runat="server" CommandName="Page" ToolTip="Prim. Pag" CommandArgument="First"
                                        CssClass="pagfirst" /><asp:Button ID="Button1" runat="server" CommandName="Page"
                                            ToolTip="Pág. anterior" CommandArgument="Prev" CssClass="pagprev" /><asp:Button ID="Button2"
                                                runat="server" CommandName="Page" ToolTip="Sig. página" CommandArgument="Next"
                                                CssClass="pagnext" /><asp:Button ID="Button3" runat="server" CommandName="Page" ToolTip="Últ. Pag"
                                                    CommandArgument="Last" CssClass="paglast" />
                                            </PagerTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vwpnlRoles" runat="server">
                        <div style="margin-left: -210px;">
                            <asp:Panel ID="pnlRol" runat="server" class="card " Style="margin-left: 560px;">
                                <fieldset style="margin-left: -310px;">
                                    <legend>Roles</legend>
                                    <div class="col m12 s12 mbot-30">
                                        <asp:Literal ID="ltPanelR" runat="server"></asp:Literal>
                                    </div>
                                    <div class="row" style="margin-left: 285px;">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-3" id="rol">
                                                        <label id="lblNombreRol" class="control-label" runat="server" for="focusedInput">
                                                            Nombre Rol</label>
                                                        <asp:TextBox ID="tbNombreRol" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                     <div class="col-md-1 marginChkActivo">
                                                        <div class="checkbox">
                                                            <label>
                                                                <asp:CheckBox ID="cbActivoR" runat="server" AutoPostBack="false" Checked='<%#Convert.ToBoolean(Eval("activou")) %>' />
                                                                <strong>Activo</strong>
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="Panel3" runat="server" Width="500px" Height="350px" ScrollBars="Auto">
                                                        <asp:TreeView ID="tvOpcionesR" runat="server" ShowCheckBoxes="All" OnSelectedNodeChanged="tvOpcionesR_SelectedNodeChanged">
                                                        </asp:TreeView>
                                                    </asp:Panel>
                                                </div>
                                            </div><br />
                                            <div class="col-md-12">
                                                <asp:Button ID="btnGuardarRol" runat="server" class="waves-effec btn btn-primary botonPnl blue darken-3"
                                                    Text="Guardar" OnClick="btnGuardarRol_Click" />
                                                <asp:Button ID="btnActualizarRol" runat="server" class="btn btn-primary botonPnl blue darken-3"
                                                    Text="Actualizar" OnClick="btnActualizarRol_Click" />
                                                <asp:Button ID="btnCancelarRol" runat="server" class="btn btn-danger botonPnl grey darken-1"
                                                    Text="Cancelar" OnClick="btnCancelarRol_Click" CausesValidation="False" />
                                                <asp:Button ID="btnCerrarRol" runat="server" class="btn btn-primary botonPnl teal darken-1"
                                                    Text="Cerrar" OnClick="btnCerrarRol_Click" CausesValidation="False" />
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </asp:Panel>
                        </div>
                    </asp:View>
                    <asp:View ID="wvRol" runat="server">
                        <div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <asp:Button ID="popupR" runat="server" Style="display: none" Text="Cerrar" />
                                        <asp:ImageButton ID="ImageButton1" runat="server" data-delay="20"
                                            data-position="right" ImageUrl="~/Images/roles.png"
                                            OnClick="btnp_Click" />
                                        <cc1:ModalPopupExtender ID="btnCrearRol_MPExtender" runat="server" BackgroundCssClass="ModalPopupBG"
                                            DynamicServicePath="" Enabled="True" PopupControlID="pnlRol" TargetControlID="popupR">
                                        </cc1:ModalPopupExtender>
                                    </div>
                                    <div class="col-md-2" style="margin-top: 20px; margin-left: -118px;">
                                        <asp:Button ID="btnUsuariosvr" runat="server" Text="Regresar" OnClick="btnUsuariosvr_Click"
                                            class="waves-effec btn btn-link " />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="hoverable">
                                        <asp:Literal ID="LtMsgRo" runat="server"></asp:Literal>
                                        <asp:GridView ID="gvPerfil" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            BackColor="White" BorderStyle="None" BorderWidth="1px" CssClass="table table-striped col s12 m12"
                                            DataKeyNames="idPerfil,Activo" EmptyDataText="No hay Perfiles" GridLines="Vertical"
                                            HorizontalAlign="Center" Width="100%" OnPageIndexChanging="gvPerfil_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Estado">
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgActivoInactivo" runat="server" CssClass="tooltipped" data-delay="10"
                                                            data-position="right" data-tooltip='<%# (Eval("Activo").Equals(true) ? "Activo" : (Eval("Activo").Equals(false) ? "Inactivo" : "Inactivo"))%>'
                                                            Height="15px" ImageUrl='<%# (Eval("Activo").Equals(true) ? "~/Images/Verde.png" : (Eval("Activo").Equals(false) ? "~/Images/Rojo.png" : "~/Images/Rojo.png"))%>'
                                                            Width="15px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Descripcion" HeaderText="Rol" />
                                                <asp:TemplateField HeaderText="Editar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgEditR" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Height="25px" ImageUrl="~/Images/userEdit.png" OnClick="imgEditR_Click" Width="25px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Detalle">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgDetailsR" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Height="28px" ImageUrl="~/Images/UserDetail.png" OnClick="imgDetailsR_Click"
                                                            Width="28px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                Página
                                    <asp:DropDownList ID="paginasDropDownList" runat="server" AutoPostBack="true" Font-Size="12px"
                                        OnSelectedIndexChanged="GoPageR">
                                    </asp:DropDownList>
                                                de
                                    <asp:Label ID="lblTotalNumberOfPages" runat="server" />&nbsp;&nbsp;
                                    <asp:Button ID="Button4" runat="server" CommandArgument="First" CommandName="Page"
                                        CssClass="pagfirst" ToolTip="Prim. Pag" /><asp:Button ID="Button1" runat="server"
                                            CommandArgument="Prev" CommandName="Page" CssClass="pagprev" ToolTip="Pág. anterior" />
                                                <asp:Button ID="Button2" runat="server" CommandArgument="Next" CommandName="Page"
                                                    CssClass="pagnext" ToolTip="Sig. página" /><asp:Button ID="Button3" runat="server"
                                                        CommandArgument="Last" CommandName="Page" CssClass="paglast" ToolTip="Últ. Pag" />
                                            </PagerTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
                <%-- Aqui hiba el form --%>
                <%-- </div>--%>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //INICIALIZACION PARA TOOLTIP

        //        $('.tooltipped').tooltip({ delay: 50 });

        //METODOS PARA SELECCION CHECKBOX
        function OnTreeClick(evt) {
            var src = window.event != window.undefined ? window.event.srcElement : evt.target
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick) {
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                {
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                    {
                        //check or uncheck children at all levels
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
                //check or uncheck parents at all levels
                CheckUncheckParents(src, src.checked);
            }
        }

        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }

        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;

            if (parentNodeTable) {
                var checkUncheckSwitch;

                if (check) //checkbox checked
                {
                    checkUncheckSwitch = true;
                }
                else //checkbox unchecked
                {
                    var isAllSiblingsUnChecked = AreAllSiblingsUnChecked(srcChild);
                    if (!isAllSiblingsUnChecked)
                        checkUncheckSwitch = true;
                    else
                        checkUncheckSwitch = false;
                }

                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;
                    //do the same recursively
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function AreAllSiblingsUnChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (prevChkBox.checked) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }
    </script>
</asp:Content>


