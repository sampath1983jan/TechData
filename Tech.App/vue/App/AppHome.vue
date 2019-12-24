<template>
    <div>
        <div>
            <!--Component List-->
            <div id="itemlist">
                <div class="row">
                    <div class="col-md-4">
                        <div class=' list' style="border-radius:5px;border-style: solid;border-width: 1px 1px 1px 1px;border-color:#ccc">
                            <div class='' style=" background-color:#ff006e; padding:5px; color:#fff;border-radius:5px">Components</div>
                            <ul>
                                <li v-for="d in Complist"><div class=' rowlight' style='padding-left:15px;'><a v-on:click="onChanged(d.ComponentID)">{{d.ComponentName}} </a></div>
                            </ul>
                            <div style="margin:5px 15px; text-align:right"><a v-on:click="showFullComponent">More ...</a></div>
                        </div>
                    </div>
                </div>
            </div>
            <!--Component List End-->
            <!--Component List-->
            <div id="itemlist">
                <div class="row">
                    <div class="col-md-4">
                        <div class='list' style="border-radius:5px;border-style: solid;border-width: 1px 1px 1px 1px;border-color:#ccc">
                            <div class='' style=" background-color:#ff006e; padding:5px; color:#fff;border-radius:5px">Forms</div>
                            <ul>
                                <li v-for="d in FormList"><div class=' rowlight' style='padding-left:15px;'><a v-on:click="onFormSelection(d.FormID)">{{d.Name}} </a></div>
                            </ul>
                            <div style="margin:5px 15px; text-align:right"><a v-on:click="showFullComponent">More ...</a></div>
                        </div>
                    </div>
                </div>
            </div>
            <!--Component List End-->

        </div>
       
    </div>
