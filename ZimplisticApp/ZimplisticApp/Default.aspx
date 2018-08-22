<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" ValidateRequest = "false" CodeBehind="Default.aspx.cs" Inherits="ZimplisticApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" onLoad="disablePage()">
   
        <div id="dvPassport">
            Select the option
            <asp:Button ID="XMLFileUpload" Text="FileUpload" runat="server" OnClick="XMLFileUpload_Click" />
            <asp:Button ID="XML_Text" Text="XML Text" runat="server" onClick="XML_Text_Click"/>

        </div>
        <div>
            Browse File
            <asp:FileUpload ID="FileUploadToServer" runat="server" Enabled="false"/>
            <br/>
        </div>
        <div>
            <asp:RadioButton id="GetListItemsByListNameV4" Text="GetListItemsByListNameV4" 
                runat="server" GroupName="apiName" OnCheckedChanged="apiList_CheckedChanged"/>
        </div>
        <div>
            <asp:RadioButton id="SearchOrdersV6" Text="SearchOrdersV6"
                 runat="server" GroupName="apiName" OnCheckedChanged="apiList_CheckedChanged"/>
        </div>
        <div>
            <asp:RadioButton id="GetOrderShippingRecords" Text="GetOrderShippingRecords"
                 runat="server" GroupName="apiName" OnCheckedChanged="apiList_CheckedChanged"/>
        </div>
        <div>
            <asp:RadioButton id="GetOrderV2" Text="GetOrderV2"
                 runat="server" GroupName="apiName" OnCheckedChanged="apiList_CheckedChanged"/>
        </div>
        <div>
            <asp:RadioButton id="InsertOrderV1" Text="InsertOrderV1"
                 runat="server" GroupName="apiName" OnCheckedChanged="apiList_CheckedChanged"/>
        </div>
        <div>
            <asp:Button ID="GetResponse" runat="server" Text="Get Response"  OnClick="GetResponse_Click" Enabled="false"/>
         </div>
        <br />
        <div class="col-md-5">
            <asp:TextBox ID="requestText" TextMode="multiline" Columns="80" Rows="30" runat="server" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="col-md-1">
            &nbsp;
        </div>
        <div class="col-md-6">
            <asp:TextBox ID="responseText" TextMode="multiline" Columns="80" Rows="30" runat="server" ReadOnly="true"></asp:TextBox>
        </div>
    
</asp:Content>
