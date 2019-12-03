﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="visitor_message_list.aspx.cs" Inherits="YTS.Web.admin.tools.visitor_message_list" ValidateRequest="false" %>

<%@ Import Namespace="YTS.Tools" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <title>访客留言</title>
    <link rel="stylesheet" type="text/css" href="../../scripts/artdialog/ui-dialog.css" />
    <link rel="stylesheet" type="text/css" href="../../css/pagination.css" />
    <link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
    <link rel="stylesheet" type="text/css" href="../../css/ColorStyle.css" />

    <script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
    <script type="text/javascript" src="../../../scripts/datepicker/WdatePicker.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
    <script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
    <script type="text/javascript" src="../../scripts/zrqYTSJs/Tools/alertBox.js"></script>
    <script type="text/javascript" src="../../scripts/zrqYTSJs/AdminPageJs/loading.js"></script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:history.back(-1);" class="back"><i class="iconfont icon-up"></i><span>返回上一页</span></a>
            <a href="../center.aspx" class="home"><i class="iconfont icon-home"></i><span>首页</span></a>
            <i class="arrow iconfont icon-arrow-right"></i>
            <span>访客留言</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div id="floatHead" class="toolbar-wrap">
            <div class="toolbar">
                <div class="box-wrap">
                    <a class="menu-btn"><i class="iconfont icon-more"></i></a>
                    <div class="l-list">
                        <ul class="icon-list">
                            <!--<li><a href="product_type_edit.aspx?action=<%=YTS.Tools.DTEnums.ActionEnum.Add %>"><i class="iconfont icon-close"></i><span>新增</span></a></li>-->
                            <li><a href="javascript:;" onclick="checkAll(this);"><i class="iconfont icon-check"></i><span>全选</span></a></li>
                            <li>
                                <asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return ExePostBack('btnDelete');"><i class="iconfont icon-delete"></i><span>删除</span></asp:LinkButton>
                            </li>
                        </ul>
                        <div class="menu-list flex">
                            <div class="lineBox">
                                <span>访客姓名</span>
                                <asp:TextBox ID="txt_Name" runat="server" CssClass="input" style="width: 100px;"></asp:TextBox>
                            </div>
                            <div class="lineBox">
                                <span>指定时间</span>
                                <asp:TextBox ID="txt_StartTime" runat="server" CssClass="input rule-date-input" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>-<asp:TextBox ID="txt_EndTime" runat="server" CssClass="input rule-date-input" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="r-list">
                        <asp:TextBox ID="txt_LikeSelect" runat="server" placeholder="模糊查询(多个内容用 , 间隔)" CssClass="input likeSelectInput"></asp:TextBox>
                        <asp:LinkButton ID="lbtnEmpty" runat="server" CssClass="btn-search" style="margin: 0;width: auto;">
                            <i class="iconfont icon-delete iyuan"></i>
                            <span class="span">清空</span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnSearch" runat="server" OnClientClick="window.Loading('请稍后')" CssClass="btn-search" style="margin: 0;width: auto;">
                            <i class="iconfont icon-search iyuan"></i>
                            <span class="span">查询</span>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--列表-->
        <div class="table-container">
            <asp:Repeater ID="rptList" runat="server">
                <HeaderTemplate>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                        <tr>
                            <th width="4%">选择</th>

                            <th width="auto">访客姓名</th>
                            <th width="auto">访客电话</th>
                            <th width="auto">访客所留内容</th>
                            <th width="auto">访客ip</th>

                            <th width="8%">添加时间</th>
                            <th width="8%">备注</th>
                            <!--<th width="8%">操作</th>-->
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                            <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />(<%#Eval("id")%>)
                        </td>

                        <td><%#Eval("name")%></td>
                        <td><%#Eval("tel")%></td>
                        <td><%#Eval("msg")%></td>
                        <td><%#Eval("ipaddress")%></td>

                        <td><%#string.Format("{0:G}",Eval("TimeAdd")).Replace('/','-')%></td>
                        <td><%#Eval("Remark")%></td>
                        <!--<td align="center"><a href="product_type_edit.aspx?action=<%#YTS.Tools.DTEnums.ActionEnum.Edit%>&id=<%#Eval("id")%>">修改</a></td>-->
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <!--/列表-->

        <!--内容底部-->
        <div class="line20"></div>
        <div class="pagelist">
            <div class="l-btns">
                <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);"  AutoPostBack="True"></asp:TextBox><span>条/页</span>
            </div>
            <div id="PageContent" runat="server" class="default"></div>
        </div>
        <!--/内容底部-->

    </form>
</body>
</html>
