﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ASPXPAGE_JQUERY.aspx.cs" Inherits="JqueryBasics.ASPXPAGE_JQUERY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src=" jquery-1.11.3.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btngetemployee').click(function () {
                var empid = $('#txtid').val();
                $.ajax({
                    url: 'ASPXPAGE_JQUERY.aspx/getemployeebyid',
                    method: 'post',
                    contentType: 'application/json',
                    data: '{empid:' + empid + '}',
                    dataType: 'json',
                    success: function (data) {
                        $('#txtname').val(data.d.name);
                        $('#txtgender').val(data.d.gender);
                        $('#txtsalary').val(data.d.salary);

                    },
                    error: function (error) {
                        alert(error);
                    }
                });
            });

            $('#btngetemployee1').click(function () {
                var empid = $('#txtid1').val();
                $.ajax({
                    url: 'EmployeeService.svc/getemployeebyid',
                    method: 'post',
                    contentType: 'application/json',
                    data: JSON.stringify({employeeid: empid}),
                    dataType: 'json',
                    success: function (data) {
                        $('#txtname1').val(data.d.name);
                        $('#txtgender1').val(data.d.gender);
                        $('#txtsalary1').val(data.d.salary);

                    },
                    error: function (error) {
                        alert(error);
                    }
                });
            });

            //query live weather data
            $('#btngetweather').click(function () {
                var requestdata = $('#txtcity').val() + ',' + $('#txtcountry').val();
                var resultemenent = $('#resultdiv');
                $.ajax({
                    url: 'http://api.openweathermap.org/data/2.5/weather',
                    method: 'get',
                    data: {q: requestdata},
                    dataType: 'json',
                    success: function (data) {
                        resultemenent.html('Weather: ' + data.weather[0].main + '<br/>' +
                            'Description: ' + data.weather[0].description);
                    }
                });
            });
        });
        </script>
</head>
<body>
    <form id="form1" runat="server">
    ID:
        <input id="txtid" type="text" style="86px" />
        <input type="button" id="btngetemployee" value="get employee - ASPX" />
        <br /><br />
        <table border="1" style="border-collapse:collapse">
            <tr>
                <td>name</td>
                <td><input id="txtname" type="text" /></td>
            </tr>
            <tr>
                <td>gender</td>
                <td><input id="txtgender" type="text" /></td>
            </tr>
            <tr>
                <td>name</td>
                <td><input id="txtsalary" type="text" /></td>
            </tr>

        </table>
        <br /><br /><br />
        <input id="txtid1" type="text" style="86px" />
        <input type="button" id="btngetemployee1" value="get employee - WCF" />
        <br /><br />
        <table border="1" style="border-collapse:collapse">
            <tr>
                <td>name</td>
                <td><input id="txtname1" type="text" /></td>
            </tr>
            <tr>
                <td>gender</td>
                <td><input id="txtgender1" type="text" /></td>
            </tr>
            <tr>
                <td>name</td>
                <td><input id="txtsalary1" type="text" /></td>
            </tr>

        </table>


        <table border="1" style="border-collapse:collapse">
            <tr>
                <td>City</td>
                <td><input id="txtcity" type="text" /></td>
            </tr>
            <tr>
                <td>Country</td>
                <td><input id="txtcountry" type="text" /></td>
            </tr>
            </table>
        <input type="button" id="btngetweather" value="get weather live data" />
        <br /><br />
        <div id="resultdiv">

        </div>
    </form>
</body>
</html>
