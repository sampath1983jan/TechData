
<template>    
    <div>
        <h1 class="text-center login-title">Sign in to continue</h1>
        <div class="account-wall">
            <img class="profile-img" src="https://lh5.googleusercontent.com/-b0-k99FZlyE/AAAAAAAAAAI/AAAAAAAAAAA/eu7opA4byxI/photo.jpg?sz=120"
                 alt="">
            <form class="form-signin">
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">                          
                            <input type="text" @blur="validate" id="txtuser" class="form-control" @keyup.alt.67="clear" v-on:keyup.VK_TAB="showdata"  v-model="userName" placeholder="Email" required autofocus>
                        </div>
                        <div class="col-md-12" style="margin-top:10px;">                            
                            <input type="password" @blur="validate" v-model="password" required class="form-control" @keyup.alt.67="clear" placeholder="Password" required>
                        </div>
                        <div class="col-md-12 "  v-if="error!=''"  id="errr"><span class="isError">{{error}}</span></div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="margin-top:10px;">
                            <button class="btn btn-lg btn-primary-color btn-block" @keyup.alt.67="login" v-on:click.prevent="login" type="submit">
                                Sign in
                            </button>
                        </div>

                    </div>

                    <div class="row" style="margin:15px 0px 15px 0px">
                        <div class="col-md-12">
                            <label class="checkbox pull-left">
                                <input type="checkbox" value="remember-me">
                                Remember me
                            </label>
                            <a href="#" class="pull-right need-help"   >Need help? </a><span class="clearfix"></span>
                        </div>

                    </div>
                </div>                
            </form>
        </div>
        <!--<a class="pull-right text-center new-account" href="#" onclick='window.location = "@Url.Action("Registration", "Authentication")";'>Create Account</a>-->
        <a href="#" class="text-center new-account">Create an account </a>
    </div>
</template>
<script >  
    module.exports = {
        data: function () {
            return {
                userName: '',
                password: '',
                error:"",
            }
        }, methods: {
            validate: function () {
                if (this.userName == "") {
                    this.error = ("User Name field is required field");
                  //  $("#txtuser").focus();
                    return;
                }
                if (this.password == "") {
                    this.error = ("Password field is required field")
                   // $('[type=password]').focus();
                    return;
                }
                this.error = "";
            },
            login: function () {
                if (this.userName == "") {
                    this.error =("User Name field is required field");
                    $("#txtuser").focus();
                    return;
                }
                if (this.password == "") {
                    this.error =("Password field is required field")
                    $('[type=password]').focus();
                    return;
                }
                var a = this.data;
                var that = this;
                $.ajax({
                    type: "POST",
                    url: '/Authentication/Login',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ userName: this.userName, Password: this.password, ReturnUrl: "" }),
                    dataType: "json",
                    success: function (data) { window.location.href = data.redirecturl },
                    error: function (s, f) { that.error ="Invalid username or password"}
                });                
            },
            showdata: function () {                 
                $.ajax('/Client/Gets',
                    {
                        dataType: 'jsonp', // type of response data
                        timeout: 500,     // timeout milliseconds
                        success: function (data, status, xhr) {   // success callback function
                            //$('p').append(data.firstName + ' ' + data.middleName + ' ' + data.lastName);
                            console.log(data)
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback 
                            console.log(errorMessage)
                        }
                    });                
               
            },
            clear: function (e) {                
                $(e.target).val("");                
            }
        }
    }
</script>