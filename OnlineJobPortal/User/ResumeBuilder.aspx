<%@ Page Title="Resume Builder" Language="C#" MasterPageFile="~/Layouts/User.Master" AutoEventWireup="true" CodeBehind="ResumeBuilder.aspx.cs" Inherits="OnlineJobPortal.User.ResumeBuilder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <div class="container pt-50 pb-40">
            <div class="row">
                <div class="col-12 pb-20">
                    <asp:Label ID="lblMsg" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="col-12">
                    <h2 class="contact-title text-center">Build Resume</h2>
                </div>
                <div class="col-lg-8 mx-auto">
                    <div class="form-contact contact_form">
                        <div class="row">
                            <div class="col-12">
                                <h3>Personal Information</h3>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Full Name</label>
                                    <asp:TextBox ID="txtFullName" runat="server" placeholder="Enter Full Names" CssClass="form-control" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Username</label>
                                    <asp:TextBox ID="txtUsername" runat="server" placeholder="Enter Username" CssClass="form-control" required="required"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="form-group">
                                    <label>Address</label>
                                    <asp:TextBox ID="txtAddress" runat="server" placeholder="Enter Address" CssClass="form-control" TextMode="MultiLine" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Mobile Number</label>
                                    <asp:TextBox ID="txtMobile" runat="server" placeholder="Enter Mobile Number" CssClass="form-control" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Email Address</label>
                                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Email Address" CssClass="form-control" TextMode="Email" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-group">
                                    <label>Country</label>
                                    <asp:DropDownList ID="ddlCountry" class="form-control select2" runat="server"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-12 pt-4">
                                <h6>Education/Resume Information Information</h6>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>10th Percentage/Grade</label>
                                    <asp:TextBox ID="txtTenth" runat="server" placeholder="Ex: 90%" CssClass="form-control" required="required"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>12th Percentage/Grade</label>
                                    <asp:TextBox ID="txtTwefth" runat="server" placeholder="Ex: 90%" CssClass="form-control" required="required"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Graduation with pointer/Grade</label>
                                    <asp:TextBox ID="txtGraduation" runat="server" placeholder="Ex: Btech with 9.2 pointer" CssClass="form-control" required="required"></asp:TextBox>
                                </div>
                            </div>

                                                        <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Post Graduation with pointer/Grade</label>
                                    <asp:TextBox ID="txtPostGraduation" runat="server" placeholder="Ex: Btech with 9.2 pointer" CssClass="form-control" required="required"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>PHD with Percentage/Grade</label>
                                    <asp:TextBox ID="txtPhd" runat="server" placeholder="PHD with Grade" CssClass="form-control" required="required"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Job Profile/Works On</label>
                                    <asp:TextBox ID="txtWork" runat="server" placeholder="Job Profile" CssClass="form-control" required="required"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Work Experience</label>
                                    <asp:TextBox ID="txtExperience" runat="server" placeholder="Work Experience" CssClass="form-control" required="required"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-12">
                                <div class="form-group">
                                    <label>Resume</label>
                                    <asp:FileUpload ID="fuResume" runat="server" CssClass="form-control pt-2" ToolTip=".doc, .docx, .pdf extensions only!" required="true" />
                                </div>
                            </div>

                        </div>
                        <div class="form-group mt-3">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button button-contactForm boxed-btn mr-4" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
