<template>
    <div> 
        <h4>{{AppForm.Name}}</h4>
    </div>   
    
</template>
<script>
    module.exports = {
        name: "FormDeisgner",
        components: {
        
        },
        data: function () {
            return {
                FormID: "",
                AppID: "",
                componentTemplate:"<div id='chComp'><div><choose-component :appid='AppID' :formid='FormID' :comp='Component'></choose-component></div></div>",
                AppForm: {}
            }
        },
        beforeRouteEnter(to, from, next) {          
            next();
        },
        beforeRouteUpdate(to, from, next) {             
            next();
        },
        props: ["name"],
        created: function () {
            this.AppID = this.$route.params.id;
            this.FormID = this.$route.params.fid;
            this.getForm(this.SetComponent);
        },
        methods: {
            SetComponent: function () {   
                var that = this;
                if (this.AppForm.ComponentID == "") {
                  //  $("#newElemt").click(function (e) {                     
                        var jPopupDemo = new jPopup({
                            content: this.componentTemplate,
                            hashtagValue: '#demopopup',
                            shouldSetHash: false
                        });
                        $(".jPopup").find(".content").attr("id", "newelement");
                        $(".jPopup").find(".content").css("top", "40%");
                        $(".jPopup").find(".content").css("width", "90%");
                        $(".jPopup").find(".content").css("padding-left", "10%");                       
                    //});
                    this.getComp(function (data) {
                        var chComp = httpVueLoader('/vue/App/Forms/ChooseComponent.vue?' + (Math.random() * 10000));
                        var self = new Vue({
                            components: {
                                'choose-component': chComp
                            },
                            data: function () {
                                return {
                                    AppID: that.AppID,
                                    Component: data,
                                    FormID:that.FormID,
                                }
                            },
                        }).$mount('#chComp');
                    })
                    
                }

            },
            getForm: function (callback) {
                var cForm = this;               
                    $.ajax('/App/' + this.AppID + '/Form/Get/' + this.FormID,
                        {
                            type: "GET",
                            dataType: 'jsonp', // type of response data
                            data: {},
                            // timeout milliseconds
                            success: function (data, status, xhr) {  // success callback function
                                cForm.AppForm = data;
                                cForm.Name = cForm.AppForm.Name;
                                $("#apptitle").find("h5").css({ "float": "left" });
                                $("#apptitle").find("h5").html("/" + cForm.AppForm.Name);
                                callback();
                            },
                            error: function (jqXhr, textStatus, errorMessage) { // error callback
                                alert(errorMessage);
                            }
                        });              

            },
            getComp: function (callback) {
                var that = this;
                $.ajax('/MyApp/' + this.AppID + '/Components',
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        data: {},
                        // timeout milliseconds
                        success: function (data, status, xhr) {  // success callback function        
                            var d = [];
                            $.each(data, function (i, v) {
                                d.push(v.Component);
                            });
                      
                            callback(d);                             
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });

            }
        },
        //router: router
    }
    </script>
