﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebClient.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        input[type=text], input[type=password]
        {
            width: 200px;
        }
        table
        {
            border: 1px solid #ccc;
        }
        table th
        {
            background-color: #F7F7F7;
            color: #333;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            border-color: #ccc;
        }
    </style>
</head>
<body>

    <form id="TextCheckbox Example" METHOD="GET" ACTION="text.asp"> 
  <H3>Superserver!</H3> 
  <p> 
  <table> 
  <tr> 
  <td>Webport:</td> 
  <td><INPUT TYPE="TEXT" NAME="webport" VALUE="" SIZE="20" MAXLENGTH="150"></td> 
  </tr><tr> 
  <td>Webroot:</td> 
  <td><INPUT TYPE="TEXT" NAME="webroot" VALUE="" SIZE="25" MAXLENGTH="150"></td> 
  </tr><tr> 
  <td>Default page:</td> 
  <td><INPUT TYPE="TEXT" NAME="defaultpage" VALUE="" SIZE="25" MAXLENGTH="150"></td> 
  </tr> 
  </table> 
  </p> 
  <p> 
  Directory browsing:<BR> 
  <INPUT TYPE="CHECKBOX" NAME= "info" VALUE="dbon"><BR> 
  </p> 
  <p> 
  <input type="submit" VALUE="Show log">
  <INPUT TYPE="submit" VALUE="Save"> 
  <INPUT TYPE="submit" VALUE="Cancel"> 
  </p> 
  </form> 
</body>
</html>
