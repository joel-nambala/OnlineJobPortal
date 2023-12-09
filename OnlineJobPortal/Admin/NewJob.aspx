<%@ Page Title="New Job" Language="C#" MasterPageFile="~/Layouts/Admin.Master" AutoEventWireup="true" CodeBehind="NewJob.aspx.cs" Inherits="OnlineJobPortal.Admin.NewJob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('../Images/bg.jpg'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container pt-4 pb-4">
            <div class="btn-toolbar justify-content-between mb-3">
                <div class="btn-group">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <div class="input-group h-25">
                    <asp:HyperLink ID="hlBack" runat="server" NavigateUrl="~/Admin/JobList.aspx" CssClass="btn btn-secondary" Visible="false">Back</asp:HyperLink>
                </div>
            </div>
            <h3 class="text-center"><% Response.Write(Session["title"]); %></h3>
            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="txtJobTitle" style="font-weight: 600;">Job Title</label>
                    <asp:TextBox ID="txtJobTitle" CssClass="form-control" runat="server" placeholder="Ex. Web Developer, App Developer" required="true"></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="txtNoOfPosts" style="font-weight: 600;">No. of Posts</label>
                    <asp:TextBox ID="txtNoOfPosts" CssClass="form-control" runat="server" placeholder="Enter Number of Positions" TextMode="Number" required="true"></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-12 pt-3">
                    <label for="txtDescription" style="font-weight: 600;">Description</label>
                    <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" placeholder="Enter Job Description" TextMode="MultiLine" required="true"></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="txtQualification" style="font-weight: 600;">Qualification/Education Required</label>
                    <asp:TextBox ID="txtQualification" CssClass="form-control" runat="server" placeholder="Ex. MCA, BTech, MBA, PHD" required="true"></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="txtExperience" style="font-weight: 600;">Experience Required</label>
                    <asp:TextBox ID="txtExperience" CssClass="form-control" runat="server" placeholder="Ex. 2 Years, 1.5 Years" required="true"></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="txtSpecialization" style="font-weight: 600;">Specialization Required</label>
                    <asp:TextBox ID="txtSpecialization" CssClass="form-control" runat="server" placeholder="Enter Specialization" required="true" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="txtLastDate" style="font-weight: 600;">Last Date to Apply</label>
                    <asp:TextBox ID="txtLastDate" CssClass="form-control datepicker" runat="server" placeholder="Enter Last Date to Apply" TextMode="Date" required="true"></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="txtSalary" style="font-weight: 600;">Salary</label>
                    <asp:TextBox ID="txtSalary" CssClass="form-control" runat="server" placeholder="Ex. 25000/Month, 7000/Year" required="true"></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="ddlJobType" style="font-weight: 600;">Job Type</label>
                    <asp:DropDownList ID="ddlJobType" CssClass="form-control w-100" runat="server">
                        <asp:ListItem Value="0">Select Job Type</asp:ListItem>
                        <asp:ListItem>Full Time</asp:ListItem>
                        <asp:ListItem>Part Time</asp:ListItem>
                        <asp:ListItem>Remote</asp:ListItem>
                        <asp:ListItem>Freelance</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Job Type cannot be null!" ControlToValidate="ddlJobType" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" Font-Size="Small" InitialValue="0"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="txtCompany" style="font-weight: 600;">Company/Organization Name</label>
                    <asp:TextBox ID="txtCompany" CssClass="form-control" runat="server" placeholder="Enter Company/Organization Name" required="true"></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="txtCompany" style="font-weight: 600;">Company/Organization Logo</label>
                    <asp:FileUpload ID="fuCompanyLogo" runat="server" CssClass="form-control" ToolTip=".jpg, .jpeg, .png extensions only" />
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="txtWebsite" style="font-weight: 600;">Website</label>
                    <asp:TextBox ID="txtWebsite" CssClass="form-control" runat="server" placeholder="Enter Website" TextMode="Url"></asp:TextBox>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="txtEmail" style="font-weight: 600;">Email</label>
                    <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder="Enter Email" TextMode="Email"></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-12 pt-3">
                    <label for="txtAddress" style="font-weight: 600;">Address</label>
                    <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server" placeholder="Enter Work Location" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-6 pt-3">
                    <label for="ddlCountry" style="font-weight: 600;">Country</label>
                    <asp:DropDownList ID="ddlCountry" class="form-control select2" runat="server"></asp:DropDownList>
                </div>
                <div class="col-md-6 pt-3">
                    <label for="txtState" style="font-weight: 600;">State</label>
                    <asp:TextBox ID="txtState" CssClass="form-control" runat="server" placeholder="Enter State"></asp:TextBox>
                </div>
            </div>

            <div class="row mr-lg-5 ml-lg-5 mb-3">
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAdd" runat="server" Text="Add Job" OnClick="btnAdd_Click" CssClass="btn btn-primary btn-block" BackColor="#7200cf" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
