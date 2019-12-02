
<template>
    <div>
       
             
        <div class="row" style="border-bottom:2px solid #ddd">
            <div class="col-md-8">
                <h4 style="margin-top: 10px;padding: 0px;font-weight: bold;">My application list</h4>
                <label style="padding-left:5px">Manage your application. You can create, edit and remove application based your privileges</label>
            </div>
            <div class="col-md-4" style="padding-left:20%;padding-top:10px"><button id="add" class="btn btn-primary" v-on:click="newapp">New Application</button></div>
        </div>
        <div class="row">
            <div class="col-md-12">
               
            </div>
            <div class="col-md-12">
                <div id="aplist" class="tile">
              
                    <ul>
                        <li v-for="d in Apps" class="tilehg">
                            <div class="">
                                <div class='row '>
                                    <div class='col-md-10'>
                                        <a v-on:click="redirect(d.AppID)">
                                            <h5>{{d.AppName}}</h5>   
                                            <label>{{d.Description}}</label> <br/>
                                            <small>{{d.CreatedOn | formatDate}}</small>
                                        </a>

                                        <!--<router-link :to="d.url">-->
                                        <!--</router-link>-->
                                    </div>
                                    <div class='col-md-2'>
                                        <a href="#" style="text-indent:-100%;" v-bind:name="d.AppID" v-on:click="remove(d.AppID)">
                                            <span title="Delete" act="del" class="remove fa fa-remove"></span>
                                        </a>
                                    </div>
                                </div>
                            </div>

                        </li>
                    </ul>
                </div>
            </div>
            <div class="col-md-5">
                <!--<router-view :key="$route.fullPath"></router-view>-->
                <div id="window"></div>
            </div>
        </div>


    </div>
</template>

<script>     
    
    //const View = {
    //    template: '<div>User {{ id }} {{ this.$route.params.action}}<div >{{App.AppName}}</div>  <div >{{App.Description}}</div>  <div ></div></div>  ',
    //    props: ['id'],
    //    data: function () {
    //        return {
    //            App: {}
    //        }
    //    },
    //    created() {
    //        this.getApp(this.$route.params.id);
    //    },
    //    methods: {
    //        getApp: function (id) {
    //            var that = this;
    //            $.ajax('/App/GetApp',
    //                {
    //                    type: "GET",
    //                    dataType: 'jsonp', // type of response data
    //                    data: { clientid: "3271b7623dd34f7b96e574f2351a1486413346222",appid:id },
    //                    // timeout milliseconds
    //                    success: function (data, status, xhr) {   // success callback function
    //                        that.App = data;                     
    //                    },
    //                    error: function (jqXhr, textStatus, errorMessage) { // error callback
    //                        alert(errorMessage);
    //                    }
    //                });
    //        }
    //    }
    //}

    //const routes = [
    //    //  { path: '/vueclient/:id', component: vueclient },
    //    { path: '/View/:id/:action', component: View, props: true, },
    //    //{ path: '/Bar', component: Bar },
    //    //{ path: '/foo', component: foo },
    //]
    //const router = new VueRouter({
    //    routes: routes// short for `routes: routes`
    //});

     module.exports = {
         data: function (){
             return {
                 Apps: []
             }
         },

         created: function () {
             this.getApps();
         },
         methods: {
             redirect: function (aa) {
                  
                 var aps = this.Apps.filter(function (a) { return a.AppID == aa });
                 window.location = aps[0].url;
             },
             remove: function (id) {
                 var that = this;                
                 $("#window").dialogBox({
                     id: 'wd',
                     title: "",
                     message: "Are you sure, you want to remove this lookup?",
                     onAction: function () {
                         $.ajax('/App/RemoveApp',
                             {
                                 type: "Post",
                                 dataType: 'jsonp', // type of response data
                                 data: { appid: id },
                                 // timeout milliseconds
                                 success: function (data, status, xhr) {   // success callback function
                                     that.getApps();
                                     Window.feedback.$refs.foo.push({
                                         header: "", body: "App removed successfully", template: + "",
                                         autohide: true,
                                         bodyCss:"feedback",
                                         position:"right bottom",
                                     });
                                 },
                                 error: function (jqXhr, textStatus, errorMessage) { // error callback
                                     alert(errorMessage);
                                 }
                             });
                         $("#window").dialogBox('hide');
                     },
                     onCancel: function () {
                         //    alert('no');
                     },
                     type: 2,
                     alertType: 3,
                 })
                 $("#window").dialogBox('show');               
             },
             newapp: function () {
                
                 //$("#fomr").show();
                 //$("#aplist").hide();
                 var demoContent = '<div id="fomr" style="position:absolute;width:95%"><Forming v-on:saved="afterSaved"></Forming></div>';
                 var jPopupDemo = new jPopup({
                     content: demoContent,
                     hashtagValue: '#demopopup',
                     shouldSetHash: false
                 });

                 $(".jPopup").find(".content").css("top", "10%");
                 this.newForm();
                 //var a = new $.App({ "plc": "fomr" });
             },
             newForm: function () {
                 that = this;
                 var a = httpVueLoader('/vue/App/AppForm.vue?3');                 
                 new Vue({
                     components: {
                         'Forming': a,
                     },
                     data: function () {
                         return {
                             inputtext: 23,
                         }
                     },
                     methods: {
                         afterSaved: function () {
                             that.getApps();
                     }
                     }
          
                 }).$mount('#fomr');
             },
             getApps: function () {
                 var that = this;                
                 $.ajax('/App/Get',
                     {
                         type: "GET",
                         dataType: 'jsonp', // type of response data
                         data: { clientid: "3271b7623dd34f7b96e574f2351a1486413346222" },
                         // timeout milliseconds
                         success: function (data, status, xhr) {   // success callback function
                             that.Apps = data;                   
                             $.each(that.Apps, function (i, v) {
                                 v.url = "/app/appinfo/" + v.AppID ;
                                
                             });
                         },
                         error: function (jqXhr, textStatus, errorMessage) { // error callback
                             alert(errorMessage);
                         }
                     });
             }
         },
       //  router: router
    }

    //(function () {
    //    (function ($app) {
    //        $app.App = function (options) {
    //            var defaults = {
    //                plc: "",
    //                AppID: "", 
    //                template:"<div><div>Create New app to build your service</div><label>App Name</label><input type='text'></input></div>"
    //            };
    //            var opt = $.extend({}, defaults, options);
    //            var render = function () {
    //                if (opt.AppID == "") {
    //                    uiRender();
    //                } else {
    //                    getAppDetail(function () {

    //                    });
    //                }                   
    //            }
    //            var uiRender = function () {
    //                $("#" + opt.plc).append(opt.template);
    //            }
    //            var getAppDetail = function (onComplete) {

    //            }
    //            var Save = function () {

    //            }
    //            render();
                
    //        }

    //    })($);
</script>


 