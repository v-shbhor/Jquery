<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuForm.aspx.cs" Inherits="JqueryBasics.MenuForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src=" jquery-1.11.3.js"></script>
    <!-- the below two file are downloaded form the jqueryui.com required for the autotextcomplete feature-->
    <script src="jquery-ui.js"></script>
    <link href="jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {

            $.ajax({
                url: 'MenuHandler.ashx',
                method: 'get',
                dataType: 'json',
                success: function (data) {
                    buildmenu($('#menu'), data);
                    $('#menu').menu();
                }

            });

            function buildmenu(parent, items) {
                $.each(items, function () {
                    var li = $('<li>' + this.menutext + '</li>');
                    if(!this.active)
                    {
                        li.addClass('ui-state-disabled');
                    }
                    li.appendTo(parent);

                    if(this.List && this.List.length > 0)
                    {
                        var ul = $('<ul></ul>');
                        ul.appendTo(li);
                        buildmenu(ul,this.List);
                    }
                });
            } 
        });
        </script>
</head>
<body style="font-family: Arial">
    <form id="form1" runat="server">
    <div style="width:150px">
    <ul id="menu"></ul>        
    </div>
    </form>
</body>
</html>
