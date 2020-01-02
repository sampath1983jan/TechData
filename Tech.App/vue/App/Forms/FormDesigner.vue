<template>
    <div> 
        <h4>{{AppForm.Name}}</h4>
        <div class="row">
            <div class="col-md-2">
                <div id="compAttribute"></div>
            </div>
            <div class="col-md-10 dropattribute">             
                <div class="grid-stack dropattribute"></div>
            </div>
        </div>
       
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
                componentTemplate: "<div id='chComp'><div><choose-component :appid='AppID' :formid='FormID' :comp='Component'></choose-component></div></div>",
                componentAttributeTemplate: "<div><component-attribute :attributes='ComponentAttribute' v-on:addfield='NewField'></component-attribute></div>",
                attributeTemplate:"<div id='attProperty'><attribute-property :attribute='attribute'></attribute-property></div>",
                AppForm: {},
                FormFields: [],
                Designer: {},
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
        mounted: function () {
            $('.grid-stack').gridstack({ animate: true, cellHeight: 40, verticalMargin: 5, resizable: { autoHide: true, handles: "e" } });       
            $('.grid-stack').data('gridstack').setAnimation(true);
            this.Designer= new DesignGrid();
        },
        methods: {     
            AttributeForm: function (element) {
                var that = this;
                var jPopupDemo = new jPopup({
                    content: this.attributeTemplate,
                    hashtagValue: '#demopopup',
                    shouldSetHash: false
                });
                $(".jPopup").find(".content").attr("id", "newelement");
                $(".jPopup").find(".content").css("top", "40%");
                $(".jPopup").find(".content").css("width", "90%");
                $(".jPopup").find(".content").css("padding-left", "10%");
                var aProperty = httpVueLoader('/vue/App/Forms/AttributeProperties.vue?' + (Math.random() * 10000));
                var self = new Vue({
                    components: {
                        'attribute-property': aProperty
                    },
                    data: function () {
                        return {
                            AppID: that.AppID,
                            attribute: element,
                            FormID: that.FormID,
                        }
                    },
                }).$mount('#attProperty');
            },
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
                                    FormID: that.FormID,
                                }
                            },
                        }).$mount('#chComp');
                    })
                } else { 
                    $("#compAttribute").append(this.componentAttributeTemplate)
                    var compAttri = httpVueLoader('/vue/App/Forms/FormComponentAttribute.vue?' + (Math.random() * 10000));
                    this.getComp(function (data) {                     
                        var self = new Vue({
                            components: {
                                'component-attribute': compAttri
                            },
                            data: function () {
                                return {
                                    AppID: that.AppID,
                                    ComponentAttribute: data.Attributes,
                                    FormID: that.FormID,
                                }
                            },
                            methods: {
                                NewField: function (element) {
                                    var exist = that.FormFields.filter(function (x) { return x.FieldID == element.FieldID });                                   
                                    if (exist.length > 0) {
                                        return;
                                    }                                  
                                    that.FormFields.push(element);
                                    that.Designer.addNewWidget(element);
                                    that.AttributeForm(element);
                                },
                            }
                        }).$mount('#compAttribute');
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
                $.ajax('/App/' + this.AppID + '/Component/' + this.AppForm.ComponentID,
                    {
                        type: "GET",
                        dataType: 'jsonp', // type of response data
                        //  data: { clientid: id },
                        // timeout milliseconds
                        success: function (data, status, xhr) {   // success callback function    
                      
                            callback(data);
                         
                        },
                        error: function (jqXhr, textStatus, errorMessage) { // error callback
                            alert(errorMessage);
                        }
                    });

            }
        },
        //router: router
    }  

    function DesignGrid() {
        this.afterAdded = function (v) {

        }
        this.items = [
          
        ];
        var count = 0;
        this.grid = $('.grid-stack').data('gridstack');
        this.addNewWidget = function (ele) {
            var node =  {
                x: 0,
                y: this.items.length,
                width: 3,
                height: 1
            };
            this.grid.addWidget($('<div><div class="grid-stack-item-content">' + ele.AttributeName + '</div></div>'), node);
            this.items.push(node);
            this.afterAdded(node);
            return false;
        }.bind(this);
   //     this.addNewWidget();
        this.toggleFloat = function () {
            this.grid.float(!this.grid.float());
            $('#float').html('float: ' + this.grid.float());
        }.bind(this);
        //this.addNewWidget();
        //this.addNewWidget();
        //$('#add-widget').click(this.addNewWidget);
        //$('#float').click(this.toggleFloat);
    };

    </script>