</template>
<script>    
    
    module.exports = {
        name: "AppHome",    
     
        components: {
            //'comp-list': a,
            //'comp-form': CompForm,
            //'form-list': fmlist
        },
        data: function () {
            return {
                AppID: appid,
                App: {},
                Complist: [],
                FormList:[]
            }
        },
        beforeRouteEnter(to, from, next) {
    
                        next();
        },
        beforeRouteUpdate(to, from, next) {
          
            next();
        },
        props: ["appid"],
        created: function () {           
            this.AppID = appid;           
            this.getComps();
            this.getApp();     
            this.getForms();
        },
        methods: {    
            onChanged: function (id) {
                this.compid = id;
              
                this.$router.push({ path: "/comp/" + this.AppID + "/" + this.compid, params: { id: this.AppID, cid: this.compid }, props: true });
              

                window.hideMenu();
            },
            onFormSelection: function (id) {
                this.formid = id;
                this.$router.push({ name: "FromDesigner", params: { id: this.AppID, fid: this.formid }, props: true });
                window.hideMenu();
            },
            getTop: function (no, data,type) {
                var i = 1;
                var d = [];
                $.each(data, function (ik, v) {
                    if (i > no) {
                        return;
                    } else {
                        d.push(v[type]);
                        i = i + 1;
                    }
                });
                return d;
            },
            showFullComponent: function () {
                $('#apptitle')[0].click();
            },
            getComps: function () {
                var that = this;
                //  if (localStorage.getItem('CompList') != null) {
                //
                //     that.Complist = this.getTop(5, JSON.parse(localStorage.getItem('CompList')),"Component");
                //  }
                $.ajax('/MyApp/' + this.AppID + '/Components',
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        data: {},
                        // timeout milliseconds
                        success: function (data, status, xhr) {  // success callback function      
                            var d = [];
                            d = that.getTop(5, data,"Component");
                            localStorage.setItem('CompList', JSON.stringify(data));
                            that.Complist = d;
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });
            },
            getApp: function () {
                var that = this;
                $.ajax('/MyApp/' + this.AppID,
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        //  data: {  appid: id },
                        // timeout milliseconds
                        success: function (data, status, xhr) {   // success callback function
                            that.App = data;
                            //    template = template.format( App.Description);
                            $("#apptitle").find("h3").html("");
                            $("#apptitle").find("h3").append(that.App.AppName);
                            // $("#container").append(template);
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });
            }
            ,
            getForms: function () {
                var that = this;
                if (localStorage.getItem('FormList') != null) {
                    this.FormList = this.getTop(-1, JSON.parse(localStorage.getItem('FormList'),"Form"));

                } else {
                    $.ajax('/App/' + this.AppID + '/Form/Get',
                        {
                            type: "GET",
                            dataType: 'jsonp', // type of response data
                            data: {},
                            // timeout milliseconds
                            success: function (data, status, xhr) {  // success callback function
                                var d = [];
                      
                                d = that.getTop(5, data.Forms,"Form");
                                //  localStorage.setItem('FormList', JSON.stringify(data));
                                that.FormList = d;
                            },
                            error: function (jqXhr, textStatus, errorMessage) { // error callback
                                alert(errorMessage);
                            }
                        });
                }

            }
           
        },
    }
    $(document).ready(function () {
        var isShown = false;
        var isSlideShown = false;
        var newElement = '<div >'
            + '<div class="elementItem" id="form">'
            + '<h4>Forms</h4>'
            + '<i class="fas fa-file-code" style="font-size:70px"></i>'
            + '</div>'
            + '<div class="elementItem" id="comp">'
            + ' <h4>Components</h4>'
            + '<i class="fab fa-elementor" style="font-size:70px"></i>'
            + '</div>'
            + '<div class="elementItem" id="pages"> '
            + ' <h4>Pages</h4>'

            + ' <i class="fas fa-file-alt" style="font-size:70px"></i>'
            + '</div>'
            + '<div class="elementItem" id="reports">'
            + '<h4>Reports'
            + '</h4>'
            + '<i class="fas fa-file-word" style="font-size:70px"></i>'
            + '</div>'
            + '</div >';
        $('#apptitle').click(function () {
            var windowHeight = window.innerHeight;
            var h = windowHeight - $("#apptitle").innerHeight();
            h = h;
            $(".screen").css("height", h + "px");

            $('#slider').height(h + "px");

            var hidden = $('#slider');
            if (hidden.hasClass('visible')) {
                hidden.animate({ "left": "-1800px" }, "slow").removeClass('visible');
                window.hideScreen();
            } else {
                hidden.animate({ "left": "0px" }, "slow").addClass('visible');
                window.showScreen();
                if (isSlideShown == false) {
                    //   getComponent();
                    isSlideShown = true;
                }
            }
            $('#homesd').animate({ "left": "-1000px" }, "slow").removeClass('visible');
        });
        window.showScreen = function () {
            $(".screen").removeClass("hidden");
            $(".screen").css("left", "0px")
            $(".screen").css("top", $("#apptitle").innerHeight() + "px")
            var windowHeight = window.innerHeight;
            var h = windowHeight - $("#apptitle").innerHeight();
            h = h;
            $(".screen").css("height", h + "px");

        }
        window.hideScreen = function () {
            $(".screen").addClass("hidden");
            $(".screen").css("left", "-1800px")
        }
        window.hideMenu = function () {
            $('#slider').animate({ "left": "-1800px" }, "slow").removeClass('visible');
            $(".screen").addClass("hidden");
            $(".screen").css("left", "-1800px")
        }
        $(".screen").click(function () {
            hideMenu();
            $('#homesd').animate({ "left": "-1000px" }, "slow").removeClass('visible');
        })
        $("#newElemt").click(function (e) {         
            e.stopPropagation();
            var jPopupDemo = new jPopup({
                content: newElement,
                hashtagValue: '#demopopup',
                shouldSetHash: false
            });
            $(".jPopup").find(".content").attr("id", "newelement");
            $(".jPopup").find(".content").css("top", "40%");
            $(".jPopup").find(".content").css("width", "90%");
            $(".jPopup").find(".content").css("padding-left", "10%");
            $("#comp").click(function () {
                renderNewComponent();
            })
            $("#form").click(function () {
                renderNewForm();
            });
        });
        $("#homenav").click(function () {
            var hidden = $('#homesd');
            var windowHeight = window.innerHeight;
            var h = windowHeight - $("#apptitle").innerHeight();
            h = h;
            $('#homesd').css("top", $("#apptitle").innerHeight() + "px")
            $('#homesd').height(h + "px");

            if (hidden.hasClass('visible')) {
                hidden.animate({ "left": "-1000px" }, "slow").removeClass('visible');
                window.hideScreen();

            } else {
                hidden.animate({ "left": "0px" }, "slow").addClass('visible');
                window.showScreen();
                if (isShown == false) {
                    getApps();
                    isShown = true;
                }
            }
            $('#slider').animate({ "left": "-1800px" }, "slow").removeClass('visible');
        })
        renderNewComponent = function (plc) {
            $("#newelement").html("");
            $("#newelement").append("<div><csimpleform v-on:saved='afterSaved'></csimpleform></div>");
            newCompnonent("newelement");
        }
        renderNewForm = function (plc) {
            $("#newelement").html("");
            $("#newelement").append("<div><simpleform v-on:saved='afterSaved'></simpleform></div>");
            newForm("newelement");
        }
        renderApp = function (data) {
            var alt = "<div class=' list' style='' ><div class='stlabel'>Available Apps</div><ul>";
            $.each(data, function (i, v) {
                alt = alt + "<li>";
                alt = alt + "<div class=' rowlight' style='padding-left:15px;'><a href='" + v.url + "'>" + v.AppName + "</a></div>"
            });
            alt = alt + "</ul><div><a href='/App/Index'>Back to Home</a></div></div>";
            $("#appContainer").html("");
            $("#appContainer").append(alt);
        }
        renderForm = function (data) {
            var cmp = "<div class=' list' style='border-right:1px solid #ccc' ><div class='stlabel'>Form</div><ul>";
            $.each(data, function (i, v) {
                cmp = cmp + "<li>";
                cmp = cmp + "<div class=' rowlight' style='padding-left:15px;'><a href='" + v.url + "'>" + v.ComponentName + "</a></div>"
            });
            cmp = cmp + "</ul></div>";
            $("#formContainer").html("");
            $("#formContainer").append(cmp);
        }
        renderPage = function (data) {
            var cmp = "<div class=' list' style='border-right:1px solid #ccc' ><div class='stlabel'>Page</div><ul>";
            $.each(data, function (i, v) {
                cmp = cmp + "<li>";
                cmp = cmp + "<div class=' rowlight' style='padding-left:15px;'><a href='" + v.url + "'>" + v.ComponentName + "</a></div>"
            });
            cmp = cmp + "</ul></div>";
            $("#pageContainer").html("");
            $("#pageContainer").append(cmp);
        }
        getApps = function () {
            var that = this;
            $.ajax('/App/Get',
                {
                    type: "GET",
                    dataType: 'jsonp', // type of response data

                    // timeout milliseconds
                    success: function (data, status, xhr) {   // success callback function

                        $.each(data, function (i, v) {
                            v.url = "/app/appinfo/" + v.AppID;
                        });
                        renderApp(data);
                    },
                    error: function (jqXhr, textStatus, errorMessage) { // error callback
                        alert(errorMessage);
                    }
                });
        }
    });
</script>